using UnityEngine;

public class player : MonoBehaviour
{
    private Transform miTranform;
    public int velocidad;
    public Vector3 _velocidad;
    public GameObject bulletPrefab;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;

    void Start()
    {
        miTranform = transform;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            miTranform.Translate(velocidad * Time.deltaTime * new Vector3(3, 0, 0));
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            miTranform.Translate(velocidad * Time.deltaTime * new Vector3(-3, 0, 0));
        }

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            ScoreManager.LoseLife();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Invader10") || other.CompareTag("Invader25") ||
                     other.CompareTag("Invader50") || other.CompareTag("Invader100"))
        {
            
            ScoreManager.TriggerGameOverByContact();
        }
    }
}