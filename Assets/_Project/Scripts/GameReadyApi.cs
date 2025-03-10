using System.Runtime.InteropServices;
using UnityEngine;
using GamePush;

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
        GP_Game.GameReady();
    }

    public void OnGameplayAPIStart()
    {
        GP_Game.GameplayStart();
    }

    public void OnGameplayAPIStop()
    {
        GP_Game.GameplayStop();
    }

}
