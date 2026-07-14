using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{

        public void StartFixedMode()
    {
        SceneManager.LoadScene("fixedscene");
    }

    public void StartThirdPersonMode()
    {
        SceneManager.LoadScene("3rdscene");
    }

    public void RestartCurrentScene()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoHome()
    {
        SceneManager.LoadScene("Introscene");
    }
}
