/****************************************************
 *  文件：SoundData.cs
 *  作者：子子芽
 *  日期：Created by DefaultCompany on 2022/10/29 13:18:27
 *  项目：Gunshot_Timer
 *  功能：Function
 *  修改日志：Time / Revise

*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 游戏设置  相关数据
/// </summary>
public class SettingData
{
    public float BKMusicVolume = 1;
    public float SoundVolume = 1;

    public SettingData()
    { 

    }


    /// <summary>
    /// 改变背景音乐大小
    /// </summary>
    /// <param name="volueme">音量</param>
    public void ChangeBKMusicVolume(float volueme)
    {
        BKMusicVolume = Mathf.Clamp(volueme, 0, 1);
    }

    /// <summary>
    /// 改变音效大小
    /// </summary>
    /// <param name="volueme">音量</param>
    public void ChangeSoundVolume(float volueme)
    {
        SoundVolume = Mathf.Clamp(volueme, 0, 1);
    }

    public void LoadSettingData()
    {
        SettingData temp = JsonDataMgr.GetInstance().LoadData<SettingData>("SettingData");
        BKMusicVolume = temp.BKMusicVolume;
        SoundVolume = temp.SoundVolume;
    }

    public void SaveSettingData(SettingData sd)
    {
        JsonDataMgr.GetInstance().SaveData(sd, "SettingData");
    }

}
