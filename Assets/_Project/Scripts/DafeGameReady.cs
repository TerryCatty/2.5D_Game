using UnityEngine;

public class DafeGameReady : MonoBehaviour
{
   public GameManager GameManager;

    private void Start()
    {
        GameManager = GameManager.instance;
    }

    public void StartReady()
    {
        GameManager.StartReady();
    }
}
