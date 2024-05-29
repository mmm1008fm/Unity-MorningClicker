using UnityEngine;

[System.Serializable]
public class BrefingInfo
{
    [field: SerializeField] public BattleLoacation Location { get; private set; }
    [field: SerializeField] public GameObject[] EnemyTypePrefabs { get; private set; }
    [field: SerializeField] public int EnemyArmor { get; private set; }
    [field: SerializeField] public int EnemyWarriors { get; private set; }
    [field: SerializeField] public BattleArtefact ActualArtefact { get; private set; }
}