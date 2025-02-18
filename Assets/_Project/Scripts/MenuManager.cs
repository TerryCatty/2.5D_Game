using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class MenuManager : MonoBehaviour
{

    public GameObject imageVolumeOff;
    public GameObject levelMenu;
    public Button[] levels;
    private bool isPause;
    public GameObject pauseObj;

    private void Start()
    {
        try
        {
            if (imageVolumeOff != null)
                imageVolumeOff.SetActive(!FindAnyObjectByType<VolumeManager>().isMusic);

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


    public void OpenLinkNewTab(string link)
    {
        Application.OpenURL(link);
    }

    public Animator fadeAnimator;
    public int nextLevel;

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
            FindAnyObjectByType<VolumeManager>().imageVolumeOff = imageVolumeOff;
            FindAnyObjectByType<VolumeManager>().SetVolumeMusic();
        }
        catch
        {

        }
        
    }

    public void Pause()
    {
        isPause = !isPause;
        Time.timeScale = isPause ? 0 : 1;
        try
        {
            if (imageVolumeOff != null)
                imageVolumeOff.SetActive(!FindAnyObjectByType<VolumeManager>().isMusic);

        }
        catch
        {

        }
        pauseObj.SetActive(isPause);
    }

    public void ExitGame()
    {

    }
}
