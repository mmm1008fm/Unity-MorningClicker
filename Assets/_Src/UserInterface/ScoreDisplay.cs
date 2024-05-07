using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private string _prefix = "Score: ";
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void OnEnable()
    {
        ResourceBank.ScoreChanged += UpdateScoreDisplay;
    }

    private void OnDisable()
    {
        ResourceBank.ScoreChanged -= UpdateScoreDisplay;
    }

    private void Start()
    {
        UpdateScoreDisplay(ResourceBank.Instance.Score);
    }

    private void UpdateScoreDisplay(int score)
    {
        _scoreText.text = _prefix + score;
    }
}