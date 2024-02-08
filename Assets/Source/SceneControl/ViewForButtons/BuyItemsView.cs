using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuyItemsView : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _effect;
	[SerializeField] private Button _buyButton;
	[SerializeField] private ShopItem _item;
	[SerializeField] private int _cost;
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
				_effect.text = $"Defense: {PlayerVariables.DefensePercent * 100f}%";
				break;
			case ShopItem.Warrior:
				_effect.text = $"Warriors count: {PlayerVariables.Warriors}";
				break;
			case ShopItem.Windmill:
				_effect.text = $"Windmill power: {PlayerVariables.WindmillPower}";
				break;
			case ShopItem.PerClick:
				_effect.text = $"Per click: {PlayerVariables.PerClick}";
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
