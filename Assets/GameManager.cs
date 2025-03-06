using UnityEngine;
using System;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;


[Serializable]
public struct GameData
{
    public int levels;
    public float musicLevel;
    public bool isMusic;
}
public class GameManager : MonoBehaviour
{

    public GameData data;

    public static GameManager instance;

    private HelpManager hintManager;
    public GameReadyApi m_GameReadyApi;

    public bool isAuth;

    public bool isPause;
    public bool isFocus;

    int level;

    [DllImport("__Internal")]
    private static extern void CheckUserAuth();
    [DllImport("__Internal")]
    private static extern void AuthUser();

    [DllImport("__Internal")]
    private static extern void ShowAdvExtern();
    [DllImport("__Internal")]
    private static extern void ShowAdvExternLoadLevel();

    [DllImport("__Internal")]
    private static extern void ShowHintExtern();

    [DllImport("__Internal")]
    private static extern void SaveExtern(string value);
    [DllImport("__Internal")]
    private static extern void LoadExtern();

    public bool canGameAPI;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }

        else
        {
            Destroy(gameObject);
        }



    }

    public void StartReady()
    {
        m_GameReadyApi.OnLoadingAPIReady();
    }

    public void ReadyAPI()
    {
        canGameAPI = true;
        CheckUserAuth();
        SetGameAPI();
    }

    public void Load()
    {
        Debug.Log("isAuth " + isAuth);

        if (isAuth)
        {
            try
            {
                LoadExtern();
                //LoadData("");
            }
            catch
            {
                if (PlayerPrefs.HasKey("GameData"))
                {
                    LoadData(PlayerPrefs.GetString("GameData"));
                }
                Debug.Log("LoadPlayerPrefs1");
            }
        }
        else
        {
            if (PlayerPrefs.HasKey("GameData"))
            {
                Debug.Log("Has");
                LoadData(PlayerPrefs.GetString("GameData"));
            }
            Debug.Log("LoadPlayerPrefs");
        }

        m_GameReadyApi.OnGameplayAPIStart();
    }

    public void SetHitManager(HelpManager manager)
    {
        hintManager = manager;
    }

    public void SendMsgAuth(string isNotAuth)
    {
        if (isNotAuth == "true")
        {
            isAuth = false;
            Debug.Log("NotAuth");
        }
        else if (isNotAuth == "false")
        {
            isAuth = true;
            Debug.Log("Auth");
        }
        Load();
    }

    private void OnApplicationFocus(bool focus)
    {
        isFocus = focus;
        FindAnyObjectByType<VolumeManager>().SetVolume(isFocus && !isPause);

        SetGameAPI();
    }

    public void SetPause(bool pause)
    {
        isPause = pause;
        FindAnyObjectByType<VolumeManager>().SetVolume(isFocus && !isPause);

        SetGameAPI();
    }

    public void SetGameAPI()
    {
        if (canGameAPI == false) return;

        if (isFocus && isPause == false && SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().buildIndex != 9)
        {
            m_GameReadyApi.OnGameplayAPIStart();
        }
        else if (isFocus == false || isPause || SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 9)
        {
            m_GameReadyApi.OnGameplayAPIStop();
        }

    }


    public void SetLevel(int level)
    {
        if (level > data.levels)
        {
            data.levels = level;
        }
    }

    public void Adv()
    {
        try
        {
            FindAnyObjectByType<MenuManager>().Pause(1);
            m_GameReadyApi.OnGameplayAPIStop();
            ShowAdvExtern();
        }
        catch
        {

        }
    }

    public void AdvIsShow()
    {
        FindAnyObjectByType<MenuManager>().Pause(0);
        m_GameReadyApi.OnGameplayAPIStart();
    }

    public void SetMusicLevel(float value)
    {
        data.musicLevel = value;
    }
    public void SetIsMusic(bool value)
    {
        data.isMusic = value;
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
        SetGameAPI();
    }

    public void LoadLevelWithAdv(int level)
    {
        this.level = level;
        FindAnyObjectByType<MenuManager>().Pause(1);
        m_GameReadyApi.OnGameplayAPIStop();
        ShowAdvExternLoadLevel();
    }
    public void AdvIsShowNextLevel()
    {
        FindAnyObjectByType<MenuManager>().Pause(0);
        m_GameReadyApi.OnGameplayAPIStart();
        SceneManager.LoadScene(level);
        SetGameAPI();
    }

    private void Update()
    {
        if (canGameAPI)
        {
            SetGameAPI();
        }
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(data);

        if (isAuth)
        {

            Debug.Log("Save");

            try
            {
                SaveExtern(json);
            }
            catch
            {
                Debug.Log("error in save");
            }
        }

        PlayerPrefs.SetString("GameData", json);
        PlayerPrefs.Save();
        Debug.Log("SavePlayerPrefs" + json);
    }

    public void LoadData(string value)
    {
        Debug.Log(value);

        if (value != "")
            data = JsonUtility.FromJson<GameData>(value);

        data.musicLevel = 0f;
        data.isMusic = true;
        Debug.Log("Load");
    }

    public void AdvHint()
    {
        try
        {
            m_GameReadyApi.OnGameplayAPIStop();
            FindAnyObjectByType<MenuManager>().Pause(1);
            ShowHintExtern();
        }
        catch
        {

        }
    }

    public void AdvIsShowHint()
    {
        m_GameReadyApi.OnGameplayAPIStart();
        FindAnyObjectByType<MenuManager>().Pause(0);
        hintManager.OpenConfirm();
    }

    public void ResetData()
    {
        bool isMusic = data.isMusic;
        float volume = data.musicLevel;
        data = new GameData();

        data.isMusic = isMusic;
        data.musicLevel = volume;

        SaveData();
    }

}