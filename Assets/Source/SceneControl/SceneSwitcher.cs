using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneSwitcher
{
    public static void QuitGame() => Application.Quit();

    public static void Switch(string sceneName) => SceneManager.LoadScene(sceneName);

    public static void Reload() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
