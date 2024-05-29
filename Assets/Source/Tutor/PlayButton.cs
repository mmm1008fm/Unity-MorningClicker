using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _tutorWindow;
    [SerializeField] private Button _noTutor;
    [SerializeField] private Button _yesTutor;

    private void Awake()
    {
        _button.onClick.AddListener(OnClick);
        _noTutor.onClick.AddListener(OnNoTutor);
        _yesTutor.onClick.AddListener(OnYesTutor);

        _tutorWindow.SetActive(false);
    }

    private void OnYesTutor()
    {
        SceneManager.LoadScene("Tutor");
    }

    private void OnNoTutor()
    {
        SceneManager.LoadScene("Main");
    }

    private void OnClick()
    {
        _tutorWindow.SetActive(true); // Временно
        if (ResourceBank.Instance.FirstTime && ResourceBank.Instance.Score <= 0)
        {
            _tutorWindow.SetActive(true);
        }
        else
        {
            // SceneManager.LoadScene("Main"); // Временно отключено
        }
    }
}