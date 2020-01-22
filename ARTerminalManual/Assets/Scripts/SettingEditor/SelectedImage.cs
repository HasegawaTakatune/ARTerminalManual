using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// イメージ画像の選択UI
/// </summary>
public class SelectedImage : MonoBehaviour
{
    /// <summary>
    /// イメージUI
    /// </summary>
    [SerializeField] private Image image = default;

    /// <summary>
    /// バイナリデータ
    /// </summary>
    private byte[] _binary;
    public byte[] Binary
    {
        get { return _binary; }
        set
        {
            _binary = value;
            if (_binary == null) return;
            Texture2D texture = FileControll.Binary2Texture2D(_binary);
            image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
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
            string filePath = FileControll.OpenExistFile(Common.FILTER_IMAGE_FILE);

            if (filePath.Length < 1) return;

            // Texture2DをSpriteに変換してImageに設定する
            Texture2D texture = FileControll.FileStream2Texture2D(filePath);
            image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

            // 取得したファイルからバイナリデータを取得する
            _binary = FileControll.ReadFile2Binary(filePath);

        }
        catch (Exception e)
        {
            Common.ShowDialog("Error", e.Message);
        }
    }
}
