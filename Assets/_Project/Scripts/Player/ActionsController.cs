using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsController : MonoBehaviour
{
    [SerializeField] private List<PlayerAction> actionsList;

    private void Update()
    {
        if(actionsList.Count > 0)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                actionsList[0].DoAction();
            }

        }
    }

    public void AddAction(PlayerAction action)
    {
        if(actionsList.Contains(action))
        {
            RemoveAction(action);
        }

        actionsList.Add(action);
    }

    public void RemoveAction(PlayerAction action)
    {
        actionsList.Remove(action);
    }
}


public class PlayerAction: MonoBehaviour
{
    public virtual void DoAction()
    {

    }
}