using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// 接続端子説明UI
/// </summary>
public class Manual : MonoBehaviour
{
    /// <summary>
    /// 最大サイズ
    /// </summary>
    public static readonly Vector3 MAX_SIZE = new Vector3(1.2f, 4.0f, 1.0f);

    /// <summary>
    /// 最小サイズ
    /// </summary>
    public static readonly Vector3 MIN_SIZE = new Vector3(1.2f, 0.5f, 1.0f);

    /// <summary>
    /// 端子説明UI　詳細説明モード時の名前の配置
    /// </summary>
    public static readonly Vector2 NAME_DESCRIPTION_MODE = new Vector2(0, 120);

    /// <summary>
    /// 端子説明UI　縮小モード時の名前の配置
    /// </summary>
    public static readonly Vector2 NAME_MINIMAMU_MODE = new Vector2(0, -40);

    /// <summary>
    /// 接続端子UI　詳細説明モード時のリサイズボタンの配置
    /// </summary>
    public static readonly Vector2 RESIZE_DESCRIPTION_MODE = new Vector2(25, -220);

    /// <summary>
    /// 端子説明UI　縮小モード時のリサイズボタンの配置
    /// </summary>
    public static readonly Vector2 RESIZE_MINIMAMU_MODE = new Vector2(25, -60);

    /// <summary>
    /// パネル
    /// </summary>
    [SerializeField] private RectTransform Panel = default;

    /// <summary>
    /// 名前
    /// </summary>
    [SerializeField] private Text NameUI = default;

    /// <summary>
    /// 説明
    /// </summary>
    [SerializeField] private Text DescriptionUI = default;

    /// <summary>
    /// イメージ画像
    /// </summary>
    [SerializeField] private Image[] ImageUI = new Image[2];

    /// <summary>
    /// more/returnボタン
    /// </summary>
    [SerializeField] private RectTransform ResizeButton = default;

    /// <summary>
    /// more/returnボタンのテキスト
    /// </summary>
    [SerializeField] private Text ResizeText = default;

    /// <summary>
    /// サイズ変更フラグ
    /// </summary>
    private bool isMax = false;

    /// <summary>
    /// 初期化
    /// </summary>
    private void Awake()
    {
        Resize(isMax);

    }

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="name"></param>
    public void Init(string name = "")
    {
        if (name != "")
        {
            //LoadInfo(name);
            NameUI.text = name;
            DescriptionUI.text = name;
        }
    }

    /// <summary>
    /// more/returnボタンクリックイベント
    /// </summary>
    public void OnClickResize()
    {
        isMax = !isMax;
        Resize(isMax);
    }

    /// <summary>
    /// パネルサイズ変更処理
    /// </summary>
    /// <param name="isMaxsize">サイズ true:最大 false:最小</param>
    private void Resize(bool isMaxsize)
    {
        if (isMaxsize)
        {
            Panel.localScale = MAX_SIZE;
            NameUI.rectTransform.localPosition = NAME_DESCRIPTION_MODE;
            ResizeButton.localPosition = RESIZE_DESCRIPTION_MODE;
            ResizeText.text = "return◀";
        }
        else
        {
            Panel.localScale = MIN_SIZE;
            NameUI.rectTransform.localPosition = NAME_MINIMAMU_MODE;
            ResizeButton.localPosition = RESIZE_MINIMAMU_MODE;
            ResizeText.text = "more▶";
        }

        DescriptionUI.enabled = isMaxsize;
        for (int i = 0; i < ImageUI.Length; i++)
            ImageUI[i].enabled = isMaxsize;
    }

    /// <summary>
    /// バイナリ―データをテクスチャに変換する
    /// </summary>
    /// <param name="bytes">画像データ</param>
    /// <returns></returns>
    private Texture Binary2Texture(byte[] bytes)
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(bytes);
        return texture;
    }
}
