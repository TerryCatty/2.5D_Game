using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public bool resetData;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Change");

            if (resetData) PlayerPrefs.DeleteAll();
            else
                other.gameObject.GetComponent<PlayerData>().Save();
        }
    }
}
