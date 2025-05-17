using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour
{
    public string nextSceneName;
    public string skipToSceneName;

    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    public void SkipToScene()
    {
        SceneManager.LoadScene(skipToSceneName);
    }
}
