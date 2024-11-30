using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class CameraFollow : MonoBehaviour
{
    public CameraParams CameraParams;

    [SerializeField] private Transform target;
    private Vector3 fixedPosition;
    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        MovementLogic();
    }

    private void MovementLogic()
    {
        Vector3 position = target.position + CameraParams.offset;

        fixedPosition.x = CameraParams.blockedMove.x == 0 ? position.x : fixedPosition.x;
        fixedPosition.y = CameraParams.blockedMove.y == 0 ? position.y : fixedPosition.y;
        fixedPosition.z = CameraParams.blockedMove.z == 0 ? position.z : fixedPosition.z;


        transform.DOMove(fixedPosition, CameraParams.timeChangeOffset); //= Vector3.Lerp(transform.position, fixedPosition, Time.deltaTime * CameraParams.speedChangeOffset);

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, CameraParams.zoom, Time.deltaTime * CameraParams.speedChangingZoom);


        transform.DORotate(CameraParams.rotation, CameraParams.timeChangeRotate);
    }
}

[System.Serializable]
public struct CameraParams
{
    [Range(0, 179)]
    public float zoom;
    public float speedChangingZoom;

    public float speed;
    public Vector3[] borders;

    public float timeChangeRotate;
    public Vector3 rotation;

    public float timeChangeOffset;
    public Vector3 offset;

    public Vector3 blockedMove;
} 
