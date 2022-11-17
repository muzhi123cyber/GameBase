/****************************************************
 *  文件：ResMgr.cs
 *  作者：子子芽
 *  日期：Created by DefaultCompany on 2022/3/21 11:24:4
 *  项目：Base Framework
 *  功能：资源加载模块
 *  修改日志：资源同步加载,资源异步加载

*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResMgr : BaseManager<ResMgr>
{
    public T Load<T>(string name) where T:Object
    {
        T obj = Resources.Load<T>(name);
        if(obj is GameObject)
            return GameObject.Instantiate(obj);  //如果是gameObject则实例化后返回出去
        else
            return obj;
    }


    public void LoadResAsyn<T>(string name , UnityAction<T> callBack = null ) where T:Object
    {
        MonoMgr.GetInstance().StartCoroutine(RealLoadResAsyn<T>(name,callBack));
    }

    private IEnumerator RealLoadResAsyn<T>(string name , UnityAction<T> callBack = null ) where T:Object
    {
        ResourceRequest rr = Resources.LoadAsync<T>(name);
        yield return rr;

        if(callBack != null)
        {
            if(rr.asset is GameObject)
                callBack.Invoke(GameObject.Instantiate(rr.asset) as T);
            else
                callBack.Invoke(rr.asset as T);
        }
    }
}
