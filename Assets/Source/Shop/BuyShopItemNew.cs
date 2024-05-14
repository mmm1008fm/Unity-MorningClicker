using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Этот скрипт представляет из себя предмет, который является кнопкой. Он устанавливает чего и сколько покупаем!
/// </summary>
[RequireComponent(typeof(Button))]
public class BuyShopItemNew : MonoBehaviour, IShopItem
{
    [SerializeField] private string _description = "У этого продукта нет описания";
    [SerializeField] private int _basePrice = 50;
    [SerializeField] private int _priceIncrease = 50;
    [SerializeField] private Button _buyButton;
    [SerializeField] private ShopManager _shopManager;
    [SerializeField] private ShopItem _shopItem;
    private ShopParams _shopParams;

    private void OnValidate()
    {
        _buyButton = GetComponent<Button>();
    }

    private void Awake()
    {
        _buyButton.onClick.AddListener(Buy);
        _shopParams = new ShopParams(_description, _basePrice, _priceIncrease, _shopItem);
    }

    public void Buy()
    {
        _shopManager.OpenWindow(_shopParams);
    }
}
