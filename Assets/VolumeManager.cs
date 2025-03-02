using UnityEngine;
using UnityEngine.Audio;
using static UnityEngine.Rendering.DebugUI;

public class VolumeManager : MonoBehaviour
{
    public AudioMixer mixer;

    public static VolumeManager instance;

    public GameObject imageVolumeOff;

    public bool isMusic = true;
    public bool isFocus = true;
    private void Start()
    {/*
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        else
        {
            Destroy(gameObject);
        }
*/

        isMusic = !GameManager.instance.data.isMusic;
        SetVolumeMusic();

    }


    public void SetVolumeMusic()
    {
        Debug.Log("Set");

        isMusic = !isMusic;

        imageVolumeOff?.SetActive(!isMusic);

        float value;


        if (isMusic) value = 0;
        else value = -80;

        mixer.SetFloat("Volume", value);


        GameManager.instance.SetMusicLevel(value);
        GameManager.instance.SetIsMusic(isMusic);
    }

    public void SetVolume(bool isFocus)
    {
        this.isFocus = isFocus;

        float value;

        if (isFocus) value = 0;
        else value = -80;

        if (isFocus && isMusic) value = 0;
        else if (!isFocus || !isMusic) value = -80;

        Debug.Log(value);

        mixer.SetFloat("Volume", value);
    }
}
