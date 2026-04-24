using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] int damage;
    public float speed = 20f;
    public float lifeTime = 2f;

    private float timer;

    private void OnEnable()
    {
        timer = 0f;
    }

    private void Update()
    {
        // Move forward manually
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Disable after lifetime expires
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            gameObject.SetActive(false);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit "+collision.gameObject);
        if (collision.gameObject.TryGetComponent<IDamagable>(out IDamagable dmg))
        {
            Debug.Log("Attempt Damage");
            dmg.TakeDamage(damage);
        }
    }
}
