using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffOnDistance : MonoBehaviour
{
    Transform target;
    MeshRenderer meshRenderer;

    [HideInInspector] public float maxDist;
    [HideInInspector] public float playerMaxSpeed;
    [HideInInspector] public float size;

    private void Start()
    {
        target = Camera.main.transform;
        
        if(TryGetComponent(out MeshRenderer rend))
        {
            meshRenderer = rend;
        }

        StartCoroutine(CheckDistance());
    }

    IEnumerator CheckDistance()
    {
        while (true)
        {
            Vector3 newPos = transform.position;
            newPos.y = target.position.y;

            float dist = Vector3.Distance(newPos, target.position);

            meshRenderer.enabled = dist < maxDist;

            float newTime = Mathf.Abs(dist - maxDist) / playerMaxSpeed;

            if (newTime <= 1) newTime = 1;

            yield return new WaitForSeconds(newTime);
        }
    }
}
