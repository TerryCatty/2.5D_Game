using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionsController : MonoBehaviour
{
    [SerializeField] private List<PlayerAction> actionsList;


    private Button actionButton;

    private void Start()
    {
        actionButton = GameObject.Find("ActionButton").GetComponent<Button>();
        actionButton.onClick.AddListener(DoAction);

        actionButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (actionsList.Count > 0)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                DoAction();
            }

        }
    }

    public void AddAction(PlayerAction action)
    {
        if (actionsList.Contains(action))
        {
            RemoveAction(action);
        }

        actionsList.Add(action);
        if (GetComponent<Movement>().isAndroid) actionButton.gameObject.SetActive(true);
    }

    public void RemoveAction(PlayerAction action)
    {
        actionsList.Remove(action);

        if (actionsList.Count == 0) actionButton.gameObject.SetActive(false);
    }

    public void DoAction()
    {
        actionsList[0].DoAction();
    }
}


public class PlayerAction : MonoBehaviour
{
    public virtual void DoAction()
    {

    }
}