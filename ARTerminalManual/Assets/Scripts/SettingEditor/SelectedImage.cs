using GoogleARCore;
using System;
using System.Threading.Tasks;
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
    /// 
    /// </summary>
    [SerializeField] private AugmentedImageDatabase imageDatabase = default;

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
    public void OnClickSelectedQRFilePathAsync()
    {
        try
        {
            // 画像ファイル取得
            string filePath = FileControll.OpenExistFile(Common.FILTER_IMAGE_FILE);

            if (filePath.Length < 1) return;

            // Texture2DをSpriteに変換してImageに設定する
            Texture2D texture = FileControll.FileStream2Texture2D(filePath);
            image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

            if (imageDatabase == null || QRMarkerQuality == null) return;

            // ARマーカを取得しスコアを表示して開放する                 
            Task task = ShowImageQuality(texture);

            // 取得したファイルからバイナリデータを取得する
            binary = FileControll.ReadFile2Binary(filePath);

        }
        catch (Exception e)
        {
            Common.ShowDialog("Error", e.Message);
        }
    }


    private async Task ShowImageQuality(Texture2D texture)
    {
        try
        {
            if (imageDatabase == null)
                imageDatabase = GameObject.Find("ARCore Device").GetComponent<ARCoreSession>().SessionConfig.AugmentedImageDatabase;

            Debug.Log("Check Start");
            int index = 0;
            index = await Task.Run(() => imageDatabase.AddImage("TEST", texture, 1));

            Debug.Log("ImageDatabase index:" + index);
            QRMarkerQuality.text = "Quality:" + imageDatabase[index].Quality;
            Debug.Log("Quality:" + imageDatabase[index].Quality);
        }
        catch (Exception e)
        {
            throw e;
        }
    }
}
