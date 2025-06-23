using UnityEngine;
using System;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using GamePush;


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

    public bool isAndroid;


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
        ReadyAPI();
    }

    public void ReadyAPI()
    {
        m_GameReadyApi.OnLoadingAPIReady();
        canGameAPI = true;
        isAndroid = GP_Device.IsMobile();
        CheckUserAuth();
        SetGameAPI();
    }

    public void CheckUserAuth()
    {
        /*if (GP_Platform.HasIntegratedAuth() == false)
        {
            Debug.Log("Not integrate");
            return;
        }*/

        SendMsgAuth(GP_Player.IsLoggedIn());
    }
    public void SendMsgAuth(bool auth)
    {
        isAuth = auth;

        Load();
    }


    public void Load()
    {
        Debug.Log("isAuth " + isAuth);

        //LoadData(GP_Player.GetString("gamedatajson"));
        Debug.Log("LoadGP");
        LoadData(GP_Player.GetString("gamedatajson"));
        /*isAuth = true;
        if (isAuth)
        {
            Debug.Log("LoadGP");
            LoadData(GP_Player.GetString("gamedatajson"));
        }
        else
        {
            if (PlayerPrefs.HasKey("GameData"))
            {
                Debug.Log("Has");
                LoadData(PlayerPrefs.GetString("GameData"));
            }
            Debug.Log("LoadPlayerPrefs");
        }*/

        m_GameReadyApi.OnGameplayAPIStart();
    }

    public void SetHitManager(HelpManager manager)
    {
        hintManager = manager;
    }

    private void OnApplicationFocus(bool focus)
    {
        isFocus = focus;
        FindAnyObjectByType<VolumeManager>().SetVolume(isFocus && !isPause);


        SetPause(!focus);


        SetGameAPI();
    }

    public void SetPause(bool pause)
    {
        isPause = pause;

        if(isPause) GP_Game.Pause();
        else GP_Game.Resume();

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
            GP_Ads.ShowFullscreen(OnFullscreenStart, AdvIsShow);
            // ShowAdvExtern();
        }
        catch
        {

        }
    }

    public void AdvIsShow(bool success)
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
        GP_Ads.ShowFullscreen(OnFullscreenStart, AdvIsShowNextLevel);
    }
    public void AdvIsShowNextLevel(bool success)
    {
        FindAnyObjectByType<MenuManager>().Pause(0);
        m_GameReadyApi.OnGameplayAPIStart();
        SceneManager.LoadScene(level);
        SetGameAPI();
    }

    public void OnFullscreenStart()
    {


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

        /* GP_Player.Set("gamedatajson", json);
         GP_Player.Sync(SyncStorageType.preffered);*/

        GP_Player.Set("gamedatajson", json);
        GP_Player.Sync(SyncStorageType.preffered);
        Debug.Log("Save GP" + json);

       /* if (isAuth)
        {
            GP_Player.Set("gamedatajson", json);
            GP_Player.Sync(SyncStorageType.preffered);
            Debug.Log("Save GP" + json);
        }
        else
        {
            PlayerPrefs.SetString("GameData", json);
            PlayerPrefs.Save();
            Debug.Log("SavePlayerPrefs" + json);
        }*/

       /* PlayerPrefs.SetString("GameData", json);
        PlayerPrefs.Save();
        Debug.Log("SavePlayerPrefs" + json);*/
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
            //ShowHintExtern();
            GP_Ads.ShowRewarded("Hint", OnRewarded);
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

    public void OnRewarded(string id)
    {
        if (id == "Hint")
        {
            AdvIsShowHint();
        }
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