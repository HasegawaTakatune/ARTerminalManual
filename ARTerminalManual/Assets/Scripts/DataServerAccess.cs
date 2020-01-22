using System.Collections.Generic;
using UnityEngine;
using NCMB;
using System;
using System.Threading.Tasks;

/// <summary>
/// サーバ間のデータ保存・取得の手続きを行う
/// </summary>
public class DataServerAccess : MonoBehaviour
{
    private const string CLASS_NAME = "TerminalManual";
    private const string OBJECT_NAME = "Name";
    private const string OBJECT_DESCRIPTION = "Description";
    private const string OBJECT_IMAGE1 = "Image1";
    private const string OBJECT_IMAGE2 = "Image2";
    private const string OBJECT_IMAGE_MARKER = "ImageMarker";

    [SerializeField] private GameObject content = default;

    [SerializeField] private GameObject prefab = default;

    private void Start()
    {
        Task task = ShowSettingDataAsync();
    }

    public void SaveSettingData()
    {
        try
        {
            PanelController[] contents = content.GetComponentsInChildren<PanelController>();

            // 保存・削除するアイテムを取得する
            if (contents.Length < 1) return;
            List<PanelController> SaveItem = new List<PanelController>();
            List<PanelController> DeleteItem = new List<PanelController>();
            foreach (var value in contents)
            {
                if (!value.IsDelete)
                {
                    SaveItem.Add(value);
                }
                else if (!value.isObjectIDNullOrEmpty)
                {
                    DeleteItem.Add(value);
                }
            }

            // 保存・削除を行う
            if (SaveItem != null && SaveItem.Count > 0)
            {
                Save(SaveItem);
            }
            if (DeleteItem != null && DeleteItem.Count > 0)
            {
                Delete(DeleteItem);
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    private void Save(List<PanelController> saveItem)
    {
        try
        {
            // ACL 読み込み：可　書き込み：可
            NCMBACL acl = new NCMBACL();
            acl.PublicReadAccess = true;
            acl.PublicWriteAccess = true;

            foreach (var item in saveItem)
            {
                // データの保存
                NCMBObject saveObject = new NCMBObject(CLASS_NAME);
                if (!item.isObjectIDNullOrEmpty)
                    saveObject.ObjectId = item.ObjectID;
                saveObject[OBJECT_NAME] = item.Name;
                saveObject[OBJECT_DESCRIPTION] = item.Description;
                saveObject[OBJECT_IMAGE1] = item.GetImageBinary(PanelController.IMAGE1);
                saveObject[OBJECT_IMAGE2] = item.GetImageBinary(PanelController.IMAGE2);
                saveObject[OBJECT_IMAGE_MARKER] = item.GetImageBinary(PanelController.IMAGE_MARKER);
                saveObject.SaveAsync((NCMBException e) =>
                {
                    if (e != null) throw e;
                });

                //// 画像の保存
                //for (int index = 0; index < PanelController.IMAGE_LENGTH; index++)
                //{
                //    // 画像が設定されていなければスキップ
                //    if (!item.CheckTexture2D(index)) continue;

                //    // 画像を保存する
                //    NCMBFile saveFile = new NCMBFile(item.GetTexture2D_Name(index), item.GetTexture2D(index).EncodeToJPG());
                //    saveFile.SaveAsync((NCMBException e) =>
                //    {
                //        if (e != null) throw e;
                //    });
                //}
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    private void Delete(List<PanelController> deleteItem)
    {
        try
        {
            foreach (var item in deleteItem)
            {
                // データの削除
                NCMBObject deleteObject = new NCMBObject(CLASS_NAME);
                deleteObject.ObjectId = item.ObjectID;
                deleteObject.DeleteAsync((NCMBException e) =>
               {
                   if (e != null) throw e;
               });

                //// 画像の削除
                //for (int index = 0; index < PanelController.IMAGE_LENGTH; index++)
                //{
                //    NCMBFile deleteFile = new NCMBFile(item.GetTexture2D_Name(index));
                //    deleteFile.DeleteAsync((NCMBException e) =>
                //    {
                //        if (e != null) throw e;
                //    });
                //}
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public List<Common.PANEL_DATA_SET> GetSettingData(string name = "")
    {
        try
        {
            List<Common.PANEL_DATA_SET> dataSet = new List<Common.PANEL_DATA_SET>();
            NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(CLASS_NAME);

            query.CountAsync((int count, NCMBException e) =>
            {
                if (e != null) throw e;
                else if (count < 1) return;
            });

            if (name != "") query.WhereContainedIn(OBJECT_NAME, name);

            query.FindAsync((List<NCMBObject> objectList, NCMBException e) =>
            {
                if (e != null) throw e;

                Common.PANEL_DATA_SET ds = new Common.PANEL_DATA_SET();
                foreach (var item in objectList)
                {
                    ds.object_ID = item.ObjectId;
                    ds.name = item[OBJECT_NAME].ToString();
                    ds.description = item[OBJECT_DESCRIPTION].ToString();
                    ds.image1 = System.Text.Encoding.UTF8.GetBytes(item[OBJECT_IMAGE1].ToString());
                    ds.image2 = System.Text.Encoding.UTF8.GetBytes(item[OBJECT_IMAGE2].ToString());
                    ds.image_marker = System.Text.Encoding.UTF8.GetBytes(item[OBJECT_IMAGE_MARKER].ToString());
                    dataSet.Add(ds);
                }
            });
            return dataSet;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    private async Task ShowSettingDataAsync()
    {
        try
        {
            List<Common.PANEL_DATA_SET> dataSet = new List<Common.PANEL_DATA_SET>();
            await Task.Run(() => dataSet = GetSettingData());

            GameObject obj;
            PanelController panel;
            Debug.Log("Call dataSet count:" + dataSet.Count);
            foreach (var item in dataSet)
            {
                obj = Instantiate(prefab, content.transform);
                panel = obj.GetComponent<PanelController>();

                panel.Name = item.name;
                panel.Description = item.description;
                panel.SetImageBinary(PanelController.IMAGE1, item.image1);
                panel.SetImageBinary(PanelController.IMAGE2, item.image2);
                panel.SetImageBinary(PanelController.IMAGE_MARKER, item.image_marker);
            }
        }
        catch (Exception e)
        {
            //Common.ShowDialog("Error", e.Message);
            throw e;
        }
    }
}
