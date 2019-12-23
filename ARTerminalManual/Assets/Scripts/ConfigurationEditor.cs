using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ConfigurationEditor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
