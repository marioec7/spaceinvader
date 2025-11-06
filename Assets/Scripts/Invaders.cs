using UnityEngine;

public class Invader : MonoBehaviour
{
    public int pointsValue = 10;
    public Transform shootPoint;
    public float bulletSpeed = 5f; // A�adido para que la bala sepa su velocidad

    public void Shoot(GameObject bulletPrefab)
    {
        Vector3 spawnPosition = shootPoint != null ? shootPoint.position : transform.position;
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Usamos la velocidad definida en el script de la bala si existe, sino usamos una predeterminada
            EnemyBullet enemyBulletScript = bullet.GetComponent<EnemyBullet>();
            float speedToUse = enemyBulletScript != null ? enemyBulletScript.speed : bulletSpeed;

            rb.linearVelocity = Vector2.down * speedToUse;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            Destroy(other.gameObject);

            // Notificar a la Horda que este invasor ha muerto
            InvaderHorde horde = transform.parent.GetComponent<InvaderHorde>();
            if (horde != null)
            {
                horde.RemoveInvader(this);
            }

            Destroy(gameObject);
        }

        else if (other.CompareTag("Base"))
        {
            // L�gica de fin de juego aqu�
        }
    }
}