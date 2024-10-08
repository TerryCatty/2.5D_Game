using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    public int weight;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody>())
        {
            weight += (int)collision.gameObject.GetComponent<Rigidbody>().mass;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody>())
        {
            weight -= (int)collision.gameObject.GetComponent<Rigidbody>().mass;
        }
    }
}
