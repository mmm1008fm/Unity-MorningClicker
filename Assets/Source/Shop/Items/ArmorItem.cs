using UnityEngine.EventSystems;

public class ArmorItem : ShopItem, IPointerDownHandler
{
	public override int Count
	{
		get => ResourceBank.Instance.Armor;
		set => ResourceBank.Instance.Armor = value;
	}

    public override int Price
	{
		get => ResourceBank.Instance.ArmorCost;
		set => ResourceBank.Instance.ArmorCost = value;
	}

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