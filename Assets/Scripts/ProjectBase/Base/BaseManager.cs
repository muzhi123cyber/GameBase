/****************************************************
 *  文件：BaseManager.cs
 *  作者：子子芽
 *  日期：Created by DefaultCompany on 2022/3/16 11:33:48
 *  项目：Base Framework
 *  功能：普通单例模式基类
 *  修改日志：Time / Revise

*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager<T> where T : new()
{

    private static T instance;

    public static T GetInstance()
    {
        if (instance == null)
        {
            instance = new T();
        }
        return instance;
    }


    /// <summary>
    /// 私有无参构造函数 不允许单例模式被实例化
    /// </summary>
    protected BaseManager()
    {
    }
}
