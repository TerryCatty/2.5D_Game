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
    }

    public void Next()
    {
        Time.timeScale = 1f;
        StartCoroutine(NextLevel());
    }


    private IEnumerator NextLevel()
    {
        fadeAnimator.Play("Fading");
        Debug.Log(nextLevel - 1);

        GameManager.instance.SetLevel(nextLevel - 1);
        GameManager.instance.SaveData();

        yield return new WaitForSeconds(1);

        GameManager.instance.LoadLevelWithAdv(nextLevel);
    }
}
