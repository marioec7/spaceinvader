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

    private void OnTriggerEnter2D(Collider2D other)
    {
        int points = 0;
        GameObject hitObject = other.gameObject;

        if (hitObject.CompareTag("Invader10"))
        {
            points = 10;
        }
        else if (hitObject.CompareTag("Invader25"))
        {
            points = 25;
        }
        else if (hitObject.CompareTag("Invader50"))
        {
            points = 50;
        }
        else if (hitObject.CompareTag("Invader100"))
        {
            points = 100;
        }

        Debug.Log("Colisión detectada con: " + hitObject.tag);
        Debug.Log("Puntos a sumar: " + points);

        if (points > 0)
        {
            ScoreManager.AddPoints(points);
            Destroy(hitObject);
            Destroy(gameObject);
        }
    }
}