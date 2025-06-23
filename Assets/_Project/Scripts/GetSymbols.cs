using Newtonsoft.Json.Linq;
using System.Linq;
using UnityEngine;
using GamePush;

public class GetSymbols : Localize
{
    public Language language;
    [TextArea (minLines:10, maxLines: 30)]
    public string text;
    [TextArea(minLines: 10, maxLines: 30)]
    public string textOut;

    private void Start()
    {
        foreach(LocalizeFolders folder in LocalizationManager.instance.getFolders)
        {
            foreach(TextAsset json in folder.textList)
            {

                JObject jsonObject = JObject.Parse(json.ToString());
                JArray arrayJson = (JArray)jsonObject.GetItem(0).ToArray()[0];

                foreach (JObject itemJson in arrayJson)
                {
                    text = itemJson[language.ToString()].ToString(); 
                    for (int i = 0; i < text.Length; i++)
                    {
                        for (int j = 0; j < text.Length; j++)
                        {
                            if (textOut.Contains(text[i]) == false)
                            {
                                textOut += text[i];
                                break;
                            }
                        }
                    }
                }

                
            }
        }

        
    }
}
