public class ShopParams
{
	public string Description {get; private set; }
	public int BasePrice {get; private set; }
	public int PriceIncrease {get; private set; }
	public ShopItem Item { get; private set; }

	public ShopParams(string description, int basePrice, int priceIncrease, ShopItem item)
	{
		Description = description;
		BasePrice = basePrice;
		PriceIncrease = priceIncrease;
		Item = item;
	}
}