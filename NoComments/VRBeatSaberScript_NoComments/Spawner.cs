using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] cube;
    public Transform[] point;

    public float beat = 1;
    float timer = 0f;

    public RhythmManager rhythmManager;
    private bool isGameStarted = false;

    void Start()
    {

    }

    void Update()
    {

        if (rhythmManager == null || !rhythmManager.musicSource.isPlaying) return;

        timer += Time.deltaTime;
        if (timer > beat)
        {
            int i = Random.Range(0, cube.Length);
            int p = Random.Range(0, point.Length);

            GameObject obj = Instantiate(cube[i], point[p]);

            obj.transform.localPosition = Vector3.zero;
            obj.transform.Rotate(Vector3.forward, 90 * Random.Range(0, 4));

            timer -= beat;
        }

    }
}

