using System.Runtime.InteropServices;
using UnityEngine;

public class GameReadyApi : MonoBehaviour
{
#if UNITY_WEBGL
    [DllImport("__Internal")]

    private static extern void LoadingAPIReady();

    [DllImport("__Internal")]

    private static extern void GameplayAPIStart();

    [DllImport("__Internal")]

    private static extern void GameplayAPIStop();

#endif

    public void OnLoadingAPIReady()

    {

        LoadingAPIReady();

    }

    public void OnGameplayAPIStart()

    {

        GameplayAPIStart();

    }

    public void OnGameplayAPIStop()

    {

        GameplayAPIStop();

    }

}
