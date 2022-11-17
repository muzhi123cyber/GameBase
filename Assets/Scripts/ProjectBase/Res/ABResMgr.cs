using System.ComponentModel;
using System.Security.Cryptography;
/****************************************************
 *  文件：ABResMgr.cs
 *  作者：子子芽
 *  日期：Created by DefaultCompany on 2022/10/26 13:50:52
 *  项目：ABundle_Xlua_Learn
 *  功能：Function
 *  修改日志：Time / Revise

*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ABResMgr : SingletonAutoMono<ABResMgr>
{
    //定义一个主包
    public Dictionary<string,AssetBundle> abDic = new Dictionary<string, AssetBundle>();
    /// <summary>
    /// 主包信息
    /// </summary>
    private  AssetBundle abMain = null;
    //主包的固定信息
    private  AssetBundleManifest abmf = null;

    //AB包存放路径，根据具体设定进行更改
    private string ABPathUrl
    {
        get{ return Application.streamingAssetsPath + "/";}
    }

    //主包名
    private string MainABName
    {
        get{
            #if UNITY_IOS
                return "IOS";
            #elif UNTIY_ANDROID
                return "Android";
            #else
                return "PC";
            #endif
        }
    }


    /// <summary>
    /// 记载AB包的依赖信息
    /// </summary>
    /// <param name="abName"></param>
    private void LoadAB(string abName)
    {
        //加载主包 设定主包命名规范
        if(abMain == null )
        {
            abMain = AssetBundle.LoadFromFile(ABPathUrl + MainABName);
            //通过主包获取AB包的固定信息
            abmf = abMain.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }

        //从主包中获取 需要加载的包的 所有的依赖信息并加载
        string[] strs = abmf.GetAllDependencies(abName);
        for(int i = 0 ; i < strs.Length ; i++)  //添加AB包依赖
        {
            if(!abDic.ContainsKey(strs[i]))    //判断依赖的AB包是否已经加载
            {
                abDic.Add(strs[i],AssetBundle.LoadFromFile(ABPathUrl + strs[i]));
            }
        }

        //加载AB包
        if(!abDic.ContainsKey(abName))
            abDic.Add(abName,AssetBundle.LoadFromFile( ABPathUrl + abName));
    }


    //同步加载资源
    /// <summary>
    /// 加载资源——object
    /// </summary>
    /// <param name="abName">Ab包的名字</param>
    /// <param name="resNAme">资源名字</param>
    /// <returns></returns>
    public object ABLoadRes(string abName, string resNAme)
    {
        LoadAB(abName);   //添加AB包依赖

        //加载资源
        Object obj = abDic[abName].LoadAsset(resNAme);
        if(obj is GameObject)   //如果是一个游戏对象则
           return Instantiate(obj);
        return obj;
    }

    /// <summary>
    /// 加载资源——object
    /// </summary>
    /// <param name="abName">Ab包的名字</param>
    /// <param name="resNAme">资源名字</param>
    /// <returns></returns>
    public object ABLoadRes(string abName, string resNAme, System.Type typ)
    {
        LoadAB(abName);   //添加AB包依赖

        //加载资源
        Object obj = abDic[abName].LoadAsset(resNAme,typ);
        if(obj is GameObject)   //如果是一个游戏对象则进行实例化操作
           return Instantiate(obj);
        return obj;
    }



    // / <summary>
    // / 通过泛型加载 -- 但是lua不支持泛型。。
    // / </summary>
    // / <typeparam name="T"></typeparam>
    public T ABLoadRes<T>(string abName, string resNAme) where T:Object
    {
        LoadAB(abName);   //添加AB包
        //加载资源
        T obj = abDic[abName].LoadAsset<T>(resNAme);
        if( obj is GameObject)
            return Instantiate(obj);
        return obj;
    }


    //异步加载资源
    //AB加载时 并没有使用AB包加载
    //只有加载资源时使用了异步加载

    /// <summary>
    /// 根据名字 进行异步加载、
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resNAme"></param>
    /// <param name="callBack"></param>
    public void ABLoadResAsync(string abName, string resNAme,UnityAction<Object> callBack = null)
    {
        StartCoroutine(RealABLoadResAsysn(abName,resNAme,callBack));
    }

    IEnumerator RealABLoadResAsysn(string abName, string resNAme,UnityAction<Object> callBack = null)
    {
        LoadAB(abName);
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resNAme);
        yield return abr;

        if(abr.asset is GameObject)
            callBack?.Invoke(Instantiate(abr.asset));
        else
            callBack?.Invoke(abr.asset);
    }



    /// <summary>
    /// 根据Type 进行异步加载、
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resNAme"></param>
    /// <param name="callBack"></param>
    public void ABLoadResAsync(string abName, string resNAme,System.Type typ,UnityAction<Object> callBack = null)
    {
        StartCoroutine(RealABLoadResAsysn(abName,resNAme,typ,callBack));
    }

    IEnumerator RealABLoadResAsysn(string abName, string resNAme,System.Type typ,UnityAction<Object> callBack = null)
    {
        LoadAB(abName);
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resNAme,typ);
        yield return abr;

        if(abr.asset is GameObject)
            callBack?.Invoke(Instantiate(abr.asset));
        else
            callBack?.Invoke(abr.asset);
    }


    /// <summary>
    /// 根据泛型,进行异步加载。
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resNAme"></param>
    /// <param name="callBack"></param>
    /// <typeparam name="T"></typeparam>
    public void ABLoadResAsync<T>(string abName, string resNAme,UnityAction<T> callBack = null) where T:Object
    {
        StartCoroutine(RealABLoadResAsysn(abName,resNAme,callBack));
    }

    IEnumerator RealABLoadResAsysn<T>(string abName, string resNAme,UnityAction<T> callBack = null) where T:Object
    {
        LoadAB(abName);
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync<T>(resNAme);
        yield return abr;

        if(abr.asset is GameObject)
            callBack?.Invoke(Instantiate(abr.asset as T));
        else
            callBack?.Invoke(abr.asset as T);
    }



    /// <summary>
    /// 单个AB包卸载
    /// </summary>
    /// <param name="abName"></param>
    public void UnLoadAB(string abName)
    {
        if(abDic.ContainsKey(abName))
        {
            abDic[abName].Unload(false);    //false卸载不会卸载场景上的物品
            abDic.Remove(abName);
        }
    }


    /// <summary>
    /// 所有AB包卸载
    /// </summary>
    public void UnLoadAllAB()
    {
        AssetBundle.UnloadAllAssetBundles(false);
        abDic.Clear();
        abMain = null;
        abmf = null;
    }
}
