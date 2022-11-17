/****************************************************
 *  文件：MonoMgr.cs
 *  作者：子子芽
 *  日期：Created by DefaultCompany on 2022/3/18 11:58:8
 *  项目：Base Framework
 *  功能：提供给外部的Mono模块 帧更新和开启，暂停协程接口
 *  修改日志：Time / Revise

*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoMgr :  BaseManager<MonoMgr>
{
    /// <summary>
    /// 添加提供Mono帧更新事件监听接口
    /// </summary>
    /// <param name="fun"></param>
    public void AddUpdateListener(UnityAction fun)
    {
        MonoController.GetInstance().AddUpdateListener(fun);
    }

    /// <summary>
    /// 移除帧更新事件监听
    /// </summary>
    /// <param name="fun"></param>
    public void RemoveUpdateListener(UnityAction fun)
    {
       MonoController.GetInstance().RemoveUpdateListener(fun);
    }

    public Coroutine StartCoroutine(string methodName)
    {
        return MonoController.GetInstance().StartCoroutine(methodName);
    }

    public Coroutine StartCoroutine(IEnumerator routine)
    {
        return MonoController.GetInstance().StartCoroutine(routine);
    }
    public void StopAllCoroutines()
    {
        MonoController.GetInstance().StopAllCoroutines();
    }

    public void StopCoroutine(IEnumerator routine)
    {
        MonoController.GetInstance().StopCoroutine(routine);
    }

    public void StopCoroutine(Coroutine routine)
    {
        MonoController.GetInstance().StopCoroutine(routine);
    }

    public void StopCoroutine(string methodName)
    {
        MonoController.GetInstance().StopCoroutine(methodName);
    }
}
