
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class VisualNovelle : MonoBehaviour
{
    public GameObject panel;
    public GameObject prevButton;
    public Animator fading;
    public TextMeshProUGUI text;
    public Image image;

    public GameObject spawnObject;
    public GameObject spawnObject2;
    public bool autoStart;

    private int count;

    public List<NovellPart> parts;
    public bool isPause;

    private void Start()
    {
        if (autoStart)
        {
            OpenPanel();
        }
    }

    public void OpenPanel()
    {
        if (isPause)
        {
            FindAnyObjectByType<EnemyAttackSystem>().isActive = false;
            FindAnyObjectByType<AttackSystem>().isActive2 = false;
        }

        panel.SetActive(true);

        try
        {
            FindAnyObjectByType<Movement>().isActive = false;
        }
        catch
        {

        }

        if (parts[count].text.Length > 0)
        {
            string firstChar = parts[count].text[0].ToString();
            text.text = "<color=red><b><i><size=120%>" + firstChar + "</b></i></size></color>" + parts[count].text.Remove(0, 1);
        }
        else
        {
            text.text = "";
        }
        image.sprite = parts[count].image;
    }

    public void Next()
    {
        StartCoroutine(NextSlide());

    }

    public void Previous()
    {
        StartCoroutine(PrevSlide());

    }

    IEnumerator NextSlide()
    {
        fading.Play("Fading");
        yield return new WaitForSeconds(1);


        if (count < parts.Count - 1)
        {
            count++;
            if (parts[count].text.Length > 0)
            {
                string firstChar = parts[count].text[0].ToString();
                text.text = "<color=red><b><i><size=120%>" + firstChar + "</b></i></size></color>" + parts[count].text.Remove(0, 1);
            }
            else
            {
                text.text = "";
            }

            image.sprite = parts[count].image;
            fading.Play("FadeOff");
        }
        else
        {
            panel.SetActive(false); try
            {
                FindAnyObjectByType<Movement>().isActive = true;
            }
            catch
            {

            }
            if (spawnObject2 != null)
            {
                spawnObject2.SetActive(true);
            }
            fading.Play("FadeOff");
            yield return new WaitForSeconds(1);

            if (spawnObject != null)
            {
                spawnObject.SetActive(true);
            }

            if (isPause)
            {
                FindAnyObjectByType<EnemyAttackSystem>().isActive = true;
                FindAnyObjectByType<AttackSystem>().isActive2 = true;
            }

        }
        prevButton.SetActive(count > 0);
    }

    IEnumerator PrevSlide()
    {
        fading.Play("Fading");
        yield return new WaitForSeconds(1);

        if (count > 0)
        {
            count--;
            if (parts[count].text.Length > 0)
            {
                string firstChar = parts[count].text[0].ToString();
                text.text = "<color=red><b><i><size=120%>" + firstChar + "</b></i></size></color>" + parts[count].text.Remove(0, 1);
            }
            else
            {
                text.text = "";
            }

            image.sprite = parts[count].image;
            fading.Play("FadeOff");
        }
        prevButton.SetActive(count > 0);
    }


}


[Serializable]
public struct NovellPart
{
    public string text;
    public Sprite image;
}