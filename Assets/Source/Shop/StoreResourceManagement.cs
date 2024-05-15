using UnityEngine;

public class StoreResourceManagement : MonoBehaviour
{
    [SerializeField] private ShopManager _shopManager;
    private ResourceBank _resourceBank;

    private void Start()
    {
        _resourceBank = ResourceBank.Instance;
    }

    private void OnEnable()
    {
        BuyShopItemsNew.OnBuy += OnItemBuy;
    }

    private void OnDisable()
    {
        BuyShopItemsNew.OnBuy -= OnItemBuy;
    }

    private void Update()
    {
        _shopManager.SetBalance(_resourceBank.Score);
        
        if (_shopManager.ActiveItem != null)
        {
            Debug.Log("ActiveItem: " + _shopManager.ActiveItem.Item);

            switch (_shopManager.ActiveItem.Item)
            {
                case ShopItems.Armor:
                    _shopManager.ActiveItem.Price = _resourceBank.ArmorCost;
                    _shopManager.ActiveItem.Count = (int)_resourceBank.DefensePercent;
                    break;
                case ShopItems.Warrior:
                    _shopManager.ActiveItem.Price = _resourceBank.WarriorCost;
                    _shopManager.ActiveItem.Count = _resourceBank.WarriorsCount;
                    break;
                case ShopItems.PerClick:
                    _shopManager.ActiveItem.Price = _resourceBank.ScorePerClickCost;
                    _shopManager.ActiveItem.Count = _resourceBank.ScorePerClick;
                    break;
                case ShopItems.PerSecond:
                    _shopManager.ActiveItem.Price = _resourceBank.ScorePerSecondCost;
                    _shopManager.ActiveItem.Count = _resourceBank.ScorePerSecond;
                    break;
            }
        }
    }

    private void OnItemBuy(ShopTransaction transaction)
    {
        _resourceBank.Score -= transaction.Cost;
        _shopManager.ActiveItem.Price = transaction.NewItemCost;

        switch (transaction.Item)
        {
            case ShopItems.Armor:
                _resourceBank.DefensePercent += transaction.Count;
                _resourceBank.ArmorCost = transaction.NewItemCost;
                break;
            case ShopItems.Warrior:
                _resourceBank.WarriorsCount += transaction.Count;
                _resourceBank.WarriorCost = transaction.NewItemCost;
                break;
            case ShopItems.PerClick:
                _resourceBank.ScorePerClick += transaction.Count;
                _resourceBank.ScorePerClickCost = transaction.NewItemCost;
                break;
            case ShopItems.PerSecond:
                _resourceBank.ScorePerSecond += transaction.Count;
                _resourceBank.ScorePerSecondCost = transaction.NewItemCost;
                break;
        }
    }
}