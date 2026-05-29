using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public float maxHp;
    public float moveSpeed;
    public float damage;

    public int Reward;
}