using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ダイヤログの制御
/// </summary>
public class Dialog : MonoBehaviour
{
    /// <summary>
    /// タイトル
    /// </summary>
    [SerializeField] private Text Title = default;

    /// <summary>
    /// メッセージ
    /// </summary>
    [SerializeField] private InputField Message = default;

    /// <summary>
    /// ポップアップ制御
    /// </summary>
    [SerializeField] private Popup popup = default;

    /// <summary>
    /// 初期化
    /// </summary>
    private void Start()
    {
        Title.text = string.Empty;
        Message.text = string.Empty;
        Common.Dialog = this;
    }

    /// <summary>
    /// ダイアログ表示
    /// </summary>
    /// <param name="t">タイトル</param>
    /// <param name="m">メッセージ</param>
    public void ShowDialog(string t = "Title", string m = "Message")
    {
        Title.text = t;
        Message.text = m;
        popup.Open();
    }
}
