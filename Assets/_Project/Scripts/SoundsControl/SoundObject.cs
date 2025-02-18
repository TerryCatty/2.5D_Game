using UnityEngine;

public class SoundObject : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource;
 
    public void PlayAudio(AudioClip audio, float lifetime = 1f)
    {
        m_AudioSource.clip = audio;
        m_AudioSource.Play();

        Invoke("Lifetime", lifetime);
    }

    private void Lifetime()
    {
        Destroy(gameObject);
    }
}
