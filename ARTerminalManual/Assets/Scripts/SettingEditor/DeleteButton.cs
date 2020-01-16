using System;
using UnityEngine;

/// <summary>
/// 削除ボタン
/// </summary>
public class DeleteButton : MonoBehaviour
{
    /// <summary>
    /// パネル
    /// </summary>
    [SerializeField] private GameObject panel = default;

    /// <summary>
    /// 削除クリックイベント
    /// </summary>
    public void OnClickDelete()
    {
        try
        {
            panel.transform.parent = null;
            Destroy(panel);
        }
        catch (Exception e)
        {
            Common.ShowDialog("Error", e.Message);
        }
    }
}
