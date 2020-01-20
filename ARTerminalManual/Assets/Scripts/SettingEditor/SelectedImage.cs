using GoogleARCore;
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
    /// QRマーカのスコア値を表示する
    /// </summary>
    [SerializeField] private Text QRMarkerQuality = default;

    /// <summary>
    /// ARCoreセッション
    /// </summary>
    private ARCoreSessionConfig sessionConfig = default;

    /// <summary>
    /// バイナリデータ
    /// </summary>
    public byte[] binary;

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
            binary = FileControll.ReadFile2Binary(filePath);

        }
        catch (Exception e)
        {
            Common.ShowDialog("Error", e.Message);
        }
    }

    /// <summary>
    /// ファイル選択ボタンクリックイベント
    /// </summary>
    public void OnClickSelectedQRFilePath()
    {
        try
        {
            if (QRMarkerQuality == null) return;

            if (sessionConfig == null)
                sessionConfig = new ARCoreSessionConfig();

            // 画像ファイル取得
            string filePath = FileControll.OpenExistFile(Common.FILTER_IMAGE_FILE);

            if (filePath.Length < 1) return;

            // Texture2DをSpriteに変換してImageに設定する
            Texture2D texture = FileControll.FileStream2Texture2D(filePath);
            image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

            // ARマーカを取得しスコアを表示して開放する
            int index = sessionConfig.AugmentedImageDatabase.AddImage("None", texture, 1);
            QRMarkerQuality.text = "Quality:" + sessionConfig.AugmentedImageDatabase[index].Quality;
            sessionConfig.AugmentedImageDatabase.RemoveAt(index);

            // 取得したファイルからバイナリデータを取得する
            binary = FileControll.ReadFile2Binary(filePath);

        }
        catch (Exception e)
        {
            Common.ShowDialog("Error", e.Message);
        }
    }
}
