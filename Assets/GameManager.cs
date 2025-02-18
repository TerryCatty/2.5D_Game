using UnityEngine;
using System;
using System.Collections.Generic;

using YandexMobileAds;
using YandexMobileAds.Base;

[Serializable]
public struct GameData
{
    public int levels;
    public float musicLevel;
    public bool isMusic;
}

[Serializable]
public struct LocalizeFolders
{
    public string name;
    public List<TextAsset> textList;

}

[Serializable]
public enum Language
{
    Russian,
    English
}
public class GameManager : MonoBehaviour
{
   public GameData data;

    public static GameManager instance;

    private HelpManager hintManager;

    public bool isAndroid;
    public bool showAdv;

    public AdsInterstitial ads;
    public RewarderAd adsReward;

    public Language language;

    public List<LocalizeFolders> folders;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        Load();

        if(ads != null && showAdv)
            ads.RequestInterstitial();
        if (adsReward != null && showAdv)
            adsReward.RequestRewardedAd();
    }

    public void Load()
    {

        try
        {

            LoadData(PlayerPrefs.GetString("GameData"));
            //Загрузка
        }
        catch
        {
            LoadData("");
        }
    }

    public void SetHitManager(HelpManager manager)
    {
        hintManager = manager;
    }


    public void SetLevel(int level)
    {
        if(level > data.levels)
        {
            data.levels = level;
        }
    }

    public void Adv()
    {
        try
        {
           if(showAdv) ads.ShowInterstitial();
           //Показать рекламу
        }
        catch
        {

        }
    }

    public void SetMusicLevel(float value)
    {
        data.musicLevel = value;
    }
    public void SetIsMusic(bool value)
    {
        data.isMusic = value;
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(data);
        Debug.Log("Save");

        try
        {
            PlayerPrefs.SetString("GameData", json);
            PlayerPrefs.Save();
        }
        catch
        {
            Debug.Log("error in save");
        }
    }

    public void LoadData(string value)
    {
        if(value != "")
            data = JsonUtility.FromJson<GameData>(value);

        data.musicLevel = 0f;
        data.isMusic = true;
        Debug.Log("Load");
    }

    public void AdvHint()
    {
        try
        {
            if (showAdv)
            {
                adsReward.action += AdvIsShowHint;
                adsReward.ShowRewardedAd();
            }
            else
            {
                AdvIsShowHint();
            }
        }
        catch
        {
            AdvIsShowHint();
        }
    }

    public void AdvIsShowHint()
    {
        hintManager.OpenConfirm();

        if(adsReward != null && showAdv)
            adsReward.RequestRewardedAd();
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();

        bool isMusic = data.isMusic;
        float volume = data.musicLevel;
        data = new GameData();

        data.isMusic = isMusic;
        data.musicLevel = volume;

        SaveData();
    }

}
