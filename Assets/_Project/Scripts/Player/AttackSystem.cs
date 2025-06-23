using UnityEngine;
using UnityEngine.UI;

public class AttackSystem : MonoBehaviour
{
    public bool isActive;
    public bool isActive2;

    public GameObject bulletPrefab;
    public GameObject shootStartObj;

    public float timerShoot;
    private float timer;


    public Button attackButton;

    private Movement movement;

    private void Start()
    {
        movement = GetComponent<Movement>();


        try
        {
            attackButton = GameObject.Find("AttackButton").GetComponent<Button>();
            attackButton.onClick.AddListener(SpawnBullet);
        }
        catch
        {

        }
    }

    private void Update()
    {
        attackButton?.gameObject.SetActive(isActive && movement.isAndroid);
        if (isActive == false || isActive2 == false) return;

        if (Input.GetMouseButton(0) && movement.isAndroid == false)
        {
            SpawnBullet();
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    private void SpawnBullet()
    {
        if (timer > 0) return;
        else timer = timerShoot;

        Debug.Log("Player shot");

        GameObject bullet = Instantiate(bulletPrefab, shootStartObj.transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Init(Quaternion.Euler(GetComponent<Movement>().rotatePlayer.rotation.eulerAngles) * Vector3.forward);
    }
}
