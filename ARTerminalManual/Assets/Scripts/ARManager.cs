using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class ARManager : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    private List<AugmentedImage> augmentedImages = new List<AugmentedImage>();

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private GameObject prefab = default;

    /// <summary>
    /// 
    /// </summary>
    private GameObject ARObj = default;

    /// <summary>
    /// 
    /// </summary>
    private List<string> itemList = new List<string>();

    /// <summary>
    /// 
    /// </summary>
    private void Update()
    {
        // トラッキングできなかった場合何もしない
        if (Session.Status != SessionStatus.Tracking)
            return;

        // ARCoreがトラッキングしているものを取得する。
        Session.GetTrackables<AugmentedImage>(augmentedImages, TrackableQueryFilter.Updated);

        foreach (AugmentedImage image in augmentedImages)
        {
            if (image.TrackingState == TrackingState.Tracking)
            {
                if (itemList.IndexOf(image.Name) == -1)
                {
                    // トラッキングを始めたらオブジェクトを生成する
                    Anchor anchor = image.CreateAnchor(image.CenterPose);
                    ARObj = Instantiate(prefab, anchor.transform);
                    ARObj.GetComponent<Manual>().Init(image.Name);
                    itemList.Add(image.Name);
                }
                else
                {
                    // トラッキング中は位置・回転を調節する
                    ARObj.transform.position = image.CenterPose.position;
                }
            }
        }
    }
}
