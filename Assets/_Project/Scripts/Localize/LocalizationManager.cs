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

    public Action changeLang;
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
        LoadData();
    }

    public void ChangeLanguage(int language)
    {
        langIndex = language;
        changeLang?.Invoke();
        GP_Language.Change((GamePush.Language)languages[langIndex].language);
    }



    public void LoadData()
    {
       
        string currentLanguage =
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
       ChangeLanguage(langIndex);
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
    German,
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
