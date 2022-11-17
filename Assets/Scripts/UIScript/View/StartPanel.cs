/****************************************************
 *  文件：StartPanel.cs
 *  作者：子子芽
 *  日期：Created by DefaultCompany on 2022/10/29 13:15:56
 *  项目：Gunshot_Timer
 *  功能：Function
 *  修改日志：Time / Revise

*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanel : BasePanel
{
    private CanvasGroup canvasGroup;
    private float alphaSpeed = 5;
    private float timer = 0;

    protected override void Awake()
    {
        base.Awake();
        canvasGroup = this.GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime * alphaSpeed;
            canvasGroup.alpha = timer;

            if (timer <= 0 )
            {
                timer = 0;
                canvasGroup.alpha = 0;
            }
        }
    }


    protected override void OnClick(string objName)
    {
        switch (objName)
        {
            case "StartBtn":
                GameMgr.GetInstance().EnterGameScene();  //开始游戏
                break;
            case "LoadGameBtn":
                GameMgr.GetInstance().LoadGameScene();  //加载游戏
                break;
            case "ExitBtn":
                GameMgr.GetInstance().ExitGame();       //退出游戏
                break;
            case "SetBtn":
                UIManager.GetInstance().ShowPanel<SettingPanel>("SettingPanel");
                UIManager.GetInstance().HidePanel<StartPanel>("StartPanel");
                break;
        }
    }

    public void AlphaHidePanel()
    {
        timer = 1;
    }
}
