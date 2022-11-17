/****************************************************
 *  文件：SceneMgr.cs
 *  作者：子子芽
 *  日期：Created by DefaultCompany on 2022/3/18 19:6:4
 *  项目：Base Framework
 *  功能：场景加载模块
 *  修改日志：Time / Revise
        同步加载，异步加载
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneMgr : BaseManager<SceneMgr>
{
    public void LoadScene(string sceneName,UnityAction callBack = null)
    {
        SceneManager.LoadScene(sceneName);
        if(callBack!=null)
            callBack();
    }

    public void LoadSceneAsyn(string sceneName,UnityAction callBack = null)
    {
        MonoMgr.GetInstance().StartCoroutine(RealLoadSceneAsyn(sceneName,callBack));
    }

    private IEnumerator RealLoadSceneAsyn(string sceneName,UnityAction callBack = null)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);
        while(!ao.isDone)  //判断场景是否加载完成
        {
            //这里因为 异步加载场景的 ao.progress 的值的范围为（0 ~ 0.9f）所以结果需要除以 0.9 得到（ 0 ~ 1 ）的结果
            EventCenter.GetInstance().EventTrigger("进度条加载事件",ao.progress/0.9f);   //添加
            yield return ao.progress;
        }
        if(callBack != null)
            callBack();
    }

}
