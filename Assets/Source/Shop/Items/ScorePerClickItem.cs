using UnityEngine.EventSystems;

public class ScorePerClickItem : ShopItem, IPointerDownHandler
{
    public override ShopItem Buy(int count)
    {
        ResourceBank.Instance.ScorePerClick += count;
        return this;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
		ShopManager.OpenShop(new ShopParameters(this, ResourceBank.Instance.ArmorCost, PriceIncrease, Description));
    }
}