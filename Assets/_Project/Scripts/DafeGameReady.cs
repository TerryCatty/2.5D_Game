using UnityEngine;

public class DafeGameReady : MonoBehaviour
{
   public GameManager GameManager;
    public LocalizationManager LocalizationManager;

    private void Start()
    {
        GameManager = GameManager.instance;
        LocalizationManager = LocalizationManager.instance;
    }

    public void StartReady()
    {
        GameManager.StartReady();
    }

    public void LoadLanguage()
    {
        //LocalizationManager.LoadData();
    }
}
