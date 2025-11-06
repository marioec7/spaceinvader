using UnityEngine;

public class player : MonoBehaviour
{
    private Transform miTranform;
    public int velocidad;
    public Vector3 _velocidad;
    public GameObject bulletPrefab;
    public float fireRate = 0.5f; 
    private float nextFireTime = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        miTranform = transform;
    }

    // Update is called once per frame
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
}


