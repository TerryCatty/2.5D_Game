using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;
using GamePush;

public class LocalizationManager : MonoBehaviour
{

    public static LocalizationManager instance;

    public int langIndex;

    public List<LanguageStruct> languages;

    public List<LocalizeFolders> folders;

    public List<LocalizeFolders> getFolders => folders;

    public Action changeLang;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            GP_Init.OnReady += LoadData;
        }

        else
        {
            Destroy(gameObject);
        }

    }


    public void ChangeLanguage(int language, bool changeInPlugin = true)
    {
        if (language >= languages.Count) language = 0;

        langIndex = language;
        changeLang?.Invoke();

        if (changeInPlugin)
        {
            GP_Language.Change(languages[langIndex].language);
            Debug.Log((languages[langIndex].language).ToString());
            Debug.Log(GP_Language.Current().ToString());
        }
    }



    public void LoadData()
    {
        string currentLanguage = "";

        currentLanguage =
        GP_Language.Current().ToString();

        Debug.Log(currentLanguage);


        foreach (LanguageStruct language in languages)
        {
            if(language.language.ToString().ToLower() == currentLanguage.ToLower())
            {
                langIndex = languages.IndexOf(language);
                break;
            }
        }
       ChangeLanguage(langIndex, false);
    }
}



[Serializable]
public struct LocalizeFolders
{
    public string name;
    public List<TextAsset> textList;
}
/*
[Serializable]
public enum Language
{
    Russian,
    English,
    German,
    Ukrainian,
    Japanese,
    Turkish,
    Chineese
}*/

[Serializable]
public struct LanguageStruct
{
    public Language language;
    public Sprite imageLanguage;
    public List<TMP_FontAsset> fontsLanguage;
}
