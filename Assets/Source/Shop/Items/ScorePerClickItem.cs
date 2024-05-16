using UnityEngine.EventSystems;

public class ScorePerClickItem : ShopItem, IPointerDownHandler
{
	public override int Count
	{
		get => ResourceBank.Instance.ScorePerClick;
		set => ResourceBank.Instance.ScorePerClick = value;
	}

	public override int Price
	{
		get => ResourceBank.Instance.ScorePerClickCost;
		set => ResourceBank.Instance.ScorePerClickCost = value;
	}

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