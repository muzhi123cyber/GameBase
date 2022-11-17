using System;
using System.Xml.Serialization;
/****************************************************
 *  文件：MusicMgr.cs
 *  作者：子子芽
 *  日期：Created by DefaultCompany on 2022/3/22 3:6:33
 *  项目：Base Framework
 *  功能：Function
 *  修改日志：Time / Revise

*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MusicMgr : BaseManager<MusicMgr>
{
    public AudioSource bkMusic = null;
    public float bkMusicVolume = 1;

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="bkMusicName"></param>
    public void PlayBkMusic(string bkMusicName)
    {
        if(bkMusic == null)
        {
            GameObject obj = new GameObject("BkMusic");
            bkMusic = obj.AddComponent<AudioSource>();
            bkMusic.loop = true;
            GameObject.DontDestroyOnLoad(obj);
        }

        ResMgr.GetInstance().LoadResAsyn<AudioClip>("Music/BKMusic/"+ bkMusicName,(clip)=>{
            bkMusic.clip = clip;
            bkMusic.volume = bkMusicVolume;
            bkMusic.Play();
        });
    }

    /// <summary>
    /// 暂停背景音乐
    /// </summary>
    public void PauseBkMusic()
    {
        if(bkMusic == null)
            return;
        bkMusic.Pause();
    }

    /// <summary>
    /// 停止背景音乐
    /// </summary>
    public void StopBkMusic()
    {
        if(bkMusic == null)
            return;
        bkMusic.Stop();
    }

    /// <summary>
    /// 改变背景音乐大小
    /// </summary>
    /// <param name="volume">音量</param>
    public void ChangeBkMusiceVolume(float volume)
    {
        bkMusicVolume = volume;
         if(bkMusic == null)
            return;
        bkMusic.volume = bkMusicVolume;
    }



    //负责存储所有播放AudioSource的组件
    public GameObject soundObj = null;

    public List<AudioSource> sounds = new List<AudioSource>();

    public float soundVolume = 1;

    public MusicMgr()
    {
        MonoMgr.GetInstance().AddUpdateListener(Update);
    }

    //负责检测音效组件是否已经播放完毕，播放完之后删除组件移除存储
    private void Update()
    {
        for(int i = sounds.Count - 1 ; i>=0 ; --i)   //倒着遍历组件，在移除时不会引起序号的变化
        {
           if(!sounds[i].isPlaying)
           {
                GameObject.Destroy(sounds[i]);
                sounds.RemoveAt(i);
           }
        }
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="soundName">音效名</param>
    /// <param name="isLoop">音效是否循环播放</param>
    /// <param name="callback">回调函数 传递AudioSource</param>
    public void PlaySound(string soundName,bool isLoop,UnityAction<AudioSource> callback = null)
    {
        if(soundObj == null)
            soundObj = new GameObject("Sound");
        ResMgr.GetInstance().LoadResAsyn<AudioClip>("Music/Sound/"+ soundName,(clip)=>{
            AudioSource source = soundObj.AddComponent<AudioSource>();
            source.clip = clip;
            source.loop = isLoop;
            source.volume = soundVolume;
            source.Play();
            sounds.Add(source);
            callback?.Invoke(source);
        });
    }

    /// <summary>
    /// 停止音效
    /// </summary>
    /// <param name="source">停止那个组件的音效</param>
    public void StopSound(AudioSource source)
    {
        if(sounds.Contains(source))
        {
            source.Stop();
            sounds.Remove(source);
            GameObject.Destroy(source);
        }
    }

    /// <summary>
    /// 改变音效大小
    /// </summary>
    /// <param name="volume"></param>
    public void ChangeSoundVolume(float volume)
    {
        soundVolume = volume;
        for(int i = 0;i<sounds.Count;i++)   //改变当前播放的音效大小
        {
            sounds[i].volume = soundVolume;
        }
    }

}
