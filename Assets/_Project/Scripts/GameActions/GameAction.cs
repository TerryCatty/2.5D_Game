using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameAction : MonoBehaviour
{
    [SerializeField]
    protected UnityEvent m_OnComplete = new UnityEvent();

    protected bool isComplete;

    private void Update()
    {
        if (isComplete) return;

        CheckCompleteTask();
    }

    protected virtual void CheckCompleteTask()
    {

    }
}
