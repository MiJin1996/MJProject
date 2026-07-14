using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneName;
    [Header("--- 효과 설정 ---")]
    public AudioSource audioSourceSpeaker;
    public AudioClip clickSound;
    public GameObject clickEffectPrefab;

    private IEnumerator PlayEffectsAndLoad(System.Action loadAction)
    {

        if (audioSourceSpeaker != null && clickSound != null)
            audioSourceSpeaker.PlayOneShot(clickSound);

        if (clickEffectPrefab != null)
        {

            Vector3 buttonScreenPos = transform.position;

            GameObject effect = Instantiate(clickEffectPrefab, buttonScreenPos, Quaternion.identity);

            effect.transform.SetParent(transform.parent, false);

            effect.transform.position = buttonScreenPos;

            effect.transform.localScale = Vector3.one;

            Destroy(effect, 2f);
        }

        yield return new WaitForSecondsRealtime(0.5f);

        loadAction?.Invoke();
    }

    public void ChangeScene()
    {
        StartCoroutine(PlayEffectsAndLoad(() => {
            if (!string.IsNullOrEmpty(sceneName))
                SceneManager.LoadScene(sceneName);
            else
                Debug.LogWarning("이동할 씬 이름이 설정되지 않았습니다!");
        }));
    }

    public void Restart(int sceneIndex)
    {
        StartCoroutine(PlayEffectsAndLoad(() => {
            Time.timeScale = 1f;
            SceneManager.LoadScene(sceneIndex);
        }));
    }

    public void QuitGame()
    {
        StartCoroutine(PlayEffectsAndLoad(() => {
            Time.timeScale = 1f;
            Application.Quit();
        }));
    }
}

