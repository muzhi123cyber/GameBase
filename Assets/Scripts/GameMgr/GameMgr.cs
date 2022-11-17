/****************************************************
 *  文件：GameSceneMgr.cs
 *  作者：子子芽
 *  日期：Created by DefaultCompany on 2022/10/30 0:8:50
 *  项目：Gunshot_Timer
 *  功能：Function
 *  修改日志：Time / Revise

*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameMgr : BaseManager<GameMgr>
{
    public SettingData settingData;
    public void Init()
    {
        //EventCenter.GetInstance().AddEventListener("进入开始场景", EnterStartScene);
        //EventCenter.GetInstance().AddEventListener("进入游戏场景", EnterGameScene);
        //EventCenter.GetInstance().AddEventListener("加载游戏场景", LoadGameScene);
    }


    public void EnterStartScene()
    {
        settingData = JsonDataMgr.GetInstance().LoadData<SettingData>("SettingData");
        UIManager.GetInstance().ShowPanel<BKPanel>("BKPanel");
        UIManager.GetInstance().ShowPanel<StartPanel>("StartPanel");

        MusicMgr.GetInstance().PlayBkMusic("BGMusic");
        MusicMgr.GetInstance().ChangeBkMusiceVolume(settingData.BKMusicVolume);
        MusicMgr.GetInstance().ChangeSoundVolume(settingData.SoundVolume);
    }


    public void EnterGameScene()
    {
        UIManager.GetInstance().GetPanel<StartPanel>().AlphaHidePanel();
        UIManager.GetInstance().ShowPanel<LoginPanel>("LoginPanel");

        //进入游戏场景 
        SceneMgr.GetInstance().LoadSceneAsyn("GameScene", () => {
            UIManager.GetInstance().HidePanel<BKPanel>("BKPanel");
            UIManager.GetInstance().HidePanel<StartPanel>("StartPanel");
            //UIManager.GetInstance().HidePanel<LoginPanel>("LoginPanel");
        });
    }


    public void LoadGameScene()
    {
        //加载游戏，读取_数据_游戏场景的第几个房间。
        UIManager.GetInstance().GetPanel<StartPanel>().AlphaHidePanel();
        //UIManager.GetInstance().ShowPanel<LoginPanel>("LoginPanel");

        //进入游戏场景 
        SceneMgr.GetInstance().LoadSceneAsyn("GameScene", () => {
            UIManager.GetInstance().HidePanel<BKPanel>("BKPanel");
            UIManager.GetInstance().HidePanel<StartPanel>("StartPanel");
            //UIManager.GetInstance().HidePanel<LoginPanel>("LoginPanel");
        });
    }


    public void CLearUIPanel()
    {
        UIManager.GetInstance().ClearAllPanel();
    }


    public void ExitGame()
    {
        //预处理
#if UNITY_EDITOR    //在编辑器模式下
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


}


