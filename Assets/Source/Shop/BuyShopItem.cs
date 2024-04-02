using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuyShopItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _effect;
	[SerializeField] private TextMeshProUGUI _costText;
	[SerializeField] private Button _buyButton;
	[SerializeField] private ShopItem _item;
	[SerializeField] private int _costAddition = 50;
	[SerializeField] private float _count = 1f;

	public void Awake()
	{
		_buyButton.onClick.AddListener(Buy);
	}

    private void Start()
    {
        UpdateUI();
    }

	private void UpdateUI()
	{
        // if (ResourceBank.Instance.ArmorCost <= 0 || ResourceBank.Instance.WarriorCost <= 0 || ResourceBank.Instance.ScorePerClickCost <= 0 || ResourceBank.Instance.ScorePerSecondCost <= 0)
        // {
        //     switch (_item)
        //     {
        //         case ShopItem.Armor:
        //             ResourceBank.Instance.ArmorCost = _startCost;
        //             break;
        //         case ShopItem.Warrior:
        //             ResourceBank.Instance.WarriorCost = _startCost;
        //             break;
        //         case ShopItem.Windmill:
        //             ResourceBank.Instance.ScorePerSecondCost = _startCost;
        //             break;
        //         case ShopItem.PerClick:
        //             ResourceBank.Instance.ScorePerClickCost = _startCost;
        //             break;
        //     }
        // }

        switch (_item)
		{
			case ShopItem.Armor:
				_effect.text = $"Защита: {Math.Round(ResourceBank.Instance.DefensePercent * 100f)}%";
				_costText.text = $"Купить: {ResourceBank.Instance.ArmorCost}$";
				break;
			case ShopItem.Warrior:
                ResourceBank.Instance.WarriorCost = ResourceBank.Instance.WarriorsCount * 50; // 50 is start cost of warrior
				_effect.text = $"Воины: {ResourceBank.Instance.WarriorsCount}";
				_costText.text = $"Купить: {ResourceBank.Instance.WarriorCost}$";
				break;
			case ShopItem.Windmill:
				_effect.text = $"Сила: {ResourceBank.Instance.ScorePerSecond}";
				_costText.text = $"Купить: {ResourceBank.Instance.ScorePerSecondCost}$";
				break;
			case ShopItem.PerClick:
				_effect.text = $"За клик: {ResourceBank.Instance.ScorePerClick}";
				_costText.text = $"Купить: {ResourceBank.Instance.ScorePerClickCost}$";
				break;
		}
    }

    private void Buy()
	{
        int cost = 0;
        switch (_item)
        {
            case ShopItem.Armor:
                cost = ResourceBank.Instance.ArmorCost;
                break;
            case ShopItem.Warrior:
                cost = ResourceBank.Instance.WarriorCost;
                break;
            case ShopItem.Windmill:
                cost = ResourceBank.Instance.ScorePerSecondCost;
                break;
            case ShopItem.PerClick:
                cost = ResourceBank.Instance.ScorePerClickCost;
                break;
        }

        if (ResourceBank.Instance.Score < cost)
		{
			return;
		}

		ResourceBank.Instance.Score -= cost;
		
		switch (_item)
		{
			case ShopItem.Armor:
				ResourceBank.Instance.DefensePercent += _count;
                ResourceBank.Instance.ArmorCost += _costAddition;
				break;
			case ShopItem.Warrior:
				ResourceBank.Instance.WarriorsCount += (int)_count;
				break;
			case ShopItem.Windmill:
				ResourceBank.Instance.ScorePerSecond += (int)_count;
                ResourceBank.Instance.ScorePerSecondCost += _costAddition;
				break;
			case ShopItem.PerClick:
				ResourceBank.Instance.ScorePerClick += (int)_count;
                ResourceBank.Instance.ScorePerClickCost += _costAddition;
				break;
		}
		
		UpdateUI();
    }
}
