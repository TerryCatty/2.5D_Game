using UnityEngine;
using Newtonsoft.Json.Linq;
using TMPro;
using System.Collections.Generic;
public class Localize : MonoBehaviour
{
    public virtual string GetLocalizeText(string json, string list, string key, Language language)
    {
        string result = "";

        JObject jsonObject = JObject.Parse(json);

        JArray arrayJson = (JArray)jsonObject[list];

        foreach (JObject itemJson in arrayJson)
        {
            if (itemJson["Key"].ToString().Replace(" ", "").ToLower() == key.Replace(" ", "").ToLower())
            {
                result = itemJson[language.ToString()].ToString();
                break;
            }
        }


        return result;
    }

    public virtual TMP_FontAsset GetFontAsset(int idFont)
    {
        List<TMP_FontAsset> list = LocalizationManager.instance.languages[LocalizationManager.instance.langIndex].fontsLanguage;

        if (list.Count == 0) return null;

        if(list.Count <= idFont)
        {
            return list[0];
        }
        else
        {
            return list[idFont];
        }
    }
}
