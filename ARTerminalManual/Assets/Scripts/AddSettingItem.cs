using UnityEngine;

/// <summary>
/// 設定項目の追加を行う
/// </summary>
public class AddSettingItem : MonoBehaviour
{
    /// <summary>
    /// 追加先のコンテンツ
    /// </summary>
    [SerializeField] private GameObject content;

    /// <summary>
    /// 追加するプレファブ
    /// </summary>
    [SerializeField] private GameObject prefab;

    /// <summary>
    /// アイテムの追加
    /// </summary>
    public void OnClickAddItemButton()
    {
        Instantiate(prefab, content.transform);
    }

}
