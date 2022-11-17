/****************************************************
 *  文件：SingletonAutoMono.cs
 *  作者：子子芽
 *  日期：Created by DefaultCompany on 2022/3/16 11:45:34
 *  项目：Base Framework
 *  功能：继承mono的单例模式   
 *  修改日志：Time / Revise
       继承该单例模式的类会自动生成一个过场景不被移除的GameObject


*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonAutoMono<T> :MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T GetInstance()
    {
        if (instance == null)
        {
            GameObject obj = new GameObject();
            obj.name = typeof(T).ToString();
            DontDestroyOnLoad(obj);
            instance = obj.AddComponent<T>();
        }
        return instance;
    }


}
