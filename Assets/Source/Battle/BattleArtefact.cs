using UnityEngine;


[CreateAssetMenu(fileName = "NewBattleArtefact", menuName = "BattleItems/BattleArtefact")]
public class BattleArtefact : ScriptableObject
{
	[field: SerializeField] public string Name { get; private set; }
	[field: SerializeField] public string Description { get; private set; }
	[field: SerializeField] public Sprite Item { get; private set; }
}

public abstract class ArtefactBonus
{
	public abstract void Activate();
}

public class ScorePerSecondBonus : ArtefactBonus
{
	public int Effect { get; private set; }

	public ScorePerSecondBonus(int effect)
	{
		Effect = effect;
	}

	public override void Activate()
	{
		ResourceBank.Instance.ScorePerSecond += Effect;
	}
}