using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionsController : MonoBehaviour
{
    [SerializeField] private List<PlayerAction> actionsList;



    private void Update()
    {
        if(actionsList.Count > 0)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                DoAction();
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

    public void DoAction()
    {
        actionsList[0].DoAction();
    }
}


public class PlayerAction: MonoBehaviour
{
    public virtual void DoAction()
    {

    }
}