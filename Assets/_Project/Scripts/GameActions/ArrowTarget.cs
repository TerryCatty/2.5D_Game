using UnityEngine;

public class ArrowTarget : MonoBehaviour
{
    public int id;
    public GameObject lightObj;
    public float timeLightActive;

    private float timer;
    private bool lightActive;

    public CodeDoorTarget codeDoor;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out Bullet bullet))
        {
            Destroy(bullet.gameObject);
            lightActive = true;
            LightOn();
            codeDoor.AddCodeValue(id);
        }
    }

    public void LightOn()
    {
        timer = timeLightActive;
        lightObj?.SetActive(lightActive);
    }

    private void Update()
    {
        if(lightActive)
        {
            if (timer >= 0) timer -= Time.deltaTime;
            else
            {
                lightActive = false;
                LightOn();
            }
        }
    }

}
