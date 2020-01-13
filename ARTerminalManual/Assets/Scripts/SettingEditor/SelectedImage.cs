﻿using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// イメージ画像の選択UI
/// </summary>
public class SelectedImage : MonoBehaviour
{

    /// <summary>
    /// イメージUI
    /// </summary>
    [SerializeField] private Image image;

    /// <summary>
    /// ファイル選択ボタンクリックイベント
    /// </summary>
    public void OnClickSelectedFilePath()
    {
        // 画像ファイル取得
        string filePath = FileControll.OpenExistFile(Common.FILTER_IMAGE_FILE);

        // Texture2DをSpriteに変換してImageに設定する
        Texture2D texture = FileControll.FileStream2Texture2D(filePath);
        image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

    }
}
