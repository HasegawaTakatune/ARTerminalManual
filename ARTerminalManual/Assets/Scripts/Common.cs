using UnityEngine;

/// <summary>
/// コモンアイテム
/// </summary>
public static class Common
{
    /// <summary>
    /// 最大サイズ
    /// </summary>
    public static readonly Vector3 MAX_SIZE = new Vector3(1.2f, 4.0f, 1.0f);

    /// <summary>
    /// 最小サイズ
    /// </summary>
    public static readonly Vector3 MIN_SIZE = new Vector3(1.2f, 0.5f, 1.0f);

    /// <summary>
    /// 端子説明UI　詳細説明モード時の名前の配置
    /// </summary>
    public static readonly Vector2 NAME_DESCRIPTION_MODE = new Vector2(0, 120);

    /// <summary>
    /// 端子説明UI　縮小モード時の名前の配置
    /// </summary>
    public static readonly Vector2 NAME_MINIMAMU_MODE = new Vector2(0, -40);

    /// <summary>
    /// 接続端子UI　詳細説明モード時のリサイズボタンの配置
    /// </summary>
    public static readonly Vector2 RESIZE_DESCRIPTION_MODE = new Vector2(25, -220);

    /// <summary>
    /// 端子説明UI　縮小モード時のリサイズボタンの配置
    /// </summary>
    public static readonly Vector2 RESIZE_MINIMAMU_MODE = new Vector2(25, -60);

    /// <summary>
    /// ファイルヘッダー
    /// </summary>
    public const string FILE_HEADER = "file://";

    /// <summary>
    /// 
    /// </summary>
    public const string IMAGE_FILE_PATH = "ARTerminalManual/Images/";

    /// <summary>
    /// 
    /// </summary>
    public const string INFO_FILE_PATH = "ARTerminalManual/Text/";

    /// <summary>
    /// ファイル選択のフィルター（画像ファイル）
    /// </summary>
    public const string FILTER_IMAGE_FILE = "JPGファイル|*.jpeg,*.jpg,*|PNGファイル|*.png";

    /// <summary>
    /// ダイアログ
    /// </summary>
    private static Dialog _dialog = null;
    /// <summary>
    /// ダイアログ
    /// </summary>
    public static Dialog Dialog { set { if (_dialog = null) _dialog = value; } }
    /// <summary>
    /// ダイアログ表示
    /// </summary>
    /// <param name="t">タイトル</param>
    /// <param name="m">メッセージ</param>
    /// <returns>成功の可否</returns>
    public static void ShowDialog(string t = "Title", string m = "Message") { try { if (_dialog != null) _dialog.ShowDialog(t, m); } catch { } }

}