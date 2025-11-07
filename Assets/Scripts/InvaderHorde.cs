using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InvaderHorde : MonoBehaviour
{
    public GameObject[] invaderPrefabs;
    public int rows = 5;
    public int columns = 11;
    public float spacing = 0.005f;
    public float speed = 8f;
    private int direction = 1;
    public float stepDownAmount = 0.5f;
    public float boundary = 8f;

    public GameObject enemyBulletPrefab;
    public float minShootDelay = 0.5f;
    public float maxShootDelay = 1.5f;

    private List<Invader> activeInvaders = new List<Invader>();

    void Start()
    {
        SpawnHorde();
        StartCoroutine(RandomShooterRoutine());
    }

    void Update()
    {
        transform.position += Vector3.right * direction * speed * Time.deltaTime;

        Vector3 nextPosition = transform.position;
        bool shouldStepDown = false;

        foreach (Transform invader in transform)
        {
            if (invader.position.x < -boundary && direction == -1)
            {
                shouldStepDown = true;
            }
            else if (invader.position.x > boundary && direction == 1)
            {
                shouldStepDown = true;
            }
        }

        if (shouldStepDown)
        {
            direction *= -1;
            nextPosition.y -= stepDownAmount;
            transform.position = nextPosition;
        }
    }

    void SpawnHorde()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                GameObject prefabToSpawn = invaderPrefabs[0];

                if (row == 0)
                {
                    prefabToSpawn = invaderPrefabs[3];
                }
                else if (row == 1 || row == 2)
                {
                    prefabToSpawn = invaderPrefabs[2];
                }
                else if (row == 3)
                {
                    prefabToSpawn = invaderPrefabs[1];
                }
                else
                {
                    prefabToSpawn = invaderPrefabs[0];
                }

                Vector3 position = new Vector3(col * spacing, row * spacing, 0);

                GameObject invaderGO = Instantiate(prefabToSpawn, transform.position + position, Quaternion.identity);
                invaderGO.transform.SetParent(transform);

                Invader invaderScript = invaderGO.GetComponent<Invader>();
                if (invaderScript != null)
                {
                    activeInvaders.Add(invaderScript);
                }
            }
        }
    }

    IEnumerator RandomShooterRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minShootDelay, maxShootDelay);
            yield return new WaitForSeconds(waitTime);

            if (activeInvaders.Count > 0)
            {
                int randomIndex = Random.Range(0, activeInvaders.Count);
                Invader shooter = activeInvaders[randomIndex];

                shooter.Shoot(enemyBulletPrefab);
            }
        }
    }

    public void RemoveInvader(Invader invader)
    {
        activeInvaders.Remove(invader);
    }
}