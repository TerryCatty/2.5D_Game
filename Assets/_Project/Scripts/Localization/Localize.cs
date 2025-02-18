using UnityEngine;
using Newtonsoft.Json.Linq;
public class Localize : MonoBehaviour
{
    public virtual string GetLocalizeText(string json, string list, string key, Language language)
    {
        string result = "";

        JObject jsonObject = JObject.Parse(json);

        JArray arrayJson = (JArray)jsonObject[list];

        foreach (JObject itemJson in arrayJson)
        {
            if (itemJson["Key"].ToString() == key)
            {
                result = itemJson[language.ToString()].ToString();
                break;
            }
        }


        return result;
    }
}
