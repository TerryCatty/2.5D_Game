using UnityEngine;
using DG.Tweening;

public class Credits : MonoBehaviour
{
    public Transform endPoint;
    public Transform scroll;
    public float timeScrollingAll;

    private void Start()
    {
        scroll.DOMove(endPoint.transform.position, timeScrollingAll);
    }
}
