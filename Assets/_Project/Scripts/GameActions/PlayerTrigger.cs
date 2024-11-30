using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public MovementParams parameters;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter " + other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Change");
            other.gameObject.GetComponent<Movement>().SetNewParameters(parameters);
        }
    }
}

[System.Serializable]
public struct MovementParams
{
    public float speed;
    public Vector3 moveValuesOffset;
    
}
