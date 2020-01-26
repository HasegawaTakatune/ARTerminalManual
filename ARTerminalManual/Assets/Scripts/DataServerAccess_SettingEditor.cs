using System.Collections.Generic;
using UnityEngine;
using NCMB;
using System;
using System.Collections;

/// <summary>
/// サーバ間のデータ保存・取得の手続きを行う
/// </summary>
public class DataServerAccess_SettingEditor : MonoBehaviour
{
    /// <summary>
    /// テーブル名
    /// </summary>
    private const string CLASS_NAME = "TerminalManual";
    /// <summary>
    /// カラム名　名前
    /// </summary>
    private const string OBJECT_NAME = "Name";
    /// <summary>
    /// カラム名　説明
    /// </summary>
    private const string OBJECT_DESCRIPTION = "Description";
    /// <summary>
    /// カラム名　画像ステータス
    /// </summary>
    private const string OBJECT_IMAGE1_STATE = "Image1State";
    /// <summary>
    /// カラム名　画像ステータス
    /// </summary>
    private const string OBJECT_IMAGE2_STATE = "Image2Sstate";
    /// <summary>
    /// カラム名　画像ステータス
    /// </summary>
    private const string OBJECT_IMAGE_MARKER_STATE = "ImageMarkerState";

    /// <summary>
    /// コンテンツ
    /// </summary>
    [SerializeField] private GameObject content = default;

    /// <summary>
    /// 表示パネル
    /// </summary>
    [SerializeField] private GameObject prefab = default;

    /// <summary>
    /// 
    /// </summary>
    private void Start()
    {
        GetPanelData();
    }

