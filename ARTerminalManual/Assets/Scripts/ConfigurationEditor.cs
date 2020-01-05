using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ConfigurationEditor : MonoBehaviour
{
    // 画像ファイルをバイナリデータに変換するもの
    // ファイルデータ→サーバのデータの持たせ方をちゃんと決めていないため保留
    // サーバに画像データをそのまま保存できるらしいので、このクラス廃止になるかも？

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
