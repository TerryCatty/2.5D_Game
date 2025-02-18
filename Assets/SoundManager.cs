using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundObject soundObjPrefab;

    [SerializeField] private AudioClip soundStep;
    [SerializeField] private AudioClip soundJump;


    public void PlayStep()
    {
       SoundObject spawn = Instantiate(soundObjPrefab, transform);
        spawn.PlayAudio(soundStep, 1f);
    }


    public void PlayJump()
    {
        SoundObject spawn = Instantiate(soundObjPrefab, transform);
        spawn.PlayAudio(soundJump, 1f);
    }
}
