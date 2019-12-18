using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;

public class ARManager : MonoBehaviour
{
    private List<AugmentedImage> augmentedImages = new List<AugmentedImage>();

    [SerializeField] private GameObject prefab = default;

    private GameObject ARObj = default;

    private void Update()
    {
        // トラッキングできなかった場合何もしない
        if(Session.Status != SessionStatus.Tracking)
            return;

        // ARCoreがトラッキングしているものを取得する。
        Session.GetTrackables<AugmentedImage>(augmentedImages, TrackableQueryFilter.Updated);

        foreach(AugmentedImage image in augmentedImages)
        {
            if(image.TrackingState == TrackingState.Tracking)
            {
                if(ARObj == null)
                {
                    // トラッキングを始めたらオブジェクトを生成する
                    Anchor anchor = image.CreateAnchor(image.CenterPose);
                    ARObj = Instantiate(prefab, anchor.transform);
                }
                else
                {
                    // トラッキング中は位置・回転を調節する
                    ARObj.transform.position = image.CenterPose.position;
                    ARObj.transform.rotation = image.CenterPose.rotation;
                }
            }
        }
    }
}
