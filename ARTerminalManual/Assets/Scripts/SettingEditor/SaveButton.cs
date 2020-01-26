using UnityEngine;

/// <summary>
/// 登録ボタン
/// </summary>
public class SaveButton : MonoBehaviour
{
    /// <summary>
    /// サーバ処理クラス
    /// </summary>
    [SerializeField] private DataServerAccess_SettingEditor serverAccess = default;

    /// <summary>
    /// 登録ボタンクリックイベント
    /// </summary>
    public void OnClickSaveButton()
    {
        if (serverAccess != null)
            serverAccess.SaveData();
    }
}
