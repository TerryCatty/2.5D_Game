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
        LocalizationManager.instance.changeLang += CheckLanguage;
        CheckLanguage();
    }

    private void CheckLanguage()
    {
        string json = LocalizationManager.instance.folders.First(folder => folder.name == this.folder).textList.First(text => text.name == list).text;
        textLcz.text = GetLocalizeText(json, list, key, LocalizationManager.instance.languages[LocalizationManager.instance.langIndex].language);

        TMP_FontAsset fontAsset = GetFontAsset(idFont);

        if(fontAsset != null) 
            textLcz.font = fontAsset;
    }


    private void OnDisable()
    {
        LocalizationManager.instance.changeLang -= CheckLanguage;
    }
}
