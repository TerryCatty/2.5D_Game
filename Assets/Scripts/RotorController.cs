using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotorController : PlayerAction
{
    [SerializeField] private Transform rotor;
    private float rotationRotor;
    [SerializeField] private float speedRotation;
    [SerializeField] private Vector3 vectorRotate;

    [SerializeField] Clock clock;

    private bool isActive;
    private ActionsController controller;

    public override void DoAction()
    {
        controller.GetComponent<Movement>().isSelfMoving = isActive;
        controller.GetComponent<Movement>().isRotatingByMovement = isActive;
        controller.GetComponent<Movement>().playerValues = Vector3.zero;
        isActive = !isActive;
    }

    private void Update()
    {
        if (isActive == false) return;

        RotorRotate();
    }

    private void RotorRotate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        rotationRotor -= moveHorizontal * speedRotation * Time.deltaTime;


        Vector3 rotation = rotationRotor * vectorRotate;
        rotor.transform.rotation = Quaternion.Euler(rotation);

        clock.ChangeTime(moveHorizontal);


        if (rotationRotor > 360) rotationRotor %= 360;

        if (rotationRotor < 0) rotationRotor += 360;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ActionsController>())
        {
            controller = other.GetComponent<ActionsController>();
            controller.AddAction(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ActionsController>() == controller)
        {
            controller.RemoveAction(this);
        }
    }
}
