using UnityEngine;

public class PlatformSpawner : MonoBehaviour {
    public GameObject platformSnailPrefab;
    public GameObject platformCoinPrefab;

    public int cnt = 3;

    public float timeBetSpawnMin = 1.25f;
    public float timeBetSpawnMax = 2.25f;
    private float timeBetSpawn;

    public float yMin = -3.5f;
    public float yMax = 1.5f;
    private float xPos = 20f;

    private GameObject[] platforms;
    private int currentIndex = 0;

    private Vector2 poolPosition = new Vector2(0, -25);
    private float lastSpawnTime;

    void Start() {

        platforms = new GameObject[cnt];

        for(int i=0; i<cnt; i++)
        {

            if (Random.Range(0, 2) == 0)
            {
                platforms[i] = Instantiate(platformSnailPrefab, poolPosition, Quaternion.identity);
            }
            else
            {
                platforms[i] = Instantiate(platformCoinPrefab, poolPosition, Quaternion.identity);
            }
        }

        lastSpawnTime = 0f;
        timeBetSpawn = 0f;
    }

    void Update() {

        if(GameManager.instance.isGameover)
        {
            return;
        }

        if(Time.time >= lastSpawnTime + timeBetSpawn)
        {
            lastSpawnTime = Time.time;
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);

            float yPos = Random.Range(yMin, yMax);

            platforms[currentIndex].SetActive(false);
            platforms[currentIndex].transform.position = new Vector2(xPos, yPos);

            Coin[] coins = platforms[currentIndex].GetComponentsInChildren<Coin>(true);
            for (int i = 0; i < coins.Length; i++)
            {
                coins[i].gameObject.SetActive(true);
            }

            platforms[currentIndex].SetActive(true);

            currentIndex++;
            if(currentIndex >=cnt)
            {
                currentIndex = 0;
            }

        }

    }
}

