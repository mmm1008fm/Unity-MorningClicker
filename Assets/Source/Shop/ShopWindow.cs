using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ShopWindow
{
	public GameObject Window;
	public TMP_Text DescriptionField;
	public TMP_Text PriceField;
	public TMP_Text CountField;
	public TMP_Text CountToBuyField;
	public Slider Slider;
	public Button BuyButton;
	public Button CloseButton;
}