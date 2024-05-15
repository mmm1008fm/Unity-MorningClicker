using UnityEngine.EventSystems;

public class ArmorItem : ShopItem, IPointerDownHandler
{
    public override ShopItem Buy(int count)
    {
        ResourceBank.Instance.Armor += count;
        return this;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
		ShopManager.OpenShop(new ShopParameters(this, ResourceBank.Instance.ArmorCost, PriceIncrease, Description));
    }
}