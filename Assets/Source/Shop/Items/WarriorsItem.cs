using UnityEngine.EventSystems;

public class WarriorsItem : ShopItem, IPointerDownHandler
{
	public override int Count
	{
		get => ResourceBank.Instance.Warriors;
		set => ResourceBank.Instance.Warriors = value;
	}

	public override int Price
	{
		get => ResourceBank.Instance.WarriorCost;
		set => ResourceBank.Instance.WarriorCost = value;
	}

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