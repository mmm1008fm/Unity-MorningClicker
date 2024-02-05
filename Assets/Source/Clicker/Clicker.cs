using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Clicker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Button _mainButton;
    [SerializeField] private int _perClick;
    private int _score;

    public void Initialize()
    {
        _mainButton.onClick.AddListener(() => AddScore(_perClick));
    }

    private void AddScore(int value)
    {
        _score += value;
        _scoreText.text = "Score: " + _score;
    }
}
