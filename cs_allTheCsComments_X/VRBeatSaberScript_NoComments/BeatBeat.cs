using UnityEngine;

public class BeatBeat : MonoBehaviour
{
    public float targetTime;
    public float noteSpeed;

    private RhythmManager rhythmManager;
    private float xOffset;
    private float yOffset;

    void Start()
    {

        rhythmManager = RhythmManager.Instance ?? FindObjectOfType<RhythmManager>();

        xOffset = transform.localPosition.x;
        yOffset = transform.localPosition.y;
    }

    void Update()
    {
        if (rhythmManager == null) return;

        float currentTime = rhythmManager.GetCurrentMusicTime();

        float distance = (targetTime - currentTime) * noteSpeed;

        transform.localPosition = new Vector3(xOffset, yOffset, distance);

        if (distance < -2f)
            Destroy(gameObject);
    }
}

