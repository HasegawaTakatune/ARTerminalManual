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
        }
        catch (Exception e)
        {
            Common.ShowDialog("Error", e.Message);
        }
    }
}
