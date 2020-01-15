using UnityEngine;
using System.Windows.Forms;
using System.IO;

/// <summary>
/// ファイル選択ダイヤログ
/// </summary>
public class FileControll : MonoBehaviour
{

    /// <summary>
    /// ファイル選択ダイヤログ表示
    /// </summary>
    /// <param name="filter">ファイル選択のフィルター</param>
    /// <param name="fileName">初期時に選択するファイル位置</param>
    /// <returns>ファイルパス</returns>
    public static string OpenExistFile(string filter = "", string fileName = "Desktop")
    {
        // 開くファイルを指定する、ファイルが存在しない場合は警告を出す(true)、出さない(false)
        OpenFileDialog openFileDialog = new OpenFileDialog { FileName = fileName, Filter = filter, CheckFileExists = false };
        // ダイヤログを開く
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            // 取得したファイル名をInputFieldに代入する
            return openFileDialog.FileName;
        }
        return string.Empty;
    }

    /// <summary>
    /// テクスチャファイルの取得
    /// </summary>
    /// <param name="path">ファイルパス</param>
    /// <returns>テクスチャ</returns>
    public static Texture2D FileStream2Texture2D(string path)
    {
        Texture2D texture = null;
        if (File.Exists(path))
        {
            // byte取得
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryReader binary = new BinaryReader(fileStream);
            byte[] readBinary = binary.ReadBytes((int)binary.BaseStream.Length);
            binary.Close();
            fileStream.Dispose();
            fileStream = null;

            if (readBinary != null)
            {
                int pos = 16;
                // 横サイズ
                int width = 0;
                for (int i = 0; i < 4; i++) width = width * 256 + readBinary[pos++];
                // 縦サイズ
                int height = 0;
                for (int i = 0; i < 4; i++) height = height * 256 + readBinary[pos++];

                // byteからtexture2D作成
                texture = new Texture2D(80, 80);
                texture.LoadImage(readBinary);
            }
            readBinary = null;
        }
        return texture;
    }

    /// <summary>
    /// 読み込んだファイルをバイナリ―データに変換する
    /// </summary>
    /// <param name="path">ファイルパス</param>
    /// <returns>バイナリ―データ</returns>
    private byte[] ReadFile2Binary(string path)
    {
        using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            BinaryReader binaryReader = new BinaryReader(fileStream);
            byte[] binary = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length);
            binaryReader.Close();
            return binary;
        }
    }

}
