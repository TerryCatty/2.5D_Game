using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PushController;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    public Animator anim;

    private Camera cam;

    [Header("Move")]

    public MovementParams movementParameters;


    private float Speed => movementParameters.speed;

    private Vector3 moveValuesOffset => movementParameters.moveValuesOffset;

    private CharacterController characterController;

   public Vector3 playerValues = new Vector3();
    private Rigidbody rb;
    public float moveHorizontal, moveVertical;
    public float moveHorizontalPlayer, moveVerticalPlayer;
    [HideInInspector] public Vector3 moveValues;

    public bool isSelfMoving = true;
    [Header("Rotate")]

    [SerializeField] private float rotationSpeed;
    public Transform rotatePlayer;

    public Vector3 directionRotate;
    public bool isRotatingByMovement = true;
    public bool isRotatingCursor = false;

    [Header("Jump")]
    [SerializeField] private float jumpForce;

    public bool canJump;
    public bool isJumping;
    public bool jumpStart;

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

    public bool isActive = true;

    void Start()
    {
        cam = Camera.main;
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isActive == false) return;

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
            playerValues.x += (1f - hitNormal.y) * hitNormal.x * (Speed - slideFriction) * moveValuesOffset.x;
            playerValues.z += (1f - hitNormal.y) * hitNormal.z * (Speed - slideFriction) * moveValuesOffset.z;
            if (UseGravity) playerValues.y = gravityForce * moveValuesOffset.y;

            characterController.Move(playerValues * Time.deltaTime);

            return;
        }


        moveHorizontal =  Input.GetAxis("Horizontal");
        moveHorizontalPlayer = moveHorizontal * Speed / weight;

        moveVertical =  Input.GetAxis("Vertical");
        moveVerticalPlayer = moveVertical * Speed / weight;

        moveValues.x = moveHorizontalPlayer * moveValuesOffset.x;
        moveValues.z = moveVerticalPlayer * moveValuesOffset.z;

        if (moveValues.x == 0 && moveValues.z == 0)
        {
            anim.SetFloat("Speed", 0);
        }
        else
        {
            anim.SetFloat("Speed", movementParameters.speed / movementParameters.maxSpeed);
        }


        if (isRotatingByMovement)
        {
            directionRotate = moveValues;
        }

        else if (isRotatingCursor)
        {
            RotateByCursor();
        }

        if (isSelfMoving == true)
        {

            //if (characterController.isGrounded)
                playerValues = moveValues;


            if(UseGravity) playerValues.y = gravityForce * moveValuesOffset.y;

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
    private  void RotateByCursor()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        directionRotate = worldMousePos - transform.position;
        directionRotate.y = 0;

        RotateLogic(directionRotate);
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

        if(isGround)
            anim.SetBool("IsJump", false);

        if (Input.GetAxis("Jump") > 0 || jumpStart)
        {
            Jump();
        }
    }

    public void Jump()
    {
        if (!canJump) return;

        isJump?.Invoke();

        isJumping = true;
        anim.SetBool("IsJump", true);
        canJump = false;
        gravityForce = jumpForce;
        jumpStart = false;
    }

    public void SetJumpStart()
    {
        jumpStart = true;
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


    public void SetNewParameters(MovementParams parametres)
    {
        movementParameters = parametres;
    }

    public void SetNewCameraParameters(CameraParams parametres)
    {
        cam.GetComponent<CameraFollow>().CameraParams = parametres;
    }

}
