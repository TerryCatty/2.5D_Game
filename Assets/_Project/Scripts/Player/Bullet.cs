using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float speed;
    public string tag;

    private Vector3 direction;

    public void Init(Vector3 direction)
    {
        this.direction = direction;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Bullet>() == null)
        {

            if (collision.gameObject.TryGetComponent(out Health health))
            {
                if (collision.gameObject.tag != tag) return;
                health.GetDamage(damage);
            }
            Destroy(gameObject);
        }
       
    }
}
