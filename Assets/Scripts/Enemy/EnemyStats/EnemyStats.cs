using UnityEngine;

[CreateAssetMenu(menuName = "AI/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public float WalkSpeed;
    public float RunSpeed;
    public float AttackRate;
    public int Damage;
    public int MaxHealth;
    public int SearchDuration;
    public int SearchingTurnSpeed;
}
