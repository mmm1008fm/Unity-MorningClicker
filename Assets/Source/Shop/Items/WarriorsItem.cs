using UnityEngine.EventSystems;

public class WarriorsItem : ShopItem, IPointerDownHandler
{
    public override ShopItem Buy(int count)
    {
        ResourceBank.Instance.Warriors += count;
        return this;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
		ShopManager.OpenShop(new ShopParameters(this, ResourceBank.Instance.ArmorCost, PriceIncrease, Description));
    }
}