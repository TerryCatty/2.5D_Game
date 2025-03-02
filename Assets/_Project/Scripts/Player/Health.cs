using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
   public int health;
    public bool isPlayer;

    public float fillOneHealth;

    public GameObject spawn;

    public Image imageHealth;

    private void Start()
    {
        fillOneHealth = 1 / (float)health;
        Debug.Log(1 / (float)health);
    }

    public void GetDamage(int damage)
    {
        health -= damage;

        if (imageHealth != null) imageHealth.fillAmount = fillOneHealth * health;

        if (health <= 0)
        {
            if(isPlayer) GetComponent<PlayerData>().Death();
            else
            {
                spawn.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
}
