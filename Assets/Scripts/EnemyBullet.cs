using UnityEngine;


public class EnemyBullet : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

        else if (other.CompareTag("Base"))
        {
            Destroy(gameObject);
        }
    }
}