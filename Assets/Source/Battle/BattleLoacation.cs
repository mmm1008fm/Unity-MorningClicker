using UnityEngine;

[System.Serializable]
public class BattleLoacation
{
	[field: SerializeField] public Sprite Background { get; private set; }
	[field: SerializeField] public string Name { get; private set; }
	[field: SerializeField] public string Description { get; private set; }
}