using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDoor : MonoBehaviour
{
    [SerializeField] private Transform rightDoor, leftDoor;
    private bool isOpen;


    public void OpenDoor()
    {
        isOpen = true;
        rightDoor.gameObject.SetActive(false);
        leftDoor.gameObject.SetActive(false);
    }
}
