using NCMB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStore : MonoBehaviour
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
    /// データの保存場所
    /// </summary>
    public Common.PANEL_DATA_SET[] dataSet = default;

    // Start is called before the first frame update
    private void Start()
    {

    }

    private void GetManuslData()
    {
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(CLASS_NAME);

        query.FindAsync((List<NCMBObject> objectList, NCMBException e) =>
        {

        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
