using UnityEngine;
using GoogleARCore;
using System.Collections;

/// <summary>
/// コモンアイテム
/// </summary>
public static class Common
{
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

    /// <summary>
    /// 取得データの一時保存
    /// </summary>
    public struct PANEL_DATA_SET
    {
        public string object_ID;
        public string name;
        public string description;
        public byte[] image1;
        public byte[] image2;
        public byte[] image_marker;
        public string image1Path;
        public string image2Path;
        public string imageMarkerPath;
        public int image1State;
        public int image2State;
        public int imageMarkerState;
    }

}