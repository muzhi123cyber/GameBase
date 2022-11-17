/****************************************************
 *  文件：GameStart.cs
 *  作者：子子芽
 *  日期：Created by DefaultCompany on 2022/10/29 17:14:14
 *  项目：Gunshot_Timer
 *  功能：Function
 *  修改日志：Time / Revise

*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    private void Awake()
    {
        GameMgr.GetInstance().EnterStartScene();
    }

}
