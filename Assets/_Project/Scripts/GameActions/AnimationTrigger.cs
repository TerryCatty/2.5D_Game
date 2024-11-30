using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    [SerializeField] private List<ActionAnimation> animations;

    private void OnTriggerEnter(Collider other)
    {
        foreach(ActionAnimation anim in animations)
        {
            anim.Play();
        }
    }
}
