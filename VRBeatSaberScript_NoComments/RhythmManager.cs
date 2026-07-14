using UnityEngine;
using TMPro;

public class RhythmManager : MonoBehaviour
{
    public static RhythmManager Instance { get; private set; }
    public AudioSource musicSource;
    public TMP_Text countdownText;
    public bool countdownDone = false;
    public System.Action OnMusicEnd;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {

        PlayMusic();
        StartCoroutine(CountdownAndPlayMusic());

    }

    System.Collections.IEnumerator CountdownAndPlayMusic()
    {

        for (int i = 3; i > 0; i--)
        {
            if (countdownText != null)
                countdownText.text = i.ToString();
            Debug.Log(i);
            yield return new WaitForSeconds(1f);
        }

        if (countdownText != null)
            countdownText.text = "GO!";
        Debug.Log("GO!");

        yield return new WaitForSeconds(0.5f);

        if (countdownText != null)
            countdownText.text = "";
        countdownDone = true;
    }

    void PlayMusic()
    {
        musicSource.Play();
    }

    public float GetCurrentMusicTime()
    {
        if (musicSource != null)
        {
            return musicSource.time;
        }
        return 0f;
    }
}

