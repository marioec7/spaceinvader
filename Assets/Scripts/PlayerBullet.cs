using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 10f; 

    void Update()
    {
      
        transform.position += Vector3.up * speed * Time.deltaTime;

        
        if (transform.position.y > 10f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Invader10"))
        {
            Destroy(collision.gameObject);

            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Invader25"))
        {
            Destroy(collision.gameObject);

            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Invader50"))
        {
            Destroy(collision.gameObject);

            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Invader100"))
        {
            Destroy(collision.gameObject);

            Destroy(gameObject);
        }
    }
}