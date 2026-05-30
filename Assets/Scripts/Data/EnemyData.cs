using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [field: SerializeField] public float maxHp { get; private set; }
    [field: SerializeField] public float moveSpeed { get; private set; }
    [field: SerializeField] public float damage { get; private set; }
    [field: SerializeField] public int crystalReward { get; private set; }
}