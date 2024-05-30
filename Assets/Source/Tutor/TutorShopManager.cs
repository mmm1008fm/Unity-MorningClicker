using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorShopManager : MonoBehaviour
{
    [SerializeField] private Button _warriorButton;
    [SerializeField] private GameObject _shopWindow;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Tutor _tutor;
    [SerializeField] private TMP_Text _countTextField;
    private int _count;

    private void Awake()
    {
        _warriorButton.onClick.AddListener(OnWarrior);
        _buyButton.onClick.AddListener(OnBuy);
    }

    private void OnBuy()
    {
        if (ResourceBank.Instance.Score >= 25)
        {
            ResourceBank.Instance.Score -= 25;
            _count++;
            _countTextField.text = $"Количество: {_count}";
        }
        else
        {
            _tutor.IsWarriorsBought = true;
            _shopWindow.SetActive(false);
        }
    }

    private void OnWarrior()
    {
        _shopWindow.SetActive(true);
    }
}