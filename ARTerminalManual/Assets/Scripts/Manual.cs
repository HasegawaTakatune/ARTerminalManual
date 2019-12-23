using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    /// <param name="bytes"></param>
    /// <returns></returns>
    private Texture Binary2Texture(byte[] bytes)
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(bytes);
        return texture;
    }
}
