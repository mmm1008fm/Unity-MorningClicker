using UnityEngine.EventSystems;

public class ScorePerSecondItem : ShopItem, IPointerDownHandler
{
	public override int Count
	{
		get => ResourceBank.Instance.ScorePerSecond;
		set => ResourceBank.Instance.ScorePerSecond = value;
	}

	public override int Price
	{
		get => ResourceBank.Instance.ScorePerSecondCost;
		set => ResourceBank.Instance.ScorePerSecondCost = value;
	}

	public override ShopItem Buy(int count)
	{
		ResourceBank.Instance.ScorePerSecond += count;
		return this;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		ShopManager.OpenShop(new ShopParameters(this, ResourceBank.Instance.ArmorCost, PriceIncrease, Description));
	}
}