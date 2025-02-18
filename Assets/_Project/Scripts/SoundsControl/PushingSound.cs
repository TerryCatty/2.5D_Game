using DG.Tweening;
using UnityEngine;

public class PushingSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public bool isSounds;

    private bool isStopped = true;

    Vector3 prevPos;
    public float distance;
    public float distanceActive = 0.03f;

    public float speed;

    private void FixedUpdate()
    {
        if(_audioSource == null) return;

        bool oldSounds = isSounds;

        distance = Vector3.Distance(prevPos, transform.position);

        isSounds = distance > distanceActive;

        if (oldSounds == !isSounds)
        {
            if(isStopped) _audioSource.Play();
            else _audioSource.Stop();

            isStopped = !isStopped;
        }


        speed = distance / Time.deltaTime;

        prevPos = transform.position;
    }
}
