using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public CameraParams CameraParams;

    [SerializeField] private Transform target;
    private Vector3 fixedPosition;

    private void Update()
    {
        MovementLogic();
        
        transform.rotation = Quaternion.Euler(CameraParams.rotation);
    }

    private void MovementLogic()
    {
        Vector3 position = target.position + CameraParams.offset;

        fixedPosition.x = CameraParams.blockedMove.x == 0 ? position.x : fixedPosition.x;
        fixedPosition.y = CameraParams.blockedMove.y == 0 ? position.y : fixedPosition.y;
        fixedPosition.z = CameraParams.blockedMove.z == 0 ? position.z : fixedPosition.z;

        transform.position = Vector3.Lerp(transform.position, fixedPosition, Time.deltaTime * CameraParams.speed);
    }
}

[System.Serializable]
public struct CameraParams
{
    public float zoom;
    public float speedChangingZoom;

    public float speed;
    public Vector3[] borders;

    public float speedChangeRotate;
    public Vector3 rotation;

    public float speedChangeOffset;
    public Vector3 offset;

    public Vector3 blockedMove;
} 
