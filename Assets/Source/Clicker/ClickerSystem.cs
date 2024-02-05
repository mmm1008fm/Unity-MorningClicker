using UnityEngine;
using UnityEngine.UI;

public class ClickerSystem : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    private int _score;

    public void AddScore(int value)
    {
        _score += value;
        _scoreText.text = "Score: " + _score;
    }
}
