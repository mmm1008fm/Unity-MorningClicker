public class ShopParams
{
	public int Price {get; set; }
	public string Description {get; private set; }
	public int PriceIncrease {get; private set; }

	public ShopParams(string description, int basePrice, int priceIncrease)
	{
		Price = basePrice;
		Description = description;
		PriceIncrease = priceIncrease;
	}
}