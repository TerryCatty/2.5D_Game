using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelZone : MonoBehaviour
{
    public Animator fadeAnimator;
    public int nextLevel;

    public NextLevelPanel nextLevelPanel;

    Canvas canvas;

    private void Start()
    {
        canvas = FindAnyObjectByType<Canvas>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            nextLevelPanel = Instantiate(nextLevelPanel);
            nextLevelPanel.transform.SetParent(canvas.transform, false);
            nextLevelPanel.Init(nextLevel, fadeAnimator);
        }
    }


}
