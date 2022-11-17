/****************************************************
 *  文件：SettingPanel.cs
 *  作者：子子芽
 *  日期：Created by DefaultCompany on 2022/10/29 13:15:42
 *  项目：Gunshot_Timer
 *  功能：Function
 *  修改日志：Time / Revise

*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BasePanel
{
    private SettingData settingData;
    private float oldBKV;
    private float oldSV;

    protected override void Awake()
    {
        base.Awake();
        settingData = GameMgr.GetInstance().settingData;

        oldBKV = settingData.BKMusicVolume;
        oldSV = settingData.SoundVolume;
        MusicMgr.GetInstance().ChangeBkMusiceVolume(oldBKV);
        MusicMgr.GetInstance().ChangeSoundVolume(oldSV);

        GetControl<Slider>("MusicSlider").value = oldBKV;
        GetControl<Slider>("SoundSlider").value = oldSV;
    }

    public override void HideMe()
    {
        settingData.SaveSettingData(settingData);
    }

    protected override void OnClick(string objName)
    {
        switch (objName)
        {
            case "BKMusicBtn":
                //背景音乐
                ClickBKMusicBtn();
                break;
            case "SoundBtn":
                //音效设置
                ClickSoundBtn();
                break;
            case "CloseBtn":
                UIManager.GetInstance().ShowPanel<StartPanel>("StartPanel");
                UIManager.GetInstance().HidePanel<SettingPanel>("SettingPanel");
                break;
        }
    }

    protected override void OnValueChanged(string objName, float floatValue)
    {
        switch (objName)
        {
            case "MusicSlider":
                //背景音乐
                SetBKMusic(floatValue);
                break;
            case "SoundSlider":
                //音效设置
                SetSound(floatValue);
                break;
        }
    }



    private void ClickBKMusicBtn()
    {
        if (oldBKV != 0) //如果音效 本来大小不等于0的话，则直接变成0 再改变图标
        {
            ResMgr.GetInstance().LoadResAsyn<Sprite>("ArtRes/icon/icon_90_1_04", (sprite) =>
            {
                GetControl<Image>("BKMusicBtn").sprite = sprite;
            });
            GetControl<Slider>("MusicSlider").value = 0;
            settingData.ChangeBKMusicVolume(0);
            MusicMgr.GetInstance().ChangeBkMusiceVolume(0);
        }
        else
        {
            ResMgr.GetInstance().LoadResAsyn<Sprite>("ArtRes/icon/icon_90_1_03", (sprite) =>
            {
                GetControl<Image>("BKMusicBtn").sprite = sprite;
            });
            oldBKV = 1;
            GetControl<Slider>("MusicSlider").value = 1;
            settingData.ChangeBKMusicVolume(oldBKV);
            MusicMgr.GetInstance().ChangeBkMusiceVolume(oldBKV);
        }
    }

    private void ClickSoundBtn()
    {
        if (oldSV != 0)  //如果音效 本来大小不等于0的话，则直接变成0 再改变图标
        {
            ResMgr.GetInstance().LoadResAsyn<Sprite>("ArtRes/icon/icon_90_1_06", (sprite) =>
            {
                GetControl<Image>("SoundBtn").sprite = sprite;
            });
            GetControl<Slider>("SoundSlider").value = 0;
            settingData.ChangeSoundVolume(0);
            MusicMgr.GetInstance().ChangeSoundVolume(0);
        }
        else
        {
            ResMgr.GetInstance().LoadResAsyn<Sprite>("ArtRes/icon/icon_90_1_05", (sprite) =>
            {
                GetControl<Image>("SoundBtn").sprite = sprite;
            });
            oldSV = 1;
            GetControl<Slider>("SoundSlider").value = 1;
            settingData.ChangeSoundVolume(oldSV);
            MusicMgr.GetInstance().ChangeSoundVolume(oldSV);
        }
    }


    private void SetBKMusic(float fValue)
    {
        if (fValue == 0)  //如果音效 本来大小不等于0的话，则直接变成0 再改变图标
        {
            ResMgr.GetInstance().LoadResAsyn<Sprite>("ArtRes/icon/icon_90_1_04", (sprite) =>
            {
                GetControl<Image>("BKMusicBtn").sprite = sprite;
            });
            oldBKV = 0;
            settingData.ChangeBKMusicVolume(0);
            MusicMgr.GetInstance().ChangeBkMusiceVolume(0);
        }
        else
        {
            if (oldBKV == 0)
            {
                ResMgr.GetInstance().LoadResAsyn<Sprite>("ArtRes/icon/icon_90_1_03", (sprite) =>
                {
                    GetControl<Image>("BKMusicBtn").sprite = sprite;
                });
            }
            oldBKV = fValue;
            settingData.ChangeBKMusicVolume(oldBKV);
            MusicMgr.GetInstance().ChangeBkMusiceVolume(oldBKV);
        }
    }

    private void SetSound(float fValue)
    {
        if (fValue == 0)  //如果音效 本来大小不等于0的话，则直接变成0 再改变图标
        {
            ResMgr.GetInstance().LoadResAsyn<Sprite>("ArtRes/icon/icon_90_1_06", (sprite) =>
            {
                GetControl<Image>("SoundBtn").sprite = sprite;
            });
            oldSV = 0;
            settingData.ChangeSoundVolume(oldSV);
            MusicMgr.GetInstance().ChangeSoundVolume(oldSV);
        }
        else
        {
            if (oldSV == 0)
            {
                ResMgr.GetInstance().LoadResAsyn<Sprite>("ArtRes/icon/icon_90_1_05", (sprite) =>
                {
                    GetControl<Image>("SoundBtn").sprite = sprite;
                });
            }
            oldSV = fValue;
            settingData.ChangeSoundVolume(oldSV);
            MusicMgr.GetInstance().ChangeSoundVolume(oldSV);
        }
    }



}
