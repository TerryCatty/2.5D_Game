using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HelpManager : Localize
{
    [SerializeField] private GameObject tutorialWindow;

    private List<Image> imagesList;
    private List<float> imagesOpacity;
    private List<TextMeshProUGUI> textList;
    private List<float> textOpacity;

    public float speedOpacity;

    public TextMeshProUGUI objectTextTutorial;
    public TextMeshProUGUI textCount;
    public Image objectImageTutorial;

    public List<HelpStruct> helpList;
    public string folder;
    public string list;

    bool isOpacity;
    bool isOpacityNot;

    public int count;

    private void Start()
    {
        imagesList = new List<Image>();
        imagesOpacity = new List<float>();

        try
        {
            GameManager.instance.SetHitManager(this);
        }
        catch
        {
            Debug.Log("Not");
        }

        textList = new List<TextMeshProUGUI>();
        textOpacity = new List<float>();


        textCount.text = count.ToString();

        imagesList = tutorialWindow.GetComponentsInChildren<Image>().ToList();
        textList = tutorialWindow.GetComponentsInChildren<TextMeshProUGUI>().ToList();

        foreach (Image image in imagesList)
        {
            imagesOpacity.Add(image.color.a); 
        }


        foreach (TextMeshProUGUI text in textList)
        {
            textOpacity.Add(text.color.a);
        }

        tutorialWindow.SetActive(false);

        foreach(HelpStruct helpStruct in helpList)
        {
            helpStruct.text = GetLocalizeText(GetLocalizationFile(folder, list), list, helpStruct.key, GetCurrentLanguage());
        }
    }

    private void Update()
    {
        if (isOpacity == true)
        {
            int i = 0;

            foreach (Image image in imagesList)
            {
                if (image.color.a < imagesOpacity[i]) image.color = new Color(image.color.r, image.color.g, image.color.b,
                    image.color.a + Time.deltaTime * speedOpacity);
                i++;
            }

            i = 0;

            foreach (TextMeshProUGUI text in textList)
            {
                if (text.color.a < textOpacity[i]) text.color = new Color(text.color.r, text.color.g, text.color.b,
                    text.color.a + Time.deltaTime * speedOpacity);
                i++;
            }
        }
        if (isOpacityNot)
        {
            foreach (Image image in imagesList)
            {
                if (image.color.a > 0) image.color = new Color(image.color.r, image.color.g, image.color.b,
                    image.color.a - Time.deltaTime * speedOpacity);
            }

            foreach (TextMeshProUGUI text in textList)
            {
                if (text.color.a > 0) text.color = new Color(text.color.r, text.color.g, text.color.b,
                    text.color.a - Time.deltaTime * speedOpacity);
            }
        }


    }

    public void CloseHelp()
    {
        tutorialWindow.SetActive(false);
        Time.timeScale = 1;
    }

    public void OpenHelp()
    {
        Debug.Log("Help");

        if(count > 0)
            GameManager.instance.AdvHint();
        else
            OpenConfirm();
    }

    public void OpenConfirm()
    {
        if (count == 0)
        {
            count++;
        }

        count--;
        textCount.text = count.ToString();

        objectTextTutorial.font = GetFontAsset(0);

        objectTextTutorial.text = helpList[count].text;
        objectImageTutorial.sprite = helpList[count].image;

        tutorialWindow.SetActive(true);
        foreach (Image image in imagesList)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        }


        foreach (TextMeshProUGUI text in textList)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        }
        isOpacity = true;
        isOpacityNot = false;
    }
    public void ExitHelp()
    {
        isOpacity = false;
        isOpacityNot = true;

        Invoke(nameof(OffWindow), 0.1f);
    }

    public void OffWindow()
    {
        tutorialWindow.SetActive (false);
    }
}

[Serializable]
public class HelpStruct
{
    public string text;
    public Sprite image;
    public string key;
}
