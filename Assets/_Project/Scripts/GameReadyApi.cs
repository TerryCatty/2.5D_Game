using System.Runtime.InteropServices;
using UnityEngine;
using GamePush;

public class GameReadyApi : MonoBehaviour
{

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
