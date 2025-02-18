using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelZone : MonoBehaviour
{
    public Animator fadeAnimator;
    public int nextLevel;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(NextLevel());
        }
    }


    private IEnumerator NextLevel()
    {
        fadeAnimator.Play("Fading");
        Debug.Log(nextLevel - 1);

        GameManager.instance.SetLevel(nextLevel - 1);
        GameManager.instance.SaveData();

        GameManager.instance.Adv();

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(nextLevel);
    }
}
