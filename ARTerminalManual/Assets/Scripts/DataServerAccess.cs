using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;
using System;
using System.Linq;
using System.Threading.Tasks;
using NCMB.Tasks;

/// <summary>
/// サーバ間のデータ保存・取得の手続きを行う
/// </summary>
public class DataServerAccess : MonoBehaviour
{
    private const string CLASS_NAME = "TerminalManual";

    private NCMBQuery<NCMBObject> query;

    [SerializeField] private GameObject content;

    public void SaveSettingData()
    {
        try
        {
            if (content = null) return;

            PanelController[] contents = content.GetComponentsInChildren<PanelController>();
            if (contents.Length < 1) return;

            // 保存・削除するアイテムを取得する
            List<PanelController> SaveItem = new List<PanelController>(contents.Where(item => !item.IsDelete));
            List<PanelController> DeleteItem = new List<PanelController>(contents.Where(item => item.IsDelete && item.ObjectID != ""));

        }
        catch (Exception e)
        {
            throw e;
        }
    }

    private async void Save(List<PanelController> saveItem)
    {
        try
        {
            // タスクにデータ保存の処理を入れ込む
            List<Task<NCMBObject>> tasks = new List<Task<NCMBObject>>();

            foreach (var item in saveItem)
            {
                NCMBObject saveObject = new NCMBObject(CLASS_NAME);
                saveObject["Name"] = item.Name;
                saveObject["Description"] = item.Description;
                saveObject["Image1"] = item.GetTexture2D_Name(PanelController.IMAGE1);
                saveObject["Image2"] = item.GetTexture2D_Name(PanelController.IMAGE2);
                saveObject["ImageQR"] = item.GetTexture2D_Name(PanelController.IMAGEQR);

                Task<NCMBObject> task = NCMBFileTaskExtension.SaveTaskAsync(saveObject);
                tasks.Add(task);
            }

            // タスクの実行
            await Task.WhenAll(tasks);
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    private async void SaveImage(List<PanelController> saveItem)
    {
        try
        {
            // タスクにデータ保存の処理を入れ込む
            List<Task<NCMBFile>> tasks = new List<Task<NCMBFile>>();

            foreach (var item in saveItem)
            {
                NCMBFile saveFile = new NCMBFile(CLASS_NAME);                
                saveFile[item.GetTexture2D_Name(PanelController.IMAGE1)] = item.GetTexture2D(PanelController.IMAGE1).EncodeToJPG();
                saveFile[item.GetTexture2D_Name(PanelController.IMAGE2)] = item.GetTexture2D(PanelController.IMAGE2).EncodeToJPG();
                saveFile[item.GetTexture2D_Name(PanelController.IMAGEQR)] = item.GetTexture2D(PanelController.IMAGEQR).EncodeToJPG();

                Task<NCMBFile> task = NCMBFileTaskExtension.SaveTaskAsync(saveFile);
                tasks.Add(task);
            }

            // タスクの実行
            await Task.WhenAll(tasks);
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    private void Delete(List<PanelController> deleteItem)
    {

    }
}
