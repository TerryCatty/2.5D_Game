using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClockAction : GameAction
{
    [SerializeField] private Clock clock;
    [SerializeField] private int hours;
    [SerializeField] private int minutes;
    [SerializeField] private int offset;

    protected override void CheckCompleteTask()
    {
        if(clock.hours == hours && Mathf.Abs(clock.minutes - minutes) <= offset)
        {
            isComplete = true;

            StartCoroutine(StartCheckAction());
        }
    }

    IEnumerator StartCheckAction()
    {
        yield return new WaitForSeconds(1f);

        if (clock.hours == hours && Mathf.Abs(clock.minutes - minutes) <= offset)
        {
            m_OnComplete?.Invoke();
        }
        else isComplete = false;
    }
}
