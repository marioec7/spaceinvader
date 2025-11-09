using UnityEngine;

public class Invader : MonoBehaviour
{
    public int pointsValue = 10;
    public float bulletSpeed = 5f;

    public void Shoot(GameObject bulletPrefab)
    {
        Vector3 spawnPosition = transform.position;
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
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
            InvaderHorde horde = transform.parent.GetComponent<InvaderHorde>();
            if (horde != null)
            {
                horde.RemoveInvader(this);
            }

            Destroy(gameObject);
        }
    }
}