using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameoverText;
    public GameObject homeButton;
    public GameObject Jump;
    public GameObject Run;

    public ParticleSystem rainParticles;
    public ParticleSystem rainParticless;
    public ParticleSystem rainParticlesss;
    public ParticleSystem rainParticlesssss;

    public AudioSource sfxAudioSource;
    public AudioSource bgmAudioSource;
    public AudioClip gameoverBgmClip;

    public Text timeText;
    public Text recordText;
    public Text countdownText;

    private float countdownTime = 3f;
    private float surviveTime;
    private bool isGameover;
    private bool isCountingDown = true;
    public bool IsGameStarted { get; private set; } = false;

    void Start()
    {

        surviveTime = 0;
        isGameover = false;

        isCountingDown = true;
        countdownTime = 3f;
        IsGameStarted = false;

        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(true);
            countdownText.text = "3";
        }
    }

    void Update()
    {
        if (isCountingDown)
        {

            countdownTime -= Time.deltaTime;
            if (countdownText != null)
            {
                if (countdownTime > 2f)
                    countdownText.text = "3";
                else if (countdownTime > 1f)
                    countdownText.text = "2";
                else if (countdownTime > 0f)
                    countdownText.text = "1";
            }

            if (countdownTime <= 0f)
            {
                isCountingDown = false;
                if (countdownText != null)
                    countdownText.gameObject.SetActive(false);
                StartGame();
            }
        }

        if (!isGameover && IsGameStarted)
        {
            surviveTime += Time.deltaTime;
            timeText.text = "Time: " + (int)surviveTime;
        }
        else if (isGameover)
        {
            if (Input.GetKeyDown(KeyCode.R))
                SceneManager.LoadScene("SampleScene");
        }

    }

    private void StartGame()
    {
        IsGameStarted = true;

        if (bgmAudioSource != null && !bgmAudioSource.isPlaying)
        {
            bgmAudioSource.Play();
        }

        if (Jump != null)
            Jump.SetActive(true);

        if (Run != null)
            Run.SetActive(true);

    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
            sfxAudioSource.PlayOneShot(clip);
    }

    public void EndGame()
    {

        isGameover = true;
        gameoverText.SetActive(true);
        timeText.gameObject.SetActive(false);
        Jump.SetActive(false);
        Run.SetActive(false);

        if (homeButton != null)
        homeButton.SetActive(true);

        if (rainParticles != null)
            rainParticles.Stop();
        if (rainParticless != null)
            rainParticless.Stop();
        if (rainParticlesss != null)
            rainParticlesss.Stop();
        if (rainParticlesssss != null)
            rainParticlesssss.Stop();

        BulletSpawner[] butterflies = FindObjectsByType<BulletSpawner>(FindObjectsSortMode.None);

        foreach (BulletSpawner bfly in butterflies)
        {

            bfly.enabled = false;

            Animation bflyAni = bfly.GetComponent<Animation>();
            if (bflyAni != null)
            {
                bflyAni.Stop();
            }
        }

        if (bgmAudioSource != null && gameoverBgmClip != null)
        {
            bgmAudioSource.Stop();
            bgmAudioSource.clip = gameoverBgmClip;
            bgmAudioSource.loop = true;
            bgmAudioSource.Play();
        }

        float bestTime = PlayerPrefs.GetFloat("BestTime");

        if (surviveTime > bestTime)
        {
            bestTime = surviveTime;
            PlayerPrefs.SetFloat("BestTime", bestTime);
        }
        recordText.text = "Best Time: " + (int)bestTime;
    }
}

