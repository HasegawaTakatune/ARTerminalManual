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
            List<PanelController> DeleteItem = new List<PanelController>(contents.Where(item => item.IsDelete && !item.isObjectIDNullOrEmpty));

            if (SaveItem.Count > 0)
            {
                Save(SaveItem);
            }

            if (DeleteItem.Count > 0)
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
                saveObject["Name"] = item.Name;
                saveObject["Description"] = item.Description;
                saveObject["Image1"] = item.GetTexture2D_Name(PanelController.IMAGE1);
                saveObject["Image2"] = item.GetTexture2D_Name(PanelController.IMAGE2);
                saveObject["ImageQR"] = item.GetTexture2D_Name(PanelController.IMAGEQR);
                saveObject.SaveAsync((NCMBException e) =>
                {
                    if (e != null) throw e;
                });

                // 画像の保存
                for (int index = 0; index < PanelController.IMAGE_LENGTH; index++)
                {
                    // 画像が設定されていなければスキップ
                    if (!item.CheckTexture2D(index)) continue;

                    // 画像を保存する
                    NCMBFile saveFile = new NCMBFile(item.GetTexture2D_Name(index), item.GetTexture2D(index).EncodeToJPG());
                    saveFile.SaveAsync((NCMBException e) =>
                    {
                        if (e != null) throw e;
                    });
                }
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

                // 画像の削除
                for (int index = 0; index < PanelController.IMAGE_LENGTH; index++)
                {
                    NCMBFile deleteFile = new NCMBFile(item.GetTexture2D_Name(index));
                    deleteFile.DeleteAsync((NCMBException e) =>
                    {
                        if (e != null) throw e;
                    });
                }
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public void GetSettingData(string name = "")
    {
        try
        {
            NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(CLASS_NAME);

            query.CountAsync((int count, NCMBException e) =>
            {
                if (e != null) throw e;
                else if (count < 1) return;
            });

            if (name != "") query.WhereContainedIn("Name", name);

            query.FindAsync((List<NCMBObject> objectList, NCMBException e) =>
            {
                if (e != null) throw e;

                foreach (var item in objectList)
                {
                    string inputName = item["Name"].ToString();
                }
            });

        }
        catch (Exception e)
        {
            throw e;
        }
    }

}
