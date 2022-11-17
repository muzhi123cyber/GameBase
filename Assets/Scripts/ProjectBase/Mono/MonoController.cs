/****************************************************
 *  文件：MonoController#FILEEXTENSION#
 *  作者：子子芽
 *  日期：Created by #SMARTDEVELOPERS# on #CREATETIME#
 *  项目：#PROJECTNAME#
 *  功能：Function
 *  修改日志：Time / Revise

*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoController : SingletonAutoMono<MonoController>
{
    private event UnityAction updateAction;
    private void Update() {
        if(updateAction!= null)
        {
            updateAction.Invoke();
        }
    }

    public void AddUpdateListener(UnityAction fun)
    {
        updateAction += fun;
    }

    public void RemoveUpdateListener(UnityAction fun)
    {
       updateAction -= fun;
    }

}

