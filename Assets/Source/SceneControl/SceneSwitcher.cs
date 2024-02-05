using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void QuitGame() => Application.Quit();

    public void Switch(string sceneName) => SceneManager.LoadScene(sceneName);

    public void Reload() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
