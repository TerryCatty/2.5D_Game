using System;
using System.Collections.Generic;
using UnityEngine;

public class CodeButton : MonoBehaviour
{
    private List<Rigidbody> rbs = new List<Rigidbody>();
    private Vector3 startPosition;
    public Transform pressedPosition;
    public Transform endPosition;

    public float moveUP_Force;
    public float multiplierUp;
    public float moveDown_Force;

    private bool isDown;

    private bool isPressed;

    public event Action pressedActions; 

    private void Start()
    {
        startPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out Rigidbody rb))
        {
            if(collision.gameObject.transform.position.y > transform.position.y)
            {
                rbs.Add(rb);
                moveDown_Force += rb.mass;
                isDown = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Rigidbody rb))
        {
            if (collision.gameObject.transform.position.y > transform.position.y)
            {
                rbs.Remove(rb);

                moveDown_Force -= rb.mass;

                isDown = false;
            }
        }
    }

    private void Update()
    {
        if(transform.position.y < startPosition.y && isDown == false)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * (moveUP_Force - moveDown_Force) / multiplierUp, transform.position.z);
        }
        else if (transform.position.y > endPosition.position.y && isDown == true && moveUP_Force <= moveDown_Force)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * (moveDown_Force - moveUP_Force), transform.position.z);
        }

        if(transform.position.y <= pressedPosition.position.y && isPressed == false)
        {
            isPressed = true;
            pressedActions?.Invoke();
        }

        if (transform.position.y > pressedPosition.position.y && isPressed == true)
        {
            isPressed = false;
        }
    }
}
