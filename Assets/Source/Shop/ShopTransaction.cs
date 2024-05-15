public class ShopTransaction
{
	public int Count { get; set; }
	public int Cost { get; set; }
	public int NewItemCost { get; set; }
	public ShopItems Item { get; set; }

	public ShopTransaction(int cost, int count, ShopItems item, int newItemCost)
	{
		Count = count;
		Cost = cost;
		Item = item;
		NewItemCost = newItemCost;
	}
}