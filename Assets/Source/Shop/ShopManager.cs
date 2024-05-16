using System;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
	public event Action<ShopItem> OnItemBought;
	[SerializeField] private ShopWindow _shop;
	private ShopParameters _currentParameters;

	private void Awake()
	{
		_shop.CloseButton.onClick.AddListener(CloseShop);
		_shop.BuyButton.onClick.AddListener(() => Buy(_currentParameters.Item));
		_shop.Window.SetActive(false);
	}

	private void Update()
	{
		ShopWindowManage();
	}

    public void OpenShop(ShopParameters parameters)
	{
		_currentParameters = parameters;
		_shop.Window.SetActive(true);
	}

	public void CloseShop()
	{
		_currentParameters = null;
		_shop.Window.SetActive(false);
	}

	public ShopItem Buy(ShopItem item)
	{
		var price = CalculateTotalPrice((int)_shop.Slider.value, _currentParameters.Item.Price, _currentParameters.PriceIncrease);
		var chosenCount = (int)_shop.Slider.value;
		var chosenItem = item.Buy(chosenCount);

		ResourceBank.Instance.Score -= price;
		_currentParameters.Item.Price += _currentParameters.PriceIncrease;
		_shop.Slider.value = 1f;
		OnItemBought?.Invoke(chosenItem);

		return chosenItem;
	}

	/// <summary>
	/// Рассчитывает общую стоимость покупки, учитывая увеличение цены
	/// </summary>
	/// <param name="itemsCount"></param>
	/// <param name="basePrice"></param>
	/// <returns></returns>
	public static int CalculateTotalPrice(int itemsCount, int basePrice, int priceIncrease)
	{
		var totalPrice = 0;
		
		for (int i = 0; i < itemsCount; i++)
		{
			totalPrice += basePrice + (priceIncrease * i);
		}

		return totalPrice;
	}

	/// <summary>
	/// Возвращает максимальное количество покупаемых предметов, которое можно приобрести
	/// </summary>
	/// <param name="basePrice"></param>
	/// <param name="priceIncrease"></param>
	/// <param name="currentScore"></param>
	/// <returns></returns>
	public static int GetMaxItemsAffordable(int basePrice, int priceIncrease, int currentScore)
	{
		int totalCost = 0;
		int itemCount = 0;

		while (true)
		{
			int itemPrice = basePrice + (priceIncrease * itemCount);

			if (totalCost + itemPrice > currentScore)
			{
				break;
			}

			totalCost += itemPrice;
			itemCount++;
		}

		return itemCount;
	}

	/// <summary>
	/// Включает, выключает и управляет состоянием окна магазина
	/// </summary>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	private void ShopWindowManage()
    {
        if (!_shop.Window.activeSelf)
		{
			return;
		}

		var price = CalculateTotalPrice((int)_shop.Slider.value, _currentParameters.Item.Price, _currentParameters.PriceIncrease);
		var countToBuy = GetMaxItemsAffordable(_currentParameters.Item.Price, _currentParameters.PriceIncrease, ResourceBank.Instance.Score);
		var chosenCount = (int)_shop.Slider.value;

		_shop.Slider.maxValue = countToBuy;
		_shop.Slider.minValue = 1f;

		if (countToBuy == 0)
		{
			_shop.BuyButton.interactable = false;
			_shop.Slider.value = 1;
			_shop.Slider.gameObject.SetActive(false);
		}
		else if (countToBuy == 1)
		{
			_shop.BuyButton.interactable = true;
			_shop.Slider.gameObject.SetActive(false);
		}
		else if (countToBuy > 1)
		{
			_shop.BuyButton.interactable = true;
			_shop.Slider.gameObject.SetActive(true);
			
		}
		else
		{
			throw new ArgumentOutOfRangeException(nameof(chosenCount));
		}

		_shop.PriceField.text = $"${price} (${_currentParameters.Item.Price})";
		_shop.CountField.text = $"У вас в наличии: {_currentParameters.Item.Count}";
		_shop.CountToBuyField.text = $"Покупка: {chosenCount}";
		_shop.DescriptionField.text = _currentParameters.Description;
    }
}