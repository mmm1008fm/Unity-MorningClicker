using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuyShopItems : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _effect;
	[SerializeField] private TextMeshProUGUI _costText;
	[SerializeField] private Button _buyButton;
	[SerializeField] private ShopItems _item;
	[SerializeField] private int _costAddition = 50;
	[SerializeField] private float _count = 1f;
	[SerializeField] private Slider _slider;
	[SerializeField] private TMP_Text _countShopping;
	private int _countToBuy;

	public void Awake()
	{
		_buyButton.onClick.AddListener(Buy);
	}

    private void Start()
    {
        UpdateUI();
    }

	private void Update()
	{
		UpdateUI();
	}

	private void UpdateUI()
	{
        switch (_item)
		{
			case ShopItems.Armor:
				_effect.text = $"Защита: {Math.Round(ResourceBank.Instance.DefensePercent * 100f)}%";
				_costText.text = $"Купить: {ResourceBank.Instance.ArmorCost}$";
				_slider.maxValue = ResourceBank.Instance.ArmorCost / _count;
				break;
			case ShopItems.Warrior:
                ResourceBank.Instance.WarriorCost = ResourceBank.Instance.WarriorsCount * 50; // 50 is start cost of warrior
				_effect.text = $"Воины: {ResourceBank.Instance.WarriorsCount}";
				_costText.text = $"Купить: {ResourceBank.Instance.WarriorCost}$";
				_slider.maxValue = ResourceBank.Instance.WarriorCost / _count;
				break;
			case ShopItems.PerSecond:
				_effect.text = $"Сила: {ResourceBank.Instance.ScorePerSecond}";
				_costText.text = $"Купить: {ResourceBank.Instance.ScorePerSecondCost}$";
				_slider.maxValue = ResourceBank.Instance.ScorePerSecondCost / _count;
				break;
			case ShopItems.PerClick:
				_effect.text = $"За клик: {ResourceBank.Instance.ScorePerClick}";
				_costText.text = $"Купить: {ResourceBank.Instance.ScorePerClickCost}$";
				_slider.maxValue = ResourceBank.Instance.ScorePerClickCost / _count;
				break;
		}

		_countToBuy = Mathf.FloorToInt(_slider.maxValue);
		_countShopping.text = $"Кол-во: {_countToBuy}";
    }

    private void Buy()
	{
        int cost = 0;
        switch (_item)
        {
            case ShopItems.Armor:
                cost = ResourceBank.Instance.ArmorCost;
                break;
            case ShopItems.Warrior:
                cost = ResourceBank.Instance.WarriorCost;
                break;
            case ShopItems.PerSecond:
                cost = ResourceBank.Instance.ScorePerSecondCost;
                break;
            case ShopItems.PerClick:
                cost = ResourceBank.Instance.ScorePerClickCost;
                break;
        }

        if (ResourceBank.Instance.Score < cost)
		{
			return;
		}

		ResourceBank.Instance.Score -= cost * _countToBuy;
		
		switch (_item)
		{
			case ShopItems.Armor:
				ResourceBank.Instance.DefensePercent += _count * _countToBuy;
                ResourceBank.Instance.ArmorCost += _costAddition * _countToBuy;
				break;
			case ShopItems.Warrior:
				ResourceBank.Instance.WarriorsCount += (int)_count * _countToBuy;
				break;
			case ShopItems.PerSecond:
				ResourceBank.Instance.ScorePerSecond += (int)_count * _countToBuy;
                ResourceBank.Instance.ScorePerSecondCost += _costAddition * _countToBuy;
				break;
			case ShopItems.PerClick:
				ResourceBank.Instance.ScorePerClick += (int)_count * _countToBuy;
                ResourceBank.Instance.ScorePerClickCost += _costAddition * _countToBuy;
				break;
		}
		
		UpdateUI();
    }
}
