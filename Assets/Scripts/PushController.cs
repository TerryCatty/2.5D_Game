using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PushController : MonoBehaviour
{
    [Range(1, 20)]
    [SerializeField] private float pushPower;

    private GameObject pushingObject;
    private Movement movement;

    private float weightObject;
    private float weightPlayer;
    private bool isPushing;


    private Side side;

    private float defaultDistance;
    private float distance;
    [SerializeField] private float offset;

    private void Start()
    {
        movement = GetComponent<Movement>();
        weightPlayer = movement.weight;
        movement.isJump += Jump;
    }

    private void Update()
    {
        if(pushingObject != null)
        {
            CheckDistance();

            if (Input.GetKeyDown(KeyCode.E))
            {
                defaultDistance = distance;

                side = movement.CheckSide(pushingObject.transform);
                SetPush();
            }

            if (isPushing)
            {
                  RotateToObject();
            }

            
        }

    }

    private void RotateToObject()
    {
        switch (side)
        {
            case Side.up:

                movement.directionRotate.z = Mathf.Abs(movement.moveValues.x) * -1;
                movement.directionRotate.x =
                    movement.moveValues.z > 0 ? movement.moveValues.x * -1
                    : movement.moveValues.x;

                if (movement.directionRotate.x == 0) movement.directionRotate.z = -1;

                break;

            case Side.down:

                movement.directionRotate.z = Mathf.Abs(movement.moveValues.x);
                movement.directionRotate.x =
                    movement.moveValues.z < 0 ? movement.moveValues.x * -1
                    : movement.moveValues.x;


                if (movement.directionRotate.x == 0) movement.directionRotate.z = 1;

                break;

            case Side.left:

                movement.directionRotate.x = Mathf.Abs(movement.moveValues.z);
                movement.directionRotate.z =
                    movement.moveValues.x < 0 ? movement.moveValues.z * -1
                    : movement.moveValues.z;


                if (movement.directionRotate.z == 0) movement.directionRotate.x = 1;

                break;

            case Side.right:

                movement.directionRotate.x = Mathf.Abs(movement.moveValues.x) * -1;
                movement.directionRotate.z =
                    movement.moveValues.x > 0 ? movement.moveValues.z * -1
                    : movement.moveValues.z;

                if (movement.directionRotate.z == 0) movement.directionRotate.x = -1;

                break;
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Push")
        {
            pushingObject = other.gameObject;
            weightObject = pushingObject.GetComponent<Rigidbody>().mass;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Push")
        {
            if (isPushing || other.gameObject != pushingObject) return;
            //SetPush();
        }
    }


    private void SetPush()
    {
        isPushing = !isPushing;
        movement.weight = isPushing ? weightObject : weightPlayer;
        movement.isRotatingByMovement = !isPushing;

        pushingObject.transform.SetParent(isPushing ? transform : null, true);

    }


    private void CheckDistance()
    {
        distance = Vector3.Distance(transform.position, pushingObject.transform.position);


        if (!isPushing) return;

        if(distance > defaultDistance + offset)
        {
            SetPush();
            pushingObject = null;
        }
    }


    private void Jump()
    {
        if (isPushing) SetPush();
    }
    
}


public enum Side
{
    left,
    right,
    up,
    down
}
