using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 設定パネル
/// </summary>
public class PanelController : MonoBehaviour
{
    /// <summary>
    /// オブジェクトID
    /// </summary>
    public string ObjectID = "";

    /// <summary>
    /// イメージ画像1
    /// </summary>
    public const int IMAGE1 = 0;
    /// <summary>
    /// イメージ画像２
    /// </summary>
    public const int IMAGE2 = 1;
    /// <summary>
    /// RQ画像
    /// </summary>
    public const int IMAGEQR = 2;

    /// <summary>
    /// 名前
    /// </summary>
    [SerializeField] private InputField _name = default;
    /// <summary>
    /// 名前
    /// </summary>
    public string Name { get { return _name.text; } }

    /// <summary>
    /// 説明
    /// </summary>
    [SerializeField] private InputField _description = default;
    /// <summary>
    /// 説明
    /// </summary>
    public string Description { get { return _description.text; } }

    /// <summary>
    /// 画像
    /// </summary>
    [SerializeField] private SelectedImage[] _imageBinary = new SelectedImage[3];
    /// <summary>
    /// 画像
    /// </summary>
    /// <param name="index"> インデックス 0:イメージ画像１　1:イメージ画像２　2:QRマーカー</param>
    /// <returns>バイナリデータ</returns>
    public byte[] GetImageBinary(int index) { return _imageBinary[index].binary; }

    [SerializeField] private Texture2D[] _texture2Ds = new Texture2D[3];
    public Texture2D GetTexture2D(int index) { return _texture2Ds[index]; }
    public string GetTexture2D_Name(int index)
    {
        switch (index)
        {
            case IMAGE1: return _name + "_Image1.jpg";
            case IMAGE2: return _name + "_Image2.jpg";
            case IMAGEQR: return _name + "_ImageQR.jpg";
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
