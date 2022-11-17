
/****************************************************
 *  文件：PoolMgr.cs
 *  作者：子子芽
 *  日期：Created by DefaultCompany on 2022/3/16 17:44:50
 *  项目：Base Framework
 *  功能：Function
 *  修改日志：Time / Revise

*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PoolData
{
    public GameObject father;
    public List<GameObject> lists = new List<GameObject>();

    public PoolData(GameObject obj,GameObject fatherObj)
    {
        father = new GameObject();
        father.name = obj.name;
        father.transform.SetParent(fatherObj.transform);
        PushObj(obj);
    }

    //将游戏对象存入缓存池中
    public void PushObj(GameObject obj)
    {
        //失活存入List列表中
        obj.SetActive(false);
        lists.Add(obj);
        obj.transform.parent = father.transform;
    }
    public GameObject GetObj()
    {
        GameObject obj = null;
        obj = lists[0];
        obj.SetActive(true);
        lists.RemoveAt(0);
        obj.transform.parent = null;
        return obj;
    }

}



public class PoolMgr : BaseManager<PoolMgr>
{
    private Dictionary<string,PoolData> poolDic = new Dictionary<string, PoolData>();
    private GameObject pool;


    public void GetObj(string path,UnityAction<GameObject> callBack)
    {
        if(poolDic.ContainsKey(path) && poolDic[path].lists.Count > 0 )
        {
            callBack(poolDic[path].GetObj());
        }
        else
        {
            ResMgr.GetInstance().LoadResAsyn<GameObject>(path,(obj)=>{
                obj.name = path;
                callBack(obj);
            });
        }
    }

    public void PushObj(string name, GameObject obj)
    {
        if(pool == null)
            pool = new GameObject("pool");

        if(poolDic.ContainsKey(name))
        {
            poolDic[name].PushObj(obj);
        }
        else
        {
            poolDic.Add(name,new PoolData(obj,pool));
        }
    }


    public void Clear()
    {
        poolDic.Clear();
        pool = null;
    }
}
