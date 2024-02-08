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
	[SerializeField] private float _costMultiplier = 2f;
	[SerializeField] private float _count = 1f;
	[SerializeField] private int _defaultMinCost = 10;

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
				_startCost = PlayerPrefs.GetInt("ArmorCost", _defaultMinCost);
				_effect.text = $"Defense: {Math.Round(PlayerVariables.DefensePercent * 100f)}%";
				_costText.text = $"Buy armor {_startCost}$";
				break;
			case ShopItem.Warrior:
				_startCost = PlayerPrefs.GetInt("WarriorCost", _defaultMinCost);
				_effect.text = $"Warriors count: {PlayerVariables.Warriors}";
				_costText.text = $"Buy warrior {_startCost}$";
				break;
			case ShopItem.Windmill:
				_startCost = PlayerPrefs.GetInt("WindmillCost", _defaultMinCost);
				_effect.text = $"Windmill power: {PlayerVariables.WindmillPower}";
				_costText.text = $"Buy windmill {_startCost}$";
				break;
			case ShopItem.PerClick:
				_startCost = PlayerPrefs.GetInt("ClickCost", _defaultMinCost);
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
                PlayerPrefs.SetInt("ArmorCost", _startCost);
				break;
			case ShopItem.Warrior:
				PlayerVariables.Warriors += (int)_count;
                PlayerPrefs.SetInt("WarriorCost", _startCost);
				break;
			case ShopItem.Windmill:
				PlayerVariables.WindmillPower += (int)_count;
                PlayerPrefs.SetInt("WindmillCost", _startCost);
				break;
			case ShopItem.PerClick:
				PlayerVariables.PerClick += (int)_count;
                PlayerPrefs.SetInt("ClickCost", _startCost);
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
		
		UpdateUI();
	}
}
