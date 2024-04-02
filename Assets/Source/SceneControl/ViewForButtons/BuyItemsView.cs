using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Collections;

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
	[SerializeField, ReadOnly] private int _cost;

	public void Awake()
	{
		if (_item == ShopItem.Warrior)
		{
			_cost = PlayerVariables.Warriors * _startCost;
		}

		_buyButton.onClick.AddListener(Buy);
		UpdateUI();
	}

	private void OnEnable()
	{
		UpdateUI();
	}

	private void UpdateUI()
	{
		switch (_item)
		{
			case ShopItem.Armor:
				_cost = PlayerPrefs.GetInt("ArmorCost", _defaultMinCost);
				_effect.text = $"Защита: {Math.Round(PlayerVariables.DefensePercent * 100f)}%";
				_costText.text = $"Купить: {_cost}$";
				break;
			case ShopItem.Warrior:
				_cost = PlayerPrefs.GetInt("WarriorCost", _defaultMinCost);
				_effect.text = $"Воины: {PlayerVariables.Warriors}";
				_costText.text = $"Купить: {_cost}$";
				break;
			case ShopItem.Windmill:
				_cost = PlayerPrefs.GetInt("WindmillCost", _defaultMinCost);
				_effect.text = $"Сила: {PlayerVariables.WindmillPower}";
				_costText.text = $"Купить: {_cost}$";
				break;
			case ShopItem.PerClick:
				_cost = PlayerPrefs.GetInt("ClickCost", _defaultMinCost);
				_effect.text = $"За клик: {PlayerVariables.PerClick}";
				_costText.text = $"Купить: {_cost}$";
				break;
			default:
				throw new ArgumentOutOfRangeException();
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
			default:
				throw new ArgumentOutOfRangeException();
		}
		
		UpdateUI();
	}
}
