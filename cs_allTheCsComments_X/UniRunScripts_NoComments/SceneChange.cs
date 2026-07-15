using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChange : MonoBehaviour
{
    public AudioSource audioSource;

    public void ChangeScene(int index)
    {
        StartCoroutine(ChangeSceneRoutine(index));

    }

    IEnumerator ChangeSceneRoutine(int index)
    {
        audioSource.PlayOneShot(audioSource.clip);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(index);
    }

    public void RestartScene(int index)
    {
        SceneManager.LoadScene(index);
    }

   public void HomeButton(int index)
    {
        SceneManager.LoadScene(index);
    }
}

