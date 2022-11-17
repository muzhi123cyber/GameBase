/****************************************************
 *  文件：GameSceneMgr.cs
 *  作者：子子芽
 *  日期：Created by DefaultCompany on 2022/10/30 0:55:51
 *  项目：Gunshot_Timer
 *  功能：Function
 *  修改日志：Time / Revise

*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneMgr : SingletonAutoMono<GameSceneMgr>
{
    //主角
    public GameObject player;

    //场景
    public List<GameObject> maps;

    //敌人
    public List<GameObject> enemies;

    //陷阱
    public List<GameObject> traps;

    private void Awake()
    {
        //加载场景  
        //新游戏 

        //加载游戏

        //添加主角

    }








}
