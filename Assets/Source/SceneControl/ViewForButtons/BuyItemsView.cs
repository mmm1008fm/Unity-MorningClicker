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
	[SerializeField] private int _startCost;
	[SerializeField] private float _costMultiplier;
	[SerializeField] private float _count;

	public void Awake()
	{
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
				_effect.text = $"Defense: {Math.Round(PlayerVariables.DefensePercent * 100f)}%";
				_costText.text = $"Buy armor {_startCost}$";
				break;
			case ShopItem.Warrior:
				_effect.text = $"Warriors count: {PlayerVariables.Warriors}";
				_costText.text = $"Buy warrior {_startCost}$";
				break;
			case ShopItem.Windmill:
				_effect.text = $"Windmill power: {PlayerVariables.WindmillPower}";
				_costText.text = $"Buy windmill {_startCost}$";
				break;
			case ShopItem.PerClick:
				_effect.text = $"Per click: {PlayerVariables.PerClick}";
				_costText.text = $"Buy click {_startCost}$";
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	private void Buy()
	{
		if (PlayerVariables.Score < _startCost)
		{
			return;
		}

		PlayerVariables.Score -= _startCost;
		_startCost *= (int)_costMultiplier;
		
		switch (_item)
		{
			case ShopItem.Armor:
				PlayerVariables.DefensePercent += _count;
				break;
			case ShopItem.Warrior:
				PlayerVariables.Warriors += (int)_count;
				break;
			case ShopItem.Windmill:
				PlayerVariables.WindmillPower += (int)_count;
				break;
			case ShopItem.PerClick:
				PlayerVariables.PerClick += (int)_count;
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
		
		UpdateUI();
	}
}
