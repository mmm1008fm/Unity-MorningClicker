using UnityEngine;

public abstract class ShopItem : MonoBehaviour
{
	public abstract int Count { get; set; }
	public abstract int Price { get; set; }

	[field: SerializeField] public string Name { get; private set; }
	[field: SerializeField] public string Description { get; private set; }
	[field: SerializeField] protected int PriceIncrease { get; private set; }
	[field: SerializeField] protected ShopManager ShopManager { get; private set; }

	public abstract ShopItem Buy(int count);
}