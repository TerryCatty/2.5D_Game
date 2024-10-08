using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PushController;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{

    [Header("Move")]

    [SerializeField] private float Speed = 10f;
    public float speed => Speed;

    private CharacterController characterController;

    /*[HideInInspector] */public Vector3 playerValues = new Vector3();
    private Rigidbody rb;
    public float moveHorizontal, moveVertical;
    [HideInInspector] public Vector3 moveValues;

    public bool isSelfMoving = true;
    [Header("Rotate")]

    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform rotatePlayer;

    [HideInInspector] public Vector3 directionRotate;
    public bool isRotatingByMovement = true;

    [Header("Jump")]
    [SerializeField] private float jumpForce;

    public bool canJump;
    public bool isJumping;

    public Action isJump;

    [Header("Gravity")]
    public bool UseGravity = true;
    public float weight = 10f;

    private float gravityForce;

    public float gravityValue = -9.81f;
    [HideInInspector] public bool isGround => characterController.isGrounded;

    public Transform PhysicsCubeKeeper;
    public Transform PhysicsCube;

    [Header("Sliding")]
    public float slideFriction = 0.3f;
    private bool sliding;
    private Vector3 hitNormal;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GravityLogic();
        RotateLogic(directionRotate);
        JumpLogic();
        MovementLogic();

        PhysicsCube.position = PhysicsCubeKeeper.position;
    }

    private void MovementLogic()
    {
        if (sliding && isGround)
        {
            playerValues.x += (1f - hitNormal.y) * hitNormal.x * (Speed - slideFriction);
            playerValues.z += (1f - hitNormal.y) * hitNormal.z * (Speed - slideFriction);

            characterController.Move(playerValues * Time.deltaTime);

            return;
        }


        moveHorizontal = Input.GetAxis("Horizontal") * Speed / weight;
        moveVertical = Input.GetAxis("Vertical") * Speed / weight;

        moveValues.x = moveHorizontal;
        moveValues.z = moveVertical;


        if (isRotatingByMovement)
        {
            directionRotate = moveValues;
        }

        if (isSelfMoving == true)
        {

            if (characterController.isGrounded)
                playerValues = moveValues;


            if(UseGravity) playerValues.y = gravityForce;

        }


        characterController.Move(playerValues * Time.deltaTime);
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitNormal = hit.normal;
        sliding = Vector3.Angle(Vector3.up, hitNormal) >= characterController.slopeLimit;

    }

    private void RotateLogic(Vector3 direction)
    {
        if (direction.x == 0 && direction.z == 0) return;

        Quaternion rotation = Quaternion.LookRotation(direction);

        rotation.x = 0;
        rotation.z = 0;


        rotatePlayer.transform.rotation = Quaternion.Lerp(rotatePlayer.transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    private void GravityLogic()
    {
        if (UseGravity == false) return;

        if (isSelfMoving == false)
        {
            gravityForce = -1f;
            return;
        }

        if (characterController.isGrounded == false) gravityForce -= gravityValue * Time.deltaTime;
        else gravityForce = -1f;
    }

    private void JumpLogic()
    {
        if(isSelfMoving)
            canJump = isGround;

        isJumping = !canJump;

        if (Input.GetAxis("Jump") > 0 && canJump)
        {
            isJump?.Invoke();

            isJumping = true;
            canJump = false;
            gravityForce = jumpForce;
        }
    }

    public Side CheckSide(Transform targetObject)
    {
        Vector3 pushPos = targetObject.position;
        Vector3 playerPos = transform.position;

        Vector3 direction = pushPos - playerPos;
        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;


        if (angle >= 45 && angle <= 135)
            return Side.down;
        else if (angle >= -135 && angle <= -45)
            return Side.up;
        else if (angle > -45 && angle <= 0 || angle < 45 && angle >= 0)
            return Side.left;
        else
            return Side.right;


    }


}
