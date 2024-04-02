using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuyItemsView : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _effect;
	[SerializeField] private TextMeshProUGUI _costText;
	[SerializeField] private Button _buyButton;
	[SerializeField] private ShopItem _item;
	[SerializeField] private int _startCost = 10;
	[SerializeField] private int _costAddition = 50;
	[SerializeField] private float _count = 1f;
	[SerializeField] private int _defaultMinCost = 10;
	[SerializeField] private int _cost;

	public void Awake()
	{
		_buyButton.onClick.AddListener(Buy);
	}

	private void OnEnable()
	{
		UpdateUI();
	}

	private void UpdateUI()
	{
		_cost = PlayerPrefs.GetInt("ArmorCost", _defaultMinCost);
		_cost = PlayerPrefs.GetInt("WarriorCost", _defaultMinCost);
		_cost = PlayerPrefs.GetInt("WindmillCost", _defaultMinCost);
		_cost = PlayerPrefs.GetInt("ClickCost", _defaultMinCost);

		if (_item is ShopItem.Warrior)
		{
			_cost = PlayerVariables.Warriors * _startCost;
		}

		if (_cost == 0)
		{
			_cost = _startCost;
		}

		switch (_item)
		{
			case ShopItem.Armor:
				_effect.text = $"Защита: {Math.Round(PlayerVariables.DefensePercent * 100f)}%";
				_costText.text = $"Купить: {_cost}$";
				break;
			case ShopItem.Warrior:
				_effect.text = $"Воины: {PlayerVariables.Warriors}";
				_costText.text = $"Купить: {_cost}$";
				break;
			case ShopItem.Windmill:
				_effect.text = $"Сила: {PlayerVariables.WindmillPower}";
				_costText.text = $"Купить: {_cost}$";
				break;
			case ShopItem.PerClick:
				_effect.text = $"За клик: {PlayerVariables.PerClick}";
				_costText.text = $"Купить: {_cost}$";
				break;
		}
	}

	private void Buy()
	{
		if (PlayerVariables.Score < _cost)
		{
			return;
		}

		PlayerVariables.Score -= _cost;
		_cost += _costAddition;
		
		switch (_item)
		{
			case ShopItem.Armor:
				PlayerVariables.DefensePercent += _count;
                PlayerPrefs.SetInt("ArmorCost", _cost);
				break;
			case ShopItem.Warrior:
				PlayerVariables.Warriors += (int)_count;
                PlayerPrefs.SetInt("WarriorCost", _cost);
				break;
			case ShopItem.Windmill:
				PlayerVariables.WindmillPower += (int)_count;
                PlayerPrefs.SetInt("WindmillCost", _cost);
				break;
			case ShopItem.PerClick:
				PlayerVariables.PerClick += (int)_count;
                PlayerPrefs.SetInt("ClickCost", _cost);
				break;
		}
		
		UpdateUI();
	}
}
