using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSceneView : BaseInitializable
{
    [SerializeField] private Button _exit;
    [SerializeField] private List<Button> _buyButtons;
    [SerializeField] private Button _closeWindowForBuy;
    [SerializeField] private GameObject _windowForBuy;
    [SerializeField] private GameObject _forWarriors;
    [SerializeField] private GameObject _forArmor;
    [SerializeField] private Button _buyArmor;
    [SerializeField] private Button _buyWarriors;
    [SerializeField] private TextMeshProUGUI _ScoreText;
    [SerializeField] private TextMeshProUGUI _DefenseEffect;
    [SerializeField] private TextMeshProUGUI _WarriorsCount;

    public override void Initialize()
    {
        _exit.onClick.AddListener(Exit);
        _closeWindowForBuy.onClick.AddListener(CloseWindowForBuy);
        
        _buyArmor.onClick.AddListener(BuyArmor);
        _buyWarriors.onClick.AddListener(BuyWarriors);
        
        foreach (var buyButton in _buyButtons)
        {
            var buttonName = buyButton.name; // TODO: Верно ли?
            buyButton.onClick.AddListener(() => Buy(buttonName)); // TODO: Верно ли?
        }
        
        UpdateScoreCycle();
    }

    private async void UpdateScoreCycle() // TODO: Отказаться от тасков
    {
        while (Application.isPlaying)
        {
            _ScoreText.text = $"Score: {PlayerVariables.Score}";
            _DefenseEffect.text = $"Now: {PlayerVariables.DefensePercent * 100f}%";
            _WarriorsCount.text = $"Now: {PlayerVariables.Warriors}";
            await Task.Delay(1000);
        }
    }

    private void BuyArmor() // TODO: Убрать константы
    {
        if (PlayerVariables.Score >= 500)
        {
            PlayerVariables.DefensePercent += 0.05f;
            PlayerVariables.Score -= 500;
        }
    }

    private void BuyWarriors() // TODO: Убрать константы
    {
        if (PlayerVariables.Score >= 10)
        {
            PlayerVariables.Warriors++;
            PlayerVariables.Score -= 10;
        }
    }

    private void Exit()
    {
        SceneSwitcher.Switch("Main");
    }
    
    private void Buy(string buttonName) // TODO: Верно ли?
    {
        _windowForBuy.SetActive(true);
        switch (buttonName)
        {
            case "BuyArmor":
                _forArmor.SetActive(true);
                break;
            case "BuyWarriors":
                _forWarriors.SetActive(true);
                break;
            default:
                throw new Exception($"Shop is not included {buttonName}");
        }
    }

    private void CloseWindowForBuy()
    {
        _windowForBuy.SetActive(false);
        _forArmor.SetActive(false);
        _forWarriors.SetActive(false);
    }
}
