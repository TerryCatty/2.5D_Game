using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePartsTrigger : MonoBehaviour
{
    public List<GameObject> gamePartsActive;
    public List<GameObject> gamePartsInactive;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            foreach(GameObject part in gamePartsActive)
            {
                part.SetActive(true);
            }

            foreach(GameObject part in gamePartsInactive)
            {
                part.SetActive(false);
            }
        }
    }
}
