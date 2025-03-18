using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;

public class LocalizationManager : MonoBehaviour
{

    public static LocalizationManager instance;

    public int langIndex;

    public List<LanguageStruct> languages;

    public List<LocalizeFolders> folders;

    public Action changeLang;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            LoadData();
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }

    }

    public void ChangeLanguage(int language)
    {
        if (language == langIndex) return;

        langIndex = language;
        changeLang?.Invoke();

        SaveData();
    }


    private void SaveData()
    {
        PlayerPrefs.SetInt("language", langIndex);
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        if(PlayerPrefs.HasKey("language"))
            langIndex = PlayerPrefs.GetInt("language");


    }
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
    English,
    Deutch,
    Ukrainian,
    Japan,
    Turkish,
    Chinese
}

[Serializable]
public struct LanguageStruct
{
    public Language language;
    public Sprite imageLanguage;
    public List<TMP_FontAsset> fontsLanguage;
}
