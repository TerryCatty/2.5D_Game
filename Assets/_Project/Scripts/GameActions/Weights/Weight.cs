using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weight : MonoBehaviour
{
    public List<Bowl> bowls;
    [SerializeField] private Transform arrow;
    [SerializeField] private float maxRotation;
    [SerializeField] private float speedRotation;
    [SerializeField] private float offset;

    private void Update()
    {
        RotateArrow();
    }

    private void RotateArrow()
    {
        float rotationZ = bowls[0].weight / offset - bowls[1].weight / offset;

        if (rotationZ >= maxRotation) rotationZ = maxRotation;
        else if(rotationZ <= -maxRotation) rotationZ = -maxRotation;

        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, rotationZ));

        arrow.transform.rotation = Quaternion.Lerp(arrow.transform.rotation, rotation, speedRotation * Time.deltaTime);
    }

}
