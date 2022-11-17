/****************************************************
 *  文件：EventCenter.cs
 *  作者：子子芽
 *  日期：Created by DefaultCompany on 2022/3/17 19:14:45
 *  项目：Base Framework
 *  功能：事件中心，统一管理事件
 *  修改日志：Time / Revise
        观察者模式，
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public interface IEventInfo
{

}
public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> action;

    public EventInfo(UnityAction<T> action)
    {
        if (action != null)
            this.action += action;
    }
}

public class EventInfo : IEventInfo
{
    public UnityAction action;

    public EventInfo(UnityAction action)
    {
        if (action != null)
            this.action += action;
    }
}


public class EventCenter : BaseManager<EventCenter>
{
    private Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();

    public void AddEventListener<T>(string eventName, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventInfo<T>).action += action;
        }
        else
        {
            eventDic.Add(eventName, new EventInfo<T>(action));
        }
    }

    public void RemoveEventListener<T>(string eventName, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventInfo<T>).action -= action;
        }
        else
        {
            eventDic.Remove(eventName);
        }
    }

    public void EventTrigger<T>(string eventName, T info )
    {
        if (eventDic.ContainsKey(eventName))
        {
            if ((eventDic[eventName] as EventInfo<T>).action != null)
                (eventDic[eventName] as EventInfo<T>).action.Invoke(info);
        }
    }


    public void AddEventListener(string eventName, UnityAction action)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventInfo).action += action;
        }
        else
        {
            eventDic.Add(eventName, new EventInfo(action));
        }
    }

    public void RemoveEventListener(string eventName, UnityAction action)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventInfo).action -= action;
        }
        else
        {
            eventDic.Remove(eventName);
        }
    }

    public void EventTrigger(string eventName)
    {
        if (eventDic.ContainsKey(eventName))
        {
            if ((eventDic[eventName] as EventInfo).action != null)
                (eventDic[eventName] as EventInfo).action.Invoke();
        }
    }


    public void Clear()
    {
        eventDic.Clear();
    }

}
