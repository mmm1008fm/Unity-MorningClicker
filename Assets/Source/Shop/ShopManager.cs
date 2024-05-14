using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject _window;
    [SerializeField] private TMP_Text _descriptionField;
    [SerializeField] private TMP_Text _priceField;
    [SerializeField] private TMP_Text _countField;
    [SerializeField] private TMP_Text _countToBuyField;
    [SerializeField] private Slider _slider;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _closeButton;
    private ShopItem _activeItem;
    private ShopParams _shopParams;
    private int _basePrice;
    private int _itemsThatCanBeBoughtCount;
    private int _priceIncrease;

    private void Awake()
    {
        _buyButton.onClick.AddListener(Buy);
        _closeButton.onClick.AddListener(CloseWindow);
        _slider.onValueChanged.AddListener(OnSliderValueChanged);
        _window.SetActive(false);
    }

    private void Update()
    {
        if (!_window.activeSelf)
        {
            return;
        }

        _descriptionField.text = _shopParams.Description;
        _basePrice = _shopParams.BasePrice;
        _priceIncrease = _shopParams.PriceIncrease;
        _activeItem = _shopParams.Item;
        // Остановился на том, что нихрена ничего не меняется. Код сверху - подумать. Остальная логика - исправить

        switch (_activeItem)
        {
            case ShopItem.Armor:
                _countField.text = $"Защита: {ResourceBank.Instance.DefensePercent}%";
                break;
            case ShopItem.Warrior:
                _countField.text = $"Воины: {ResourceBank.Instance.WarriorsCount}";
                break;
            case ShopItem.Windmill:
                _countField.text = $"Счёт в секунду: {ResourceBank.Instance.ScorePerSecond}";
                break;
            case ShopItem.PerClick:
                _countField.text = $"За клик: {ResourceBank.Instance.ScorePerClick}";
                break;
        }

        _itemsThatCanBeBoughtCount = GetMaxItemsAffordable(_basePrice, _priceIncrease, ResourceBank.Instance.Score);

        if (_itemsThatCanBeBoughtCount > 1)
        {
            _slider.gameObject.SetActive(true);
            _slider.maxValue = _itemsThatCanBeBoughtCount;
            _slider.minValue = 1;
        }
        else
        {
            _slider.gameObject.SetActive(false);
        }

        _countToBuyField.text = "Покупка: " + _slider.value;
    }

    public void OpenWindow(ShopParams shopParams)
    {
        _shopParams = shopParams;
        _window.SetActive(true);
    }

    public void CloseWindow()
    {
        _window.SetActive(false);
    }

    private void OnSliderValueChanged(float count)
    {
        _priceField.text = '$' + CalculateTotalPrice((int)count, _basePrice).ToString();
    }

    private void Buy()
    {
        var price = CalculateTotalPrice((int)_slider.value, _basePrice);
        
        if (ResourceBank.Instance.Score >= price)
        {
            ResourceBank.Instance.Score -= price;
        }
    }

    /// <summary>
    /// Рассчитывает общую стоимость покупки, учитывая увеличение цены
    /// </summary>
    /// <param name="itemsCount"></param>
    /// <param name="basePrice"></param>
    /// <returns></returns>
    private int CalculateTotalPrice(int itemsCount, int basePrice)
    {
        var totalPrice = 0;
        
        for (int i = 0; i < itemsCount; i++)
        {
            totalPrice += basePrice + (_priceIncrease * i);
        }

        return totalPrice;
    }

    /// <summary>
    /// Возвращает максимальное количество покупаемых предметов, которое можно приобрести
    /// </summary>
    /// <param name="basePrice"></param>
    /// <param name="priceIncrease"></param>
    /// <param name="currentMoney"></param>
    /// <returns></returns>
    private int GetMaxItemsAffordable(int basePrice, int priceIncrease, int currentMoney)
    {
        int totalCost = 0;
        int itemCount = 0;

        while (true)
        {
            int itemPrice = basePrice + (priceIncrease * itemCount);
            if (totalCost + itemPrice > currentMoney)
            {
                break;
            }
            
            totalCost += itemPrice;
            itemCount++;
        }

        return itemCount;
    }
}