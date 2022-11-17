/****************************************************
 *  文件：InputMgr.cs
 *  作者：子子芽
 *  日期：Created by DefaultCompany on 2022/3/21 16:36:10
 *  项目：Base Framework
 *  功能：输入控制模块
 *  修改日志：Time / Revise
        添加对应的输入控制事件 进行调用
        管理Input PC键鼠输入 相关逻辑
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class InputMgr : BaseManager<InputMgr>
{
    private bool isCheck = true;
    public InputMgr()
    {
        MonoMgr.GetInstance().AddUpdateListener(MyUpdate); //添加帧更新事件进行检测
    }

    public void MyUpdate()
    {
        if(!isCheck)
            return;
        CheckKeyCode(KeyCode.W);
        CheckKeyCode(KeyCode.A);
        CheckKeyCode(KeyCode.S);
        CheckKeyCode(KeyCode.D);
    }

    /// <summary>
    /// 分发按键检测
    /// </summary>
    /// <param name="key"></param>
    public void CheckKeyCode(KeyCode key)
    {
        if(Input.GetKeyDown(key))
            EventCenter.GetInstance().EventTrigger<KeyCode>("某键按下",key);
        if(Input.GetKeyUp(key))
            EventCenter.GetInstance().EventTrigger<KeyCode>("某键抬起",key);
    }

    /// <summary>
    /// 是否开启输入检测
    /// </summary>
    /// <param name="isCheck"></param>
    public void StartOrEndCheck(bool isCheck)
    {
        this.isCheck = isCheck;
    }
}
