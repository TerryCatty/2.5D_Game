using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class NextLevelPanel : MonoBehaviour
{
    public Animator fadeAnimator;
    public int nextLevel;

    public void Init(int level, Animator fade)
    {
        nextLevel = level;
        fadeAnimator = fade;
        fadeAnimator.transform.SetParent(transform);
        Time.timeScale = 0f;

        GameManager.instance.SetPause(true);
    }

    public void Next()
    {
        Time.timeScale = 1f;
        StartCoroutine(NextLevel());
    }


    private IEnumerator NextLevel()
    {
        fadeAnimator.Play("Fading");

        GameManager.instance.SetLevel(nextLevel - 1);
        GameManager.instance.SaveData();

        yield return new WaitForSeconds(0);
        GameManager.instance.SetPause(false);

        GameManager.instance.LoadLevelWithAdv(nextLevel);
    }
}
