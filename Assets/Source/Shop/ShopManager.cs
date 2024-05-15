using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public BuyShopItemsNew ActiveItem { get; private set; }
    [field: SerializeField] public int Balance { get; private set; }
    [SerializeField] private GameObject _window;
    [SerializeField] private TMP_Text _descriptionField;
    [SerializeField] private TMP_Text _priceField;
    [SerializeField] private TMP_Text _countField;
    [SerializeField] private TMP_Text _countToBuyField;
    [SerializeField] private Slider _slider;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _closeButton;
    private ShopParams _shopParams;

    private void Awake()
    {
        _buyButton.onClick.AddListener(Buy);
        _closeButton.onClick.AddListener(CloseWindow);
        _window.SetActive(false);
    }

    private void Update()
    {
        if (!_window.activeSelf)
        {
            return;
        }

        var count = GetMaxItemsAffordable(ActiveItem.Price, ActiveItem.PriceIncrease, Balance);

        if (count > 1)
        {
            _slider.gameObject.SetActive(true);
            _buyButton.interactable = true;
        }
        else if (count == 0)
        {
            _slider.gameObject.SetActive(false);
            _buyButton.interactable = false;
            _slider.value = 0f;
        }
        else if (count == 1)
        {
            _slider.gameObject.SetActive(false);
            _buyButton.interactable = true;
            _slider.value = 1f;
        }
        else
        {
            throw new ArgumentException("Invalid count");
        }

        _slider.maxValue = count;
        _slider.minValue = 1f;
        _countToBuyField.text = "Купить: " + _slider.value.ToString();
        _priceField.text = '$' + CalculateTotalPrice((int)_slider.value, ActiveItem.Price, ActiveItem.PriceIncrease).ToString() + $" (база: {ActiveItem.Price})";
        _countField.text = "В наличии: " + ActiveItem.Count.ToString();
    }

    public void SetBalance(int value)
    {
        Balance = value >= 0 ? value : 0;
    }

    public void OpenWindow(ShopParams shopParams, BuyShopItemsNew item)
    {
        _shopParams = shopParams;
        _descriptionField.text = _shopParams.Description;
        // Добавить новое, при расширении класса ShopParams
        ActiveItem = item;
        _window.SetActive(true);
    }

    public void CloseWindow()
    {
        ActiveItem = null;
        _window.SetActive(false);
    }

    private void Buy()
    {    
        var count = (int)_slider.value;
        var cost = (int)_slider.value * ActiveItem.PriceIncrease;
        var price = CalculateTotalPrice((int)_slider.value, ActiveItem.Price, ActiveItem.PriceIncrease);
        Balance -= price;
        ActiveItem.Price += cost;
        ActiveItem.Count += count;
        _slider.value = 1f;
        BuyShopItemsNew.OnBuy?.Invoke(new ShopTransaction(price, count, ActiveItem.Item, ActiveItem.Price));
    }

    /// <summary>
    /// Рассчитывает общую стоимость покупки, учитывая увеличение цены
    /// </summary>
    /// <param name="itemsCount"></param>
    /// <param name="basePrice"></param>
    /// <returns></returns>
    private int CalculateTotalPrice(int itemsCount, int basePrice, int priceIncrease)
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
    private int GetMaxItemsAffordable(int basePrice, int priceIncrease, int currentScore)
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
}