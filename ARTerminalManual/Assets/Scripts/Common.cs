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
    public static readonly Vector2 NAME_EXPLANATION_MODE = new Vector2(0, 120);

    /// <summary>
    /// 端子説明UI　縮小モード時の名前の配置
    /// </summary>
    public static readonly Vector2 NAME_MINIMAMU_MODE = new Vector2(0, -40);

    /// <summary>
    /// 接続端子UI　詳細説明モード時のリサイズボタンの配置
    /// </summary>
    public static readonly Vector2 RESIZE_EXPLANATION_MODE = new Vector2(25, -220);

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

}
