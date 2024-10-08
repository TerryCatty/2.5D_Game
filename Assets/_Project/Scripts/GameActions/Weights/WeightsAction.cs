using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeightsAction : GameAction
{
    [SerializeField] private Weight weights;

    [SerializeField] private float offsetToBowl;
    [SerializeField] private int indexBowl;

    protected override void CheckCompleteTask()
    {
        isComplete = weights.bowls[indexBowl].weight - weights.bowls.Single(x => weights.bowls.IndexOf(x) != indexBowl).weight == offsetToBowl;

        if(isComplete) m_OnComplete.Invoke();
    }

    public void DDD()
    {
        Debug.Log("Complete");
    }
}
