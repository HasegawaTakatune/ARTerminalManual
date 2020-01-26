using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// イメージ画像の選択UI
/// </summary>
public class SelectedImage : MonoBehaviour
{
    /// <summary>
    /// ファイル選択のフィルター（画像ファイル）
    /// </summary>
    ///public const string FILTER_IMAGE_FILE = "JPGファイル|*.jpeg,*.jpg,*|PNGファイル|*.png";
    public const string FILTER_IMAGE_FILE = "JPGファイル|*.jpg";

    /// <summary>
    /// 画像なし
    /// </summary>
    public const int IMAGE_STATUS_NONE = 0;
    /// <summary>
    /// 画像設定
    /// </summary>
    public const int IMAGE_STATUS_SET = 1;
    /// <summary>
    /// 画像削除
    /// </summary>
    public const int IMAGE_STATUS_DELETE = 2;

    /// <summary>
    /// イメージUI
    /// </summary>
    [SerializeField] private Image image = default;

    /// <summary>
    /// 画像ステータス
    /// </summary>
    private int _imageState = IMAGE_STATUS_NONE;

    public int ImageState
    {
        get
        {
            if (_imageState == IMAGE_STATUS_SET)
            {
                if (_binary.Length > 0) return IMAGE_STATUS_SET;
                else return IMAGE_STATUS_DELETE;
            }
            else
            {
                if (_binary.Length > 0) return IMAGE_STATUS_SET;
                else return IMAGE_STATUS_NONE;
            }
        }
        set { _imageState = value; }
    }

    /// <summary>
    /// バイナリデータ
    /// </summary>
    [SerializeField] private byte[] _binary = null;

    /// <summary>
    /// バイナリデータ
    /// </summary>
    public byte[] Binary
    {
        get { return _binary; }
        set
        {
            _binary = value;
            if (_binary == null)
            {
                //if (_imageState == IMAGE_STATUS_SET)
                //    _imageState = IMAGE_STATUS_DELETE;
                image.sprite = null;
                return;
            }
            Texture2D texture = FileControll.Binary2Texture2D(_binary);
            image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            //_imageState = IMAGE_STATUS_SET;
        }
    }

    /// <summary>
    /// ファイル選択ボタンクリックイベント
    /// </summary>
    public void OnClickSelectedFilePath()
    {
        try
        {
            // 画像ファイル取得
            string filePath = FileControll.OpenExistFile(FILTER_IMAGE_FILE);

            if (filePath.Length < 1)
            {
                image.sprite = null;
                Binary = null;
                return;
            }

            // 取得したファイルからバイナリデータを取得する
            Binary = FileControll.ReadFile2Binary(filePath);
        }
        catch (Exception e)
        {
            Common.ShowDialog("Error", e.Message);
        }
    }
}
