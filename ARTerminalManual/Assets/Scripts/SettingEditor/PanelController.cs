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
    [SerializeField] private SelectedImage[] _imageBinary = new SelectedImage[3];
    /// <summary>
    /// 画像バイナリの設定
    /// </summary>
    /// <param name="index">0:イメージ画像１　1:イメージ画像２　2:QRマーカー</param>
    /// <param name="value">バイナリ</param>
    public void SetImageBinary(int index, byte[] value) { _imageBinary[index].Binary = value; }
    /// <summary>
    /// 画像バイナリの取得
    /// </summary>
    /// <param name="index">0:イメージ画像１　1:イメージ画像２　2:QRマーカー</param>
    /// <returns>バイナリデータ</returns>
    public byte[] GetImageBinary(int index) { return _imageBinary[index].Binary; }

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
    /// イメージ画像
    /// </summary>
    [SerializeField] private Texture2D[] _texture2Ds = new Texture2D[3];
    /// <summary>
    /// イメージ画像の設定
    /// </summary>
    /// <param name="index">0:イメージ画像1 1:イメージ画像2 2:マーカー画像</param>
    /// <param name="texture">テクスチャ</param>
    public void SetTexture2D(int index, Texture2D texture) { if (0 >= index && index < IMAGE_LENGTH) _texture2Ds[index] = texture; }
    /// <summary>
    /// テクスチャの取得
    /// </summary>
    /// <param name="index">0:イメージ画像1 1:イメージ画像2 2:マーカー画像</param>
    /// <returns>テクスチャ</returns>
    public Texture2D GetTexture2D(int index) { return (0 >= index && index < IMAGE_LENGTH ? _texture2Ds[index] : null); }
    /// <summary>
    /// テクスチャの存在チェック
    /// </summary>
    /// <param name="index">0:イメージ画像1 1:イメージ画像2 2:マーカー画像</param>
    /// <returns>true:あり false:なし</returns>
    public bool CheckTexture2D(int index) { return (0 >= index && index < IMAGE_LENGTH ? _texture2Ds[index] != null : false); }
    /// <summary>
    /// イメージ画像の登録名を取得
    /// </summary>
    /// <param name="index">0:イメージ画像1 1:イメージ画像2 2:マーカー画像</param>
    /// <returns>登録名を取得</returns>
    public string GetTexture2D_Name(int index)
    {
        switch (index)
        {
            case IMAGE1: return _name + "_Image1.jpg";
            case IMAGE2: return _name + "_Image2.jpg";
            case IMAGE_MARKER: return _name + "_ImageMarker.jpg";
            default: return "";
        }
    }
    /// <summary>
    /// サーバに画像が存在するかのチェック
    /// </summary>
    private bool[] _texture2Ds_SettingServerFlg = new bool[3];
    /// <summary>
    /// サーバ画像存在チェックの設定
    /// </summary>
    /// <param name="index">0:イメージ画像1 1:イメージ画像2 2:QRマーカー画像</param>
    /// <param name="flg">判定</param>
    public void SetTexture2Ds_SettingServerFlg(int index, bool flg) { if (0 >= index && index < IMAGE_LENGTH) _texture2Ds_SettingServerFlg[index] = flg; }
    /// <summary>
    /// サーバ画像存在チェックの取得
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public bool GetTexture2Ds_SettingServerFlg(int index) { return (0 >= index && index < IMAGE_LENGTH ? _texture2Ds_SettingServerFlg[index] : false); }

    /// <summary>
    /// 削除判定
    /// </summary>
    [SerializeField] private Toggle _isDelete = default;
    /// <summary>
    /// 削除判定
    /// </summary>
    public bool IsDelete { get { return _isDelete.isOn; } }

}
