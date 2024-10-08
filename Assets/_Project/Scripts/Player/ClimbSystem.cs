using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ClimbSystem : MonoBehaviour
{
    [Header("StairClimb")]
    [SerializeField] private float speedClimb;
    [SerializeField] private bool isStairsUp;
    private Transform stairs;


    [Space]

    [Header("LedgeClimb")]

    [SerializeField] private Transform ledgeTracker;
    [SerializeField] private float distanceTrackerLedge;

    [SerializeField] private bool isLedgeUp;
    private Transform ledge;

    public LayerMask legdeMask;

    bool isMovingStairs;
    bool isMovingLedge;

    private Movement movement;
    private Side side;

    [SerializeField] private float timerAction;
    private float timer;

    private bool canMove = true;
    private bool isClimbLedge = false;

    private void Start()
    {
        movement = GetComponent<Movement>();

        movement.isJump += Jump;
    }

    private void Update()
    {
        CheckLedge();

        if (isClimbLedge && movement.isGround == false)
        {
            ClimbLedgeAnim();
        }
        else if(isClimbLedge && movement.isGround)
        {
            ResetClimb();
        }

        if (!canMove) return;

        ClimbLogic();

    }

    private void ClimbLogic()
    {
        if (timer >= -1)
        {
            timer -= Time.deltaTime;
        }

        if (isStairsUp || isLedgeUp)
        {
            if (stairs != null)
            {
                side = movement.CheckSide(stairs);
            }
            

            if (movement.isGround)
            {
                if (!isMovingStairs)
                {
                    if (side == Side.left && movement.moveValues.x > 0)
                    {
                        StartClimbStairs();
                    }
                    else if (side == Side.right && movement.moveValues.x < 0)
                    {
                        StartClimbStairs();
                    }
                    else if (side == Side.down && movement.moveValues.z > 0)
                    {
                        StartClimbStairs();
                    }

                }
            }
            else{
                if(timer <= -1)
                {
                    if(isStairsUp && !isLedgeUp && !isMovingStairs)
                    {
                        StartClimbStairs();
                    }
                    else if (isLedgeUp && !isStairsUp && !isMovingLedge)
                    {
                        StartClimbLedge();
                    }
                }
            }
        }


        if (isMovingLedge)
        {
            MoveLedge();
        }

        if (isMovingStairs)
        {
            MoveStairs();
        }
    }

    private void StartClimbLedge()
    {
        isMovingLedge = true;
        movement.isSelfMoving = false;
        movement.isRotatingByMovement = false;
        movement.canJump = true;

        timer = timerAction;
    }

    private void MoveLedge()
    {
        if(ledge == null)
        {
            isMovingLedge = false;
            isLedgeUp = false;
            return;
        }

        if (timer >= -1)
        {
            timer -= Time.deltaTime;
        }

        movement.directionRotate = Vector3.zero;
        movement.playerValues = Vector3.zero;


        float moveVertical = Input.GetAxis("Vertical") * speedClimb;

        if(moveVertical > 0)
        {
            side = CheckSideByRotate(ledge);
            canMove = false;
            isClimbLedge = true;
        }
    }

    private void ClimbLedgeAnim()
    {

        if (ledge == null)
        {
            ResetClimb();
            return;
        }

        if (transform.position.y > ledge.position.y)
        {
            ResetClimb();
        }

        if (transform.position.y < ledge.position.y)
        {
            movement.playerValues.y = speedClimb;
        }

        if (side == Side.left)
        {
            movement.playerValues.x = speedClimb;
        }
        else if (side == Side.right)
        {
            movement.playerValues.x = -speedClimb;
        }
        else if (side == Side.down)
        {
            movement.playerValues.z = speedClimb;
        }
    }

    private void CheckLedge()
    {
        RaycastHit hit;
        if (Physics.Raycast(ledgeTracker.position, ledgeTracker.TransformDirection(Vector3.forward), out hit, distanceTrackerLedge, legdeMask))
        {
            isLedgeUp = true;
            ledge = hit.collider.transform;
            Debug.Log(hit.collider.gameObject.name);
        }
        else
        {
            isLedgeUp = false;
            ledge = null;
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(ledgeTracker.position, ledgeTracker.TransformDirection(Vector3.forward) * distanceTrackerLedge, Color.yellow);
    }

    private void StartClimbStairs()
    {
        isMovingStairs = true;
        movement.isSelfMoving = false;
        movement.isRotatingByMovement = false;
        movement.canJump = true;

        timer = timerAction;
    }

    private void MoveStairs()
    {

        if (timer >= -1)
        {
            timer -= Time.deltaTime;
        }

        movement.directionRotate = Vector3.zero;
        movement.playerValues = Vector3.zero;


        float moveVertical = Input.GetAxis("Vertical") * speedClimb;

        if (isLedgeUp)
        {
            canMove = false;
            isClimbLedge = true;
            return;
        }

        movement.isRotatingByMovement = movement.isGround;


        if (movement.isGround)
        {
            if (isStairsUp == false) ResetClimb();

            float moveHorizontal = movement.moveValues.x;


            if (side == Side.left)
            {
                if (moveHorizontal < 0) movement.playerValues.x = moveHorizontal;
                else if (moveHorizontal > 0) movement.playerValues.y = moveHorizontal;
                movement.playerValues.z = moveVertical;

            }
            else if (side == Side.right)
            {
                if (moveHorizontal > 0) movement.playerValues.x = moveHorizontal;
                else if (moveHorizontal < 0) movement.playerValues.y = -moveHorizontal;
                movement.playerValues.z = moveVertical;
            }
            else if (side == Side.down)
            {
                if (moveVertical > 0) movement.playerValues.y = moveVertical;
                else if (moveVertical < 0) movement.playerValues.z = moveVertical;
                movement.playerValues.x = moveHorizontal;
            } 

        }
        else
        {
            if (isStairsUp == false && timer <= 0) ResetClimb();

            movement.playerValues = new Vector3(0, moveVertical);
            float moveHorizontal = movement.moveValues.x;

            if (side == Side.left)
            {
                if (moveHorizontal > 0) movement.playerValues.y = moveHorizontal;
            }
            else if (side == Side.right)
            {
                if (moveHorizontal < 0) movement.playerValues.y = -moveHorizontal;
            }

        }

    }

    private void CheckJump()
    {
        movement.playerValues = movement.moveValues;/*

        if (movement.moveValues == Vector3.zero)
        {
            if (side == Side.left)
                movement.moveHorizontal = -movement.speed / 2;
            else if (side == Side.right)
                movement.moveHorizontal = movement.speed / 2;
            else if (side == Side.down)
                movement.moveVertical = -movement.speed / 2;
        }
*/

    }

    private void ResetClimb()
    {
        movement.isRotatingByMovement = true;
        movement.isSelfMoving = true;
        isMovingStairs = false;
        isStairsUp = false;
        isMovingLedge = false;

        movement.playerValues = Vector3.zero;

        isClimbLedge = false;
        canMove = true;

        timer = timerAction;
    }

    private void Jump()
    {
        if (isMovingStairs && timer <= 0)
        {
            CheckJump();
            ResetClimb();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Stair")
        {
            stairs = other.transform;
            isStairsUp = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Stair")
        {
            isStairsUp = false;
            stairs = null;
        }
    }


    public Side CheckSideByRotate(Transform target)
    {

        if (target.rotation.y == -90)
            return Side.down;
        else if (target.rotation.y == 0)
            return Side.left;
        else if (target.rotation.y == 180)
            return Side.right;
        else
            return Side.down;
    }

}
