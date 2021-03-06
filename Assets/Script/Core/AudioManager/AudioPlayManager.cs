﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

    public class AudioPlayManager : MonoBehaviour
    {
        static Audio2DPlayer a2DPlayer;
        static Audio3DPlayer a3DPlayer;

    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        GameObject obj = new GameObject("[AudioManager]");
        AudioPlayManager audioManager = obj.AddComponent<AudioPlayManager>();
        DontDestroyOnLoad(obj);

        a2DPlayer = new Audio2DPlayer(audioManager);
        a3DPlayer = new Audio3DPlayer(audioManager);
        TotleVolume = RecordManager.GetFloatRecord("GameSettingData", "TotleVolume", 1f);
        MusicVolume = RecordManager.GetFloatRecord("GameSettingData", "MusicVolume", 1f);
        SFXVolume = RecordManager.GetFloatRecord("GameSettingData", "SFXVolume", 1f);
    }
    #region Volume
    private static float totleVolume = 1f;
    public static float TotleVolume
    {
        get { return totleVolume; }
        set
        {
            totleVolume = Mathf.Clamp01(value);
            SetMusicVolume();
            SetSFXVolume();

        }
    }

    public static float MusicVolume
    {
        get
        {
            return musicVolume;
        }

        set
        {
            musicVolume = Mathf.Clamp01(value);
            SetMusicVolume();
        }
    }

    public static float SFXVolume
    {
        get
        {
            return sfxVolume;
        }

        set
        {
            sfxVolume = Mathf.Clamp01(value);
            SetSFXVolume();
        }
    }

    private static float musicVolume = 1f;

    private static float sfxVolume = 1f;

    private static void SetMusicVolume()
    {
        a2DPlayer.SetMusicVolume(totleVolume * musicVolume);
        a3DPlayer.SetMusicVolume(totleVolume * musicVolume);
    }
    private static void SetSFXVolume()
    {
        a2DPlayer.SetSFXVolume(totleVolume * sfxVolume);
        a3DPlayer.SetSFXVolume(totleVolume * sfxVolume);
    }

    public static void SaveVolume()
    {
        RecordManager.SaveRecord("GameSettingData", "TotleVolume", TotleVolume);
        RecordManager.SaveRecord("GameSettingData", "MusicVolume", MusicVolume);
        RecordManager.SaveRecord("GameSettingData", "SFXVolume", SFXVolume);
    }
    #endregion


    public static void PlayMusic2D(string name, int channel, float volumeScale = 1, bool isLoop = true, float fadeTime = 0.5f)
        {
            a2DPlayer.PlayMusic(channel, name, isLoop, volumeScale, fadeTime);
        }
        public static void PauseMusic2D(int channel, bool isPause)
        {
            a2DPlayer.PauseMusic(channel, isPause);
        }
        public static void PauseMusicAll2D(bool isPause)
        {
            a2DPlayer.PauseMusicAll(isPause);
        }

        public static void StopMusic2D(int channel)
        {

            a2DPlayer.StopMusic(channel);
        }

        public static void StopMusicAll2D()
        {
            a2DPlayer.StopMusicAll();
        }

        public static void PlaySFX2D(string name, float volumeScale = 1f)
        {
            a2DPlayer.PlaySFX(name, volumeScale);
        }
        public static void PauseSFXAll2D(bool isPause)
        {
            a2DPlayer.PauseSFXAll(isPause);

        }

        public static void PlayMusic3D(GameObject owner, string audioName, int channel = 0, float delay = 0f, float volumeScale = 1, bool isLoop = true, float fadeTime = 0.5f)
        {
            a3DPlayer.PlayMusic(owner, audioName, channel, isLoop, volumeScale, delay, fadeTime);
        }
        public static void PauseMusic3D(GameObject owner, int channel, bool isPause)
        {
            a3DPlayer.PauseMusic(owner, channel, isPause);
        }
        public static void PauseMusicAll3D(bool isPause)
        {
            a3DPlayer.PauseMusicAll(isPause);
        }

        public static void StopMusic3D(GameObject owner, int channel)
        {
            a3DPlayer.StopMusic(owner, channel);

        }
        public static void StopMusicOneAll3D(GameObject owner)
        {
            a3DPlayer.StopMusicOneAll(owner);
        }
        public static void StopMusicAll3D()
        {
            a3DPlayer.StopMusicAll();
        }
        public static void ReleaseMusic3D(GameObject owner)
        {
            a3DPlayer.ReleaseMusic(owner);
        }
        public static void ReleaseMusicAll3D()
        {
            a3DPlayer.ReleaseMusicAll();
        }

        public static void PlaySFX3D(GameObject owner, string name, float delay = 0f, float volumeScale = 1f)
        {
            a3DPlayer.PlaySFX(owner, name, volumeScale, delay);
        }
        public static void PlaySFX3D(Vector3 position, string name, float delay = 0f, float volumeScale = 1)
        {
            a3DPlayer.PlaySFX(position, name, volumeScale, delay);
        }

        public static void PauseSFXAll3D(bool isPause)
        {
            a3DPlayer.PauseSFXAll(isPause);
        }
        public static void ReleaseSFX3D(GameObject owner)
        {
            a3DPlayer.ReleaseSFX(owner);
        }
        public static void ReleaseSFXAll3D()
        {
            a3DPlayer.ReleaseSFXAll();
        }
        float runTimeCount = 0;
        void Update()
        {
            if (runTimeCount <= 0)
            {
                runTimeCount = 4f;
                a3DPlayer.ClearDestroyObjectData();
                a2DPlayer.ClearMoreAudioAsset();
            }
            else
            {
                runTimeCount -= Time.unscaledDeltaTime;
            }
        }
    }

