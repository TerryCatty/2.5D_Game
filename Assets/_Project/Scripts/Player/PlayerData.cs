using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    private void Awake()
    {
        Load();
    }

    public void Death()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(scene);
    }
    public void Save()
    {
        PlayerPrefs.SetFloat("xPositionPlayer", transform.position.x);
        PlayerPrefs.SetFloat("yPositionPlayer", transform.position.y);
        PlayerPrefs.SetFloat("zPositionPlayer", transform.position.z);
        PlayerPrefs.SetInt("Scene", SceneManager.GetActiveScene().buildIndex);

        PlayerPrefs.Save();
    }

    public void Load()
    {
        Vector3 position = new Vector3();

        if (PlayerPrefs.HasKey("xPositionPlayer") == false) return;
        int scene = PlayerPrefs.GetInt("Scene");

        if (scene != SceneManager.GetActiveScene().buildIndex) return;

        position.x = PlayerPrefs.GetFloat("xPositionPlayer");
        position.y = PlayerPrefs.GetFloat("yPositionPlayer");
        position.z = PlayerPrefs.GetFloat("zPositionPlayer");

        transform.position = position;
    }
}
