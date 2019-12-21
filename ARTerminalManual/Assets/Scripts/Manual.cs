using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class Manual : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private string Name;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField, TextArea] private string Explanation;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private Image[] Images = new Image[2];

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private RectTransform Panel;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private Text NameUI;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private Text ExplanationUI;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private Image[] ImageUI = new Image[2];

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private RectTransform ResizeButton;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private Text ResizeText;

    /// <summary>
    /// 
    /// </summary>
    private bool isMax = false;

    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
        Resize(isMax);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    public void Init(string name = "")
    {
        if (name != "")
        {
            //LoadInfo(name);
            NameUI.text = name;
            ExplanationUI.text = name;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnClickResize()
    {
        isMax = !isMax;
        Resize(isMax);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isMaxsize"></param>
    private void Resize(bool isMaxsize)
    {
        if (isMaxsize)
        {
            Panel.localScale = Common.MAX_SIZE;
            NameUI.rectTransform.localPosition = Common.NAME_EXPLANATION_MODE;
            ResizeButton.localPosition = Common.RESIZE_EXPLANATION_MODE;
            ResizeText.text = "return◀";
        }
        else
        {
            Panel.localScale = Common.MIN_SIZE;
            NameUI.rectTransform.localPosition = Common.NAME_MINIMAMU_MODE;
            ResizeButton.localPosition = Common.RESIZE_MINIMAMU_MODE;
            ResizeText.text = "more▶";
        }

        ExplanationUI.enabled = isMaxsize;
        for (int i = 0; i < ImageUI.Length; i++)
            ImageUI[i].enabled = isMaxsize;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private IEnumerator LoadInfo(string path)
    {
        // ファイルの存在確認
        if (!System.IO.File.Exists(path))
        {
            yield break;
        }

        // ファイル読み込み
        WWW request = new WWW(Common.FILE_HEADER + Common.INFO_FILE_PATH + path);

        // 読み込み待ち
        while (!request.isDone)
        {
            yield return new WaitForEndOfFrame();
        }

        string[] slice = request.text.Split('\n');

        if (slice.Length == 4)
        {
            NameUI.text = slice[0];
            ExplanationUI.text = slice[1];
            string[] item = new string[] { slice[2], slice[3] };
            LoadImage(item);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private IEnumerator LoadImage(string[] path)
    {
        if (path.Length != 2) yield break;

        WWW request = null;
        Sprite sprite = null;

        for (int i = 0; i < path.Length; i++)
        {
            // ファイルの存在確認
            if (!System.IO.File.Exists(path[i]))
            {
                yield break;
            }

            // ファイル読み込み
            request = new WWW(Common.FILE_HEADER + Common.IMAGE_FILE_PATH + path[i]);

            // 読み込み待ち
            while (!request.isDone)
            {
                yield return new WaitForEndOfFrame();
            }

            // 画像を取り出す
            Texture2D texture = request.texture;

            // 読み込んだ画像からSpriteを作成する
            sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

            ImageUI[i].sprite = sprite;
        }

        yield return 0;
    }
}
