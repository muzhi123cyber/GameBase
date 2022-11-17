/****************************************************
 *  文件：LoginSlider.cs
 *  作者：子子芽
 *  日期：Created by DefaultCompany on 2022/10/29 21:9:28
 *  项目：Gunshot_Timer
 *  功能：Function
 *  修改日志：Time / Revise

*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class LoginPanel : BasePanel
{
    protected override void Awake()
    {
        base.Awake();
        EventCenter.GetInstance().AddEventListener<float>("进度条加载事件", LoginFunc);
    }


    private void OnDestroy()
    {
        EventCenter.GetInstance().RemoveEventListener<float>("进度条加载事件", LoginFunc);
    }


    /// <summary>
    /// 进度条加载事件
    /// </summary>
    /// <param name="progressValue"> 传入进度条加载度 </param>
    public void LoginFunc(float progressValue)
    {
        GetControl<Slider>("LoginSlider").value = progressValue;
    }



}