    /// <summary>
    /// データを保存する
    /// </summary>
    public void SaveData()
    {
        try
        {
            // コンテンツからデータを取得する
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

    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="saveItem">保存するアイテム</param>
    private void Save(List<PanelController> saveItem)
    {
        try
        {
            // ACL 読み込み：可　書き込み：可
            NCMBACL acl = new NCMBACL();
            acl.PublicReadAccess = true;

            foreach (var item in saveItem)
            {
                // データの保存
                NCMBObject saveObject = new NCMBObject(CLASS_NAME);
                if (!item.isObjectIDNullOrEmpty)
                    saveObject.ObjectId = item.ObjectID;
                saveObject[OBJECT_NAME] = item.Name;
                saveObject[OBJECT_DESCRIPTION] = item.Description;

                saveObject[OBJECT_IMAGE1_STATE] = (item.imageBinary[PanelController.IMAGE1].Binary.Length > 0 ? SelectedImage.IMAGE_STATUS_SET : SelectedImage.IMAGE_STATUS_NONE);
                saveObject[OBJECT_IMAGE2_STATE] = (item.imageBinary[PanelController.IMAGE2].Binary.Length > 0 ? SelectedImage.IMAGE_STATUS_SET : SelectedImage.IMAGE_STATUS_NONE);
                saveObject[OBJECT_IMAGE_MARKER_STATE] = (item.imageBinary[PanelController.IMAGE_MARKER].Binary.Length > 0 ? SelectedImage.IMAGE_STATUS_SET : SelectedImage.IMAGE_STATUS_NONE);

                saveObject.SaveAsync((NCMBException e) =>
                {
                    if (e != null) throw e;
                });

                // 画像の保存・削除
                for (int index = 0; index < PanelController.IMAGE_LENGTH; index++)
                {
                    switch (item.imageBinary[index].ImageState)
                    {
                        // 保存
                        case SelectedImage.IMAGE_STATUS_SET:
                            NCMBFile saveFile = new NCMBFile(item.GetTexture2D_Name(index), item.imageBinary[index].Binary); //{ ACL = acl };
                            saveFile.SaveAsync((NCMBException e) => { if (e != null) throw e; });
                            break;
                        // 削除
                        case SelectedImage.IMAGE_STATUS_DELETE:
                            NCMBFile deleteFile = new NCMBFile(item.GetTexture2D_Name(index), item.imageBinary[index].Binary);
                            deleteFile.DeleteAsync((NCMBException e) => { if (e != null) throw e; });
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    /// <summary>
    /// 削除
    /// </summary>
    /// <param name="deleteItem">削除するアイテム</param>
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
                    switch (item.imageBinary[index].ImageState)
                    {
                        // 削除
                        case SelectedImage.IMAGE_STATUS_SET:
                        case SelectedImage.IMAGE_STATUS_DELETE:
                            NCMBFile deleteFile = new NCMBFile(item.GetTexture2D_Name(index));
                            deleteFile.DeleteAsync((NCMBException e) => { if (e != null) throw e; });
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    /// <summary>
    /// パネルデータの取得
    /// </summary>
    public void GetPanelData()
    {
        try
        {
            List<Common.PANEL_DATA_SET> dataSet = new List<Common.PANEL_DATA_SET>();
            NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(CLASS_NAME);

            // データが取得できない・保存されていない場合処理を抜ける
            query.CountAsync((int count, NCMBException e) =>
            {
                if (e != null) throw e;
                else if (count < 1) return;
            });

            query.FindAsync((List<NCMBObject> objectList, NCMBException e) =>
            {
                if (e != null) throw e;
                Common.PANEL_DATA_SET ds = new Common.PANEL_DATA_SET();
                foreach (var item in objectList)
                {
                    // データの取得
                    ds.object_ID = item.ObjectId;
                    ds.name = item[OBJECT_NAME].ToString();
                    ds.description = item[OBJECT_DESCRIPTION].ToString();

                    ds.image1State = (int.TryParse(item[OBJECT_IMAGE1_STATE].ToString(), out int result) ? result : SelectedImage.IMAGE_STATUS_NONE);
                    ds.image2State = (int.TryParse(item[OBJECT_IMAGE2_STATE].ToString(), out result) ? result : SelectedImage.IMAGE_STATUS_NONE);
                    ds.imageMarkerState = (int.TryParse(item[OBJECT_IMAGE_MARKER_STATE].ToString(), out result) ? result : SelectedImage.IMAGE_STATUS_NONE);

                    // 画像1の取得
                    if (ds.image1State == SelectedImage.IMAGE_STATUS_SET)
                    {
                        NCMBFile file = new NCMBFile(ds.name + PanelController.IMAGE1_PATH);
                        file.FetchAsync((byte[] fileData, NCMBException fileEx) =>
                        {
                            if (fileEx != null) ds.image1 = fileData;
                            else throw fileEx;
                        });
                    }
                    // 画像2の取得
                    if (ds.image2State == SelectedImage.IMAGE_STATUS_SET)
                    {
                        NCMBFile file = new NCMBFile(ds.name + PanelController.IMAGE2_PATH);
                        file.FetchAsync((byte[] fileData, NCMBException fileEx) =>
                        {
                            if (fileEx != null) ds.image2 = fileData;
                            else throw fileEx;
                        });
                    }
                    // マーカー画像の取得
                    if (ds.imageMarkerState == SelectedImage.IMAGE_STATUS_SET)
                    {
                        NCMBFile file = new NCMBFile(ds.name + PanelController.IMAGE_MARKER_PATH);
                        file.FetchAsync((byte[] fileData, NCMBException fileEx) =>
                        {
                            if (fileEx != null) ds.image_marker = fileData;
                            else throw fileEx;
                        });
                    }
                    dataSet.Add(ds);
                }
                ShowSettingData(dataSet);
            });
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    /// <summary>
    /// 設定データの表示
    /// </summary>
    /// <param name="dataSet"></param>
    private void ShowSettingData(List<Common.PANEL_DATA_SET> dataSet)
    {
        try
        {
            GameObject obj;
            PanelController panel;

            foreach (var item in dataSet)
            {
                obj = Instantiate(prefab, content.transform);
                panel = obj.GetComponent<PanelController>();

                panel.ObjectID = item.object_ID;
                panel.Name = item.name;
                panel.Description = item.description;

                panel.imageBinary[PanelController.IMAGE1].ImageState = item.image1State;
                panel.imageBinary[PanelController.IMAGE2].ImageState = item.image2State;
                panel.imageBinary[PanelController.IMAGE_MARKER].ImageState = item.imageMarkerState;

                panel.imageBinary[PanelController.IMAGE1].Binary = item.image1;
                panel.imageBinary[PanelController.IMAGE2].Binary = item.image2;
                panel.imageBinary[PanelController.IMAGE_MARKER].Binary = item.image_marker;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }
}
