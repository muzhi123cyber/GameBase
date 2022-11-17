using System;
/****************************************************
 *  文件：BasePanel.cs
 *  作者：子子芽
 *  日期：Created by DefaultCompany on 2022/10/19 19:21:10
 *  项目：Base Framework
 *  功能：Function
 *  修改日志：Time / Revise

*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public abstract class BasePanel : MonoBehaviour
{
    private Dictionary<string,List<UIBehaviour>> controlDic = new Dictionary<string, List<UIBehaviour>>();

    protected virtual void Awake()
    {
        FindChildrenControl<Button>();
        FindChildrenControl<Image>();
        FindChildrenControl<Text>();
        FindChildrenControl<Toggle>();
        FindChildrenControl<InputField>();
        FindChildrenControl<Slider>();
        FindChildrenControl<Scrollbar>();
        FindChildrenControl<ScrollRect>();
    }

    public virtual void ShowMe(){}
    public virtual void HideMe(){}


    /// <summary>
    ///  button Onclick事件
    /// </summary>
    /// <param name="objName">确定是那个对象上的Btn</param>
    protected virtual void OnClick(string objName)
    {
    }

    /// <summary>
    ///  toggle OnValueChanged事件
    /// </summary>
    /// <param name="objName"></param>
    /// <param name="toggleIsOn"></param>
    protected virtual void OnValueChanged(string objName,bool toggleIsOn)
    {
    }

    protected virtual void OnValueChanged(string objName, float floatValue)
    {

    }


    /// <summary>
    /// 得到控件，每一个GameObject 不能挂多个同一类型UI组件
    /// </summary>
    /// <param name="gameObjectName"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetControl<T>(string gameObjectName) where T:UIBehaviour
    {
         if(controlDic.ContainsKey(gameObjectName))
         {
           for(int i=0;i<controlDic[gameObjectName].Count;i++)
           {
                if(controlDic[gameObjectName][i] is T)  
                    //每一个GameObject 不能挂多个同一类型UI组件
                    return controlDic[gameObjectName][i] as T;
           }
         }
        return null;
    }


    private void FindChildrenControl<T>()where T:UIBehaviour
    {
        //获得这个面板下的（包括子物体）携带的 该类型组件
        T[] controls = this.GetComponentsInChildren<T>();
        for(int i = 0; i<controls.Length;i++)
        {
            string gameObjectName = controls[i].gameObject.name;
            //判断这个面板是否添加了这个对象
            if(!controlDic.ContainsKey(gameObjectName))
                controlDic.Add(gameObjectName,new List<UIBehaviour>{controls[i]});
            else
                controlDic[gameObjectName].Add(controls[i]);

            //给不同的控件添加 事件监听
            if(controls[i] is Button)
            {
                (controls[i] as Button).onClick.AddListener(()=>{
                    OnClick(gameObjectName);
                });
            }
            else if(controls[i] is Toggle)
            {
                (controls[i] as Toggle).onValueChanged.AddListener((ison)=>{
                    OnValueChanged(gameObjectName,ison);
                });
            }
            else if (controls[i] is Slider)
            {
                (controls[i] as Slider).onValueChanged.AddListener((fValue) => {
                    OnValueChanged(gameObjectName, fValue);
                });
            }

        }
    }

}
