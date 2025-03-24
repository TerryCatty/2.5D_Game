using UnityEngine;
using TMPro;
using System.Linq;

public class TextLocalization : Localize
{
    private TextMeshProUGUI textLcz;

    public int idFont;

    public string key, list, folder;

    private void Start()
    {
        textLcz = GetComponent<TextMeshProUGUI>();
        CheckLanguage();
        LocalizationManager.instance.changeLang += CheckLanguage;
    }

    private void CheckLanguage()
    {
        string json = GetLocalizationFile(folder, list);
        textLcz.text = GetLocalizeText(json, list, key, GetCurrentLanguage());

        TMP_FontAsset fontAsset = GetFontAsset(idFont);

        if(fontAsset != null) 
            textLcz.font = fontAsset;
    }


    private void OnDisable()
    {
        LocalizationManager.instance.changeLang -= CheckLanguage;
    }
}
