using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialTrigger : Localize
{
    [SerializeField] private GameObject tutorialWindow;

    private List<Image> imagesList;
    private List<float> imagesOpacity;
    private List<TextMeshProUGUI> textList;
    private List<float> textOpacity;

    public float speedOpacity;
    public Sprite tutorialImage;
    public string textTutorial;

    public TextMeshProUGUI objectTextTutorial;
    public Image objectImageTutorial;

    Button closeButton;

    bool isOpacity;
    bool isOpacityNot;
    public string list, folder, mobileKey, desktopKey, key;

    private void Start()
    {


        imagesList = new List<Image>();
        imagesOpacity = new List<float>();

        textList = new List<TextMeshProUGUI>();
        textOpacity = new List<float>();

        closeButton = tutorialWindow.GetComponentsInChildren<Button>().ToList().Where(b => b.gameObject.name == "Exit").First();

        imagesList = tutorialWindow.GetComponentsInChildren<Image>().ToList();
        textList = tutorialWindow.GetComponentsInChildren<TextMeshProUGUI>().ToList();

        foreach (Image image in imagesList)
        {
            imagesOpacity.Add(image.color.a);
            //image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        }


        foreach (TextMeshProUGUI text in textList)
        {
            textOpacity.Add(text.color.a);
            //text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        }

        string json = LocalizationManager.instance.folders.First(folder => folder.name == this.folder).textList.First(text => text.name == list).text;
        string keyJson = GameManager.instance.isAndroid ? mobileKey : desktopKey;
        keyJson += key;
        textTutorial = GetLocalizeText(json, list, keyJson, LocalizationManager.instance.languages[LocalizationManager.instance.langIndex].language);

        tutorialWindow.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {


            objectTextTutorial.text = textTutorial;
            objectImageTutorial.sprite = tutorialImage;

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
            closeButton.onClick.AddListener(DestroyTrigger);

        }
    }

    private void Update()
    {
        if (isOpacity == true)
        {
            if (Input.GetKeyDown(KeyCode.E)) { DestroyTrigger(); }
            int i = 0;

            
            foreach (Image image in imagesList)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, imagesOpacity[i]);
                i++;
            }

            i = 0;

            foreach (TextMeshProUGUI text in textList)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, textOpacity[i]);
                i++;
            }

            Time.timeScale = 0;
        }
        if (isOpacityNot)
        {

            foreach (Image image in imagesList)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
            }

            foreach (TextMeshProUGUI text in textList)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
            }

            Time.timeScale = 1;
        }


    }

    public void DestroyTrigger()
    {
        closeButton.onClick.RemoveAllListeners();
        isOpacityNot = true; isOpacity = false; StartCoroutine(DeleteTrigger());
    }

    IEnumerator DeleteTrigger()
    {
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }

}
