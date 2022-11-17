using System;
using System.Security.Cryptography.X509Certificates;
/****************************************************
 *  文件：UIManager.cs
 *  作者：子子芽
 *  日期：Created by DefaultCompany on 2022/10/19 19:21:1
 *  项目：Base Framework
 *  功能：Function
 *  修改日志：Time / Revise

*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public enum E_UI_Layer
{
    Bot,
    Mid,
    Top,
    System
}


public class UIManager : BaseManager<UIManager>
{
    public Dictionary<string,BasePanel> panelDic = new Dictionary<string, BasePanel>();

    private Transform Bot;
    private Transform Mid;
    private Transform Top;
    private Transform SystemLayer;
    public RectTransform canvasTrans;

    public UIManager()
    {
        if( canvasTrans == null)
        {
            GameObject UIRoot = ResMgr.GetInstance().Load<GameObject>("UI/2DUI");
            canvasTrans = UIRoot.transform.Find("Canvas") as RectTransform;
            GameObject.DontDestroyOnLoad(UIRoot);

            Bot = canvasTrans.Find("Bot");
            Mid = canvasTrans.Find("Mid");
            Top = canvasTrans.Find("Top");
            SystemLayer = canvasTrans.Find("System");
        }
    }


    /// <summary>
    /// 返回自定义的 UI层 Obj
    /// </summary>
    /// <param name="layer"> </param>
    /// <returns></returns>
    public Transform GetUILayer(E_UI_Layer layer)
    {
        switch(layer)
        {
            case E_UI_Layer.Bot:
                return Bot;
            case E_UI_Layer.Mid:
                return Mid;
            case E_UI_Layer.Top:
                return Top;
            case E_UI_Layer.System:
                return SystemLayer;
            default:
                return  null;
        }
    }


    public void ShowPanel<T>(string panelName,E_UI_Layer panelLayer = E_UI_Layer.Mid,UnityAction<T> callBack = null) where T:BasePanel
    {
        if(panelDic.ContainsKey(panelName))
        {
            panelDic[panelName].ShowMe();
            if(callBack!= null)
                callBack(panelDic[panelName] as T);
            return;
        }

        ResMgr.GetInstance().LoadResAsyn<GameObject>("UI/Plane/" + panelName,(obj)=>{
            Transform parent = Mid;
            switch(panelLayer)
            {
            case E_UI_Layer.Bot:
                parent = Bot;
                break;
            case E_UI_Layer.Top:
                parent = Top;
                break;
            case E_UI_Layer.System:
                parent = SystemLayer;
                break;
            }
            obj.transform.SetParent(parent,false);

            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            (obj.transform as RectTransform).offsetMax = Vector2.zero;
            (obj.transform as RectTransform).offsetMin = Vector2.zero;

            T panel = obj.GetComponent<T>();
            if(callBack!= null)
                callBack(panel);
            panel.ShowMe();
            panelDic.Add(panelName,panel);
        });
    }


    public void HidePanel<T>(string panelName) where T:BasePanel
    {
        if(panelDic.ContainsKey(panelName))
        {
            panelDic[panelName].HideMe();
            GameObject.Destroy(panelDic[panelName].gameObject);
            panelDic.Remove(panelName);
        }
    }



    public void ClearAllPanel()
    {
        foreach (var item in panelDic.Values)
        {
            item.HideMe();
            GameObject.Destroy(item.gameObject);
        }
        panelDic.Clear();
    }


    public T GetPanel<T>() where T:BasePanel
    {
        if(panelDic.ContainsKey(typeof(T).Name))
        {
            return panelDic[typeof(T).Name] as T;
        }
        return null;
    }

    /// <summary>
    /// 添加自定义监听事件
    /// </summary>
    /// <param name="objName"></param>
    /// <param name="type"></param>
    /// <param name="data"></param>
    public void AddCustomListener(UIBehaviour control,EventTriggerType type,UnityAction<BaseEventData> callback)
    {
        EventTrigger trigger = control.GetComponent<EventTrigger>();
        if(trigger == null)
           trigger = control.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID =  type;
        entry.callback.AddListener(callback);

        trigger.triggers.Add(entry);
    }
}
