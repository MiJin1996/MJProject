using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public GameObject CFXR4FallingStars;

    public bool isGameover = false;
    public GameObject gameoverUI;
    public TextMeshProUGUI resultScoreText;

    public TextMeshProUGUI scoreText;
    private int score = 0;

    public AudioSource bgmAudioSource;
    public AudioClip introMusic;
    public AudioClip mainMusic;
    public AudioClip gameOverMusic;

    public GameObject endMidtermEffect;
    public AudioClip endMidtermSound;

    [Range(0f, 1f)]
    public float bgmVolume = 0.5f;
    [Range(0f, 1f)]
    public float gameOverVolume = 1f;

    void Awake() {

        if (instance == null)
        {

            instance = this;
        }
        else
        {
            Debug.LogWarning("씬에 두개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlaySceneMusic();

        GameObject effect = Instantiate(CFXR4FallingStars, Vector3.zero, Quaternion.identity);
    }

    void PlaySceneMusic()
    {
        if (bgmAudioSource == null)
        {
            Debug.LogWarning("BGM AudioSource가 할당되지 않았습니다.");
            return;
        }

        bgmAudioSource.volume = bgmVolume;
        bgmAudioSource.loop = true;

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (sceneIndex == 0)
        {
            bgmAudioSource.clip = introMusic;
            bgmAudioSource.Play();
        }
        else if (sceneIndex == 1)
        {
            bgmAudioSource.clip = mainMusic;
            bgmAudioSource.Play();
        }
    }

    void Update() {

        }

    public void AddScore(int newScore) {
        if(!isGameover)
        {
            score += newScore;
            scoreText.text = "Score: " + score;
        }
    }

    public void OnPlayerDead() {
        scoreText.gameObject.SetActive(false);
        LevelManager.instance.levelText.gameObject.SetActive(false);

        resultScoreText.text = "BEST Score: " + score;
        isGameover = true;
        gameoverUI.SetActive(true);
        bgmAudioSource.PlayOneShot(endMidtermSound);
        endMidtermEffect.SetActive(true);

        bgmAudioSource.clip = gameOverMusic;
        bgmAudioSource.Play();

    }
}

