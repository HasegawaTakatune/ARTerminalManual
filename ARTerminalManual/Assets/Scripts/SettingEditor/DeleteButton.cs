using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 削除ボタン
/// </summary>
public class DeleteButton : MonoBehaviour
{
    /// <summary>
    /// パネル
    /// </summary>
    [SerializeField] private GameObject panel;

    /// <summary>
    /// 削除クリックイベント
    /// </summary>
    public void OnClickDelete()
    {
        panel.transform.parent = null;
        Destroy(panel);
    }
}
