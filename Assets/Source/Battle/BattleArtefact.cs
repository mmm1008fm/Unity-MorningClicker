using UnityEngine;

[CreateAssetMenu(fileName = "NewBattleArtefact", menuName = "BattleItems/BattleArtefact")]
public class BattleArtefact : ScriptableObject
{
	[field: SerializeField] public string Name { get; private set; }
	[field: SerializeField] public string Description { get; private set; }
	[field: SerializeField] public Sprite Item { get; private set; }
}