using UnityEngine;
using TMPro;
using System.Linq;

public class TextLocalization : Localize
{
    private TextMeshProUGUI textLcz;

    public string key, list, folder;

    private void Start()
    {
        textLcz = GetComponent<TextMeshProUGUI>();
        CheckLanguage();
    }

    private void CheckLanguage()
    {
        string json = GameManager.instance.folders.First(folder => folder.name == this.folder).textList.First(text => text.name == list).text;
        textLcz.text = GetLocalizeText(json, list, key, GameManager.instance.language);
    }
}
