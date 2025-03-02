using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using UnityEditor;
using Unity.VisualScripting;

public class MenuManager : MonoBehaviour
{

    public GameObject imageVolumeOff;
    public GameObject levelMenu;
    public Button[] levels;
    private bool isPause;
    public GameObject pauseObj;
    [DllImport("__Internal")]
    private static extern void OpenLinkInSameTab(string link);

    private void Start()
    {
        volumeManager = FindAnyObjectByType<VolumeManager>();
        try
        {
            if (imageVolumeOff != null)
                imageVolumeOff.SetActive(!volumeManager.isMusic);

        }
        catch
        {

        }
    }
    public void OpenLevelMenu()
    {
        Debug.Log("openLevelMenu");
        levelMenu.SetActive(true);

        Debug.Log("openLevelMenu2 ");

        for(int i  = 0; i < levels.Length; i++)
        {
            levels[i].interactable = false;
        }

        for (int i = 0; i <= GameManager.instance.data.levels; i++)
        {
            levels[i].interactable = true;
        }
    }

    public void CloseLevelMenu()
    {
        levelMenu.SetActive(false);

        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].interactable = false;
        }
    }

    public void NewGame(int i = -1)
    {
        GameManager.instance.ResetData();

        if (i != -1) nextLevel = i;

        StartCoroutine(NextLevel());
    }
    public void StartGame(int i = -1)
    {
        if(i != -1) nextLevel = i;

        if (isPause && pauseObj != null) Pause();

        StartCoroutine(NextLevel());
    }

    public void OpenLink(string link)
    {
        OpenLinkInSameTab(link);
    }

    public void OpenLinkNewTab(string link)
    {
        Application.OpenURL(link);
    }

    public Animator fadeAnimator;
    public int nextLevel;

    VolumeManager volumeManager;

    

    private IEnumerator NextLevel()
    {
        fadeAnimator.Play("Fading");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(nextLevel);
    }

    public void SetOffMusic()
    {
        try
        {
            volumeManager.imageVolumeOff = imageVolumeOff;
            volumeManager.SetVolumeMusic();
        }
        catch
        {

        }
        
    }

    public void Pause(int pause = 2)
    {
        if(pause > 1)
        {
            isPause = !isPause;
        }
        else
        {
            isPause = pause == 0 ? false : true;
        }

        Time.timeScale = isPause ? 0 : 1;

        GameManager.instance.SetPause(isPause);

        try
        {
            if (imageVolumeOff != null)
                imageVolumeOff.SetActive(!volumeManager.isMusic);
                
        }
        catch
        {

        }

        if (isPause)
        {
#if UNITY_WEBGL && !UNITY_EDITOR

                GameManager.instance.m_GameReadyApi.OnGameplayAPIStop();

#else

            Debug.Log("GameplayAPIStop");

                #endif
        }
        else
        {
                #if UNITY_WEBGL && !UNITY_EDITOR

                                GameManager.instance.m_GameReadyApi.OnGameplayAPIStart();

                #else

                            Debug.Log("GameplayAPIStart");

                #endif
        }

        pauseObj.SetActive(isPause);
    }

    public void ExitGame()
    {

    }
}
