public class ShopParameters
{
	public ShopItem Item { get; private set; }
	public int Price { get; private set; }
	public int PriceIncrease { get; private set; }
	public string Description { get; private set; }

	public ShopParameters(ShopItem item, int price, int priceIncrease, string description)
	{
		Item = item;
		Price = price;
		PriceIncrease = priceIncrease;
		Description = description;
	}
}