using UnityEngine;

public class BulletSpawner: MonoBehaviour
{
    private GameManager gameManager;
    private Transform target;
    public GameObject[] bulletPrefab;

    private float spawnRate;
    private float timeAfterSpawn;
    public float RateMin = 0.5f;
    public float RateMax = 3f;

    void Start()
    {

        timeAfterSpawn = 0f;
        spawnRate = Random.Range(RateMin, RateMax);
        target = GameObject.FindGameObjectWithTag("Player").transform;
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {

        if (gameManager == null || !gameManager.IsGameStarted)
            return;

        timeAfterSpawn += Time.deltaTime;

        if(timeAfterSpawn >= spawnRate)
        {
            timeAfterSpawn = 0f;

            int randomIndex = Random.Range(0, bulletPrefab.Length);
            GameObject bullet = Instantiate(bulletPrefab[randomIndex], transform.position, transform.rotation);

            Vector3 targetPosition = target.position;
            targetPosition.y += 1.2f;
            bullet.transform.LookAt(targetPosition);

            spawnRate = Random.Range(RateMin, RateMax);
        }
    }
}

