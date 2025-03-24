using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class LocalizationMenu : MonoBehaviour
{

    public LocalizationManager LocalizationManager;

    public int lang => LocalizationManager.instance.langIndex;

    int countLanguages = Enum.GetNames(typeof(Language)).Length;
    
    public List<Sprite> imagesLanguage = new List<Sprite>();
    public Image image;

    private void Start()
    {
        LocalizationManager = LocalizationManager.instance;
        LocalizationManager.changeLang += InitLanguageMenu;
        ChangeImage(lang);
    }

    public void InitLanguageMenu()
    {
        ChangeImage(lang);
    }

    public void NextLanguage()
    {
        int currentIndex = lang;

        if(currentIndex < countLanguages - 1)
        {
            currentIndex++;
        }
        else
        {
            currentIndex = 0;
        }

        LocalizationManager.ChangeLanguage(currentIndex);
        ChangeImage(currentIndex);
    }

    public void ChangeImage(int id)
    {
        Debug.Log(image.gameObject.name);
        Debug.Log(image.sprite);
        image.sprite = imagesLanguage[id];
    }

    private void OnDisable()
    {
        LocalizationManager.changeLang -= InitLanguageMenu;
    }
}
