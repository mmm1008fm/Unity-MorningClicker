using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Этот скрипт представляет из себя предмет, который является кнопкой. Он устанавливает чего и сколько покупаем!
/// </summary>
[RequireComponent(typeof(Button))]
public class BuyShopItemsNew : MonoBehaviour, IShopItems
{
    public static Action<ShopTransaction> OnBuy;
    public int Count = 1;
    public int Price = 50;
    public int PriceIncrease = 50;
    [field: SerializeField] public ShopItems Item { get; set; }
    [SerializeField] private string _description = "У этого продукта нет описания";
    [SerializeField] private Button _buyButton;
    [SerializeField] private ShopManager _shopManager;
    private ShopParams _shopParams;

    private void OnValidate()
    {
        _buyButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        OnBuy += OnBuyMethod;
    }

    private void OnDisable()
    {
        OnBuy -= OnBuyMethod;
    }

    private void Awake()
    {
        _buyButton.onClick.AddListener(Buy);
        _shopParams = new ShopParams(_description, Price, PriceIncrease);
    }

    public void Buy()
    {
        _shopManager.OpenWindow(_shopParams, this);
    }

    private void OnBuyMethod(ShopTransaction transaction)
    {
        Debug.Log($"Совершена покупка: {transaction.Count} шт. за {transaction.Cost}$");
    }
}
