using UnityEngine;
using System;

/// <summary>
/// 設定項目の追加を行う
/// </summary>
public class AddItemButton : MonoBehaviour
{
    /// <summary>
    /// 追加先のコンテンツ
    /// </summary>
    [SerializeField] private GameObject content = default;

    /// <summary>
    /// 追加するプレファブ
    /// </summary>
    [SerializeField] private GameObject prefab = default;

    /// <summary>
    /// アイテムの追加
    /// </summary>
    public void OnClickAddItemButton()
    {
        try
        {
            Instantiate(prefab, content.transform);
        }
        catch (Exception e)
        {
            Common.ShowDialog("Error", e.Message);
        }
    }

}
