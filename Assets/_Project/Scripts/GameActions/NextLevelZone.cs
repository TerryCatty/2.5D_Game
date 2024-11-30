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

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(nextLevel);
    }
}
