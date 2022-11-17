/****************************************************
 *  文件：JsonDataMgr#FILEEXTENSION#
 *  作者：子子芽
 *  日期：Created by #SMARTDEVELOPERS# on #CREATETIME#
 *  项目：#PROJECTNAME#
 *  功能：Function
 *  修改日志：Time / Revise

*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public enum JsonTpye
{
    JsonUtlity,
    LitJson
}


public class JsonDataMgr:BaseManager<JsonDataMgr>
{
    /// <summary>
    /// 存储数据 以Json格式存储到硬盘中
    /// </summary>
    /// <param name="data"></param>
    /// <param name="fileName"></param>
    /// <param name="tpye"></param>
    public void SaveData(object data,string fileName,JsonTpye tpye = JsonTpye.LitJson)
    {
        //游戏运行时的路径
        string path = Application.persistentDataPath + "/" + fileName + ".json";
        string jsonData = "";
        switch (tpye)
        {
            case JsonTpye.JsonUtlity:
                jsonData = JsonUtility.ToJson(data);
                break;
            case JsonTpye.LitJson:
                jsonData = JsonMapper.ToJson(data);
                break;
        }
        File.WriteAllText(path, jsonData);
    }


    /// <summary>
    /// 读取数据  从硬盘中读取json数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fileName"></param>
    /// <param name="tpye"></param>
    /// <returns></returns>
    public T LoadData<T>(string fileName, JsonTpye tpye = JsonTpye.LitJson) where T : new()
    {
        //第一步首先确定   数据的路径

        //默认数据文件夹的路径  Application.streamingAssetsPath
        string path = Application.streamingAssetsPath + "/" + fileName + ".json";
        //需要先判断是否存在这个文件
        if (!File.Exists(path))
            //如果不存在这个文件则在 读写文件夹下去寻找这个文件     
            path = Application.persistentDataPath + "/" + fileName + ".json";   
        if (!File.Exists(path))
            //如果还没有则返回一个默认值出去
            return new T();


        string jsonData = File.ReadAllText(path);
        T data = default(T);
        switch (tpye)
        {
            case JsonTpye.JsonUtlity:
                data = JsonUtility.FromJson<T>(jsonData);
                break;
            case JsonTpye.LitJson:
                data = JsonMapper.ToObject<T>(jsonData);
                break;
        }
        return data;
    }

}
