public class ShopTransaction
{
	public int Count { get; set; }
	public int Cost { get; set; }

	public ShopTransaction(int cost, int count)
	{
		Count = count;
		Cost = cost;
	}
}