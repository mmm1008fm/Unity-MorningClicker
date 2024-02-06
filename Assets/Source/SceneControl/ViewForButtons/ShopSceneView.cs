using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSceneView : BaseInitializable
{
    [SerializeField] private Button _exit;
    [SerializeField] private Button _openWarriors;
    [SerializeField] private Button _openArmor;
    [SerializeField] private Button _buyArmor;
    [SerializeField] private Button _buyWarriors;
    [SerializeField] private Button _closeWindowForBuy;
    [SerializeField] private GameObject _windowForBuy;
    [SerializeField] private GameObject _forWarriors;
    [SerializeField] private GameObject _forArmor;
    [SerializeField] private TextMeshProUGUI _ScoreText;
    [SerializeField] private TextMeshProUGUI _DefenseEffect;
    [SerializeField] private TextMeshProUGUI _WarriorsCount;

    public override void Initialize()
    {
        _exit.onClick.AddListener(Exit);
        _closeWindowForBuy.onClick.AddListener(CloseWindowForBuy);
        
        _buyArmor.onClick.AddListener(BuyArmor);
        _buyWarriors.onClick.AddListener(BuyWarriors);
        
        _openArmor.onClick.AddListener(OpenArmor);
        _openWarriors.onClick.AddListener(OpenWarriors);
    }

    private void BuyArmor() // TODO: Убрать константы
    {
        if (PlayerVariables.Score >= 500)
        {
            PlayerVariables.DefensePercent += 0.05f;
            PlayerVariables.Score -= 500;
            _ScoreText.text = $"Score: {PlayerVariables.Score}";
            _DefenseEffect.text = $"Now: {PlayerVariables.DefensePercent * 100f}%";
        }
    }

    private void BuyWarriors() // TODO: Убрать константы
    {
        if (PlayerVariables.Score >= 10)
        {
            PlayerVariables.Warriors++;
            PlayerVariables.Score -= 10;
            _ScoreText.text = $"Score: {PlayerVariables.Score}";
            _WarriorsCount.text = $"Now: {PlayerVariables.Warriors}";
        }
    }

    private void Exit()
    {
        SceneSwitcher.Switch("Main");
    }
    
    private void OpenWarriors()
    {
        _windowForBuy.SetActive(true);
        _forWarriors.SetActive(true);
    }
    
    private void OpenArmor()
    {
        _windowForBuy.SetActive(true);
        _forArmor.SetActive(true);
    }

    private void CloseWindowForBuy()
    {
        _windowForBuy.SetActive(false);
        _forArmor.SetActive(false);
        _forWarriors.SetActive(false);
    }
}
