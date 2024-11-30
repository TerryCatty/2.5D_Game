using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ActionAnimation : MonoBehaviour
{
    private bool isPlayed = false;
    [SerializeField] private List<AnimationParameters> animKeys; 
    [SerializeField] private Animator animator; 

    public void Play()
    {
        if (isPlayed) return;

        isPlayed = true;


        animator.enabled = true;
       // StartCoroutine(AnimationProcess());
    }

    IEnumerator AnimationProcess()
    {
        for(int i = 0; i  < animKeys.Count; i++)
        {
            switch (animKeys[i].type)
            {
                case TypeParam.Move:
                    animKeys[i].target.DOMove(animKeys[i].moveVector, animKeys[i].timeKey);
                    Debug.Log("Move");
                    break;
                case TypeParam.Rotate:
                    animKeys[i].target.DORotate(animKeys[i].moveVector, animKeys[i].timeKey);
                    Debug.Log("Rotate");
                    break;
            }

            if(i < animKeys.Count - 1)
                if (animKeys[i + 1].isSyncWithPrevious == false) 
                    yield return new WaitForSeconds(animKeys[i].timeKey);
        }
    }
}

[System.Serializable]
public struct AnimationParameters
{
    public Transform target;
    public float timeKey;
    public bool isSyncWithPrevious;
    public Vector3 moveVector;

    public TypeParam type;

}

[System.Serializable]
public enum TypeParam
{
    Rotate,
    Move
}