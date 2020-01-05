using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Windows.Forms;

/// <summary>
/// ファイル選択ダイヤログ
/// </summary>
public class OpenFileButton : MonoBehaviour
{

    /// <summary>
    /// 
    /// </summary>
    public string inputFilePath;

    /// <summary>
    /// ファイル選択ダイヤログ表示
    /// </summary>
    public void OpenExistFile()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();

        // InputFieldの初期値を代入しておく
        //openFileDialog.FileName = inputFieldPath.text;

        // 開くファイルを指定する
        openFileDialog.Filter = "JPGファイル|*.jpeg,*.jpg,*|PNGファイル|*.png";

        // ファイルが存在しない場合は警告を出す(true)、出さない(false)
        openFileDialog.CheckFileExists = false;

        // ダイヤログを開く
        openFileDialog.ShowDialog();

        // 取得したファイル名をInputFieldに代入する
        inputFilePath = openFileDialog.FileName;
    }

}
