using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class ComboManager : MonoBehaviour
{
    public static ComboManager Instance;

    public TMP_Text comboText;

    public TMP_Text missText;

    public GameObject gameOverPanel;
    public GameObject gameOverPanel2;

    public TMP_Text finalComboText;

    public int maxMisses = 10;

    public float punchScaleAmount = 1.4f;

    public float punchDuration = 0.15f;

    private int missCount = 0;
    private int combo = 0;
    private Vector3 originalComboScale;
    private Vector3 originalMissScale;
    private Coroutine comboAnimationCoroutine;
    private Coroutine missAnimationCoroutine;

    void Awake()
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

        if (gameOverPanel == null)
            gameOverPanel = GameObject.Find("GameOverPanel");

        if (gameOverPanel2 == null)
            gameOverPanel2 = GameObject.Find("GameOverPanel2");

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        if (gameOverPanel2 != null)
        {
            gameOverPanel2.SetActive(false);
        }

        UpdateComboText();
        UpdateMissText();
    }

    void Start()
    {

        if (comboText != null)
        {
            originalComboScale = comboText.transform.localScale;
        }
        if (missText != null)
        {
            originalMissScale = missText.transform.localScale;
        }
    }

    public void AddCombo()
    {
        combo++;
        UpdateComboText();

        if (comboText != null)
        {

            if (comboAnimationCoroutine != null)
            {
                StopCoroutine(comboAnimationCoroutine);
            }

            comboAnimationCoroutine = StartCoroutine(PunchScaleText(comboText, originalComboScale));
        }
    }

    private IEnumerator PunchScaleText(TMP_Text targetText, Vector3 baseScale)
{
    if (targetText == null) yield break;

    targetText.transform.localScale = baseScale * punchScaleAmount;

    float elapsedTime = 0f;

    while (elapsedTime < punchDuration)
    {
        elapsedTime += Time.deltaTime;

        targetText.transform.localScale = Vector3.Lerp(
            baseScale * punchScaleAmount,
            baseScale,
            elapsedTime / punchDuration
        );

        yield return null;
    }

    targetText.transform.localScale = baseScale;
}

    public void OnMiss()
    {
        missCount++;
        UpdateMissText();

        combo = 0;
        UpdateComboText();

        if (missText != null)
        {

            if (missAnimationCoroutine != null)
            {
                StopCoroutine(missAnimationCoroutine);
            }

        missAnimationCoroutine = StartCoroutine(PunchScaleText(missText, originalMissScale));

        }

        if (missCount >= maxMisses)
        {
            if (gameOverPanel != null)
                gameOverPanel.SetActive(true);

            if (gameOverPanel2 != null)
                gameOverPanel2.SetActive(true);

            if (finalComboText != null)
            {
                finalComboText.text = "COMBO : " + combo;
            }

            Note[] remainingNotes = FindObjectsByType<Note>(FindObjectsSortMode.None);
            foreach (Note note in remainingNotes)
            {
                Destroy(note.gameObject);
            }

            Time.timeScale = 0f;

            AudioSource[] audios = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
            foreach (AudioSource audio in audios)
            {
                audio.Pause();
            }

            UnityEngine.Video.VideoPlayer[] videos = FindObjectsByType<UnityEngine.Video.VideoPlayer>(FindObjectsSortMode.None);
            foreach (UnityEngine.Video.VideoPlayer video in videos)
            {
                video.Pause();
            }
        }
        else
        {
            Debug.Log($"미스 {missCount}/{maxMisses} - 아직 게임 종료 아님");
        }
    }

    public void UpdateComboText()
    {
        if (comboText != null)
        {
            comboText.text = "COM : " + combo;
        }
    }

    public void UpdateMissText()
    {
        if (missText != null)
        {
            missText.text = "MISS : " + missCount;
        }
    }

}

