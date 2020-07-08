using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 設定パネル
/// </summary>
public class PanelController : MonoBehaviour
{
    public const string IMAGE1_PATH = "_Image1.jpg";
    public const string IMAGE2_PATH = "_Image2.jpg";
    public const string IMAGE_MARKER_PATH = "_ImageMarker.jpg";

    /// <summary>
    /// イメージ画像1
    /// </summary>
    public const int IMAGE1 = 0;
    /// <summary>
    /// イメージ画像２
    /// </summary>
    public const int IMAGE2 = 1;
    /// <summary>
    /// マーカー画像
    /// </summary>
    public const int IMAGE_MARKER = 2;
    /// <summary>
    /// 長さ
    /// </summary>
    public const int IMAGE_LENGTH = 3;

    /// <summary>
    /// オブジェクトID
    /// </summary>
    public string ObjectID = "";
    /// <summary>
    /// オブジェクトIDが設定されているかの判定
    /// </summary>
    public bool isObjectIDNullOrEmpty { get { return string.IsNullOrEmpty(ObjectID); } }

    /// <summary>
    /// 名前
    /// </summary>
    [SerializeField] private InputField _name = default;
    /// <summary>
    /// 名前
    /// </summary>
    public string Name { get { return _name.text; } set { _name.text = value; } }

    /// <summary>
    /// 説明
    /// </summary>
    [SerializeField] private InputField _description = default;
    /// <summary>
    /// 説明
    /// </summary>
    public string Description { get { return _description.text; } set { _description.text = value; } }

    /// <summary>
    /// 画像
    /// </summary>
    [SerializeField] public SelectedImage[] imageBinary = new SelectedImage[3];

    /// <summary>
    /// イメージ画像の登録名を取得
    /// </summary>
    /// <param name="index">0:イメージ画像1 1:イメージ画像2 2:マーカー画像</param>
    /// <returns>登録名を取得</returns>
    public string GetTexture2D_Name(int index)
    {
        switch (index)
        {
            case IMAGE1: return _name.text + IMAGE1_PATH;
            case IMAGE2: return _name.text + IMAGE2_PATH;
            case IMAGE_MARKER: return _name.text + IMAGE_MARKER_PATH;
            default: return "";
        }
    }

    /// <summary>
    /// 削除判定
    /// </summary>
    [SerializeField] private Toggle _isDelete = default;
    /// <summary>
    /// 削除判定
    /// </summary>
    public bool IsDelete { get { return _isDelete.isOn; } }
}
