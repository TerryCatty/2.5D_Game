using DG.Tweening;
using UnityEngine;

public class EnemyAttackSystem : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject[] shootStartObj;
    public GameObject rotationFire;

    public float timerShoot;
    private float timer;

    public float speedRotate;

    public float timerShoot1;
    public float timerShoot2;

    public Health health;

    private int healthStart;
    public float bulletSpeed;
    public float bulletSpeed1;
    public float bulletSpeed2;
    public float speedAnimation;
    public float speedAnimation1;
    public float speedAnimation2;

    public bool isActive;

    Animator animator;
    private void Start()
    {
        healthStart = (int)(health.health / 2);
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        if (isActive == false) return;

        animator.SetFloat("speed", speedAnimation);

        if (timer <= 0)
        {
            foreach (GameObject obj in shootStartObj)
            {
                SpawnBullet(obj);
            }
            timer = timerShoot;
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if(health.health < healthStart)
        {
            timerShoot = timerShoot2;
            transform.localScale = new Vector3(150, 150, 150);
            bulletSpeed = bulletSpeed2;
            speedAnimation = speedAnimation2;
        }
        else
        {
            timerShoot = timerShoot1;
            bulletSpeed = bulletSpeed1;
            speedAnimation = speedAnimation1;
        }
    }


    private void SpawnBullet(GameObject shootObj)
    {
        Debug.Log("Shot");
        GameObject bullet = Instantiate(bulletPrefab, shootObj.transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Init(shootObj.transform.right);
        bullet.GetComponent<Bullet>().SetSpeed(bulletSpeed);
        
    }
}
