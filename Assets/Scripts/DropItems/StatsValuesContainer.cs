using UnityEngine;

[CreateAssetMenu(fileName = "NewStats", menuName = "ItemStats")]
public class StatsValuesContainer : ScriptableObject
{
    public Vector2Int[] RarityChanceRange;
    public Vector2Int[] DefenceRange;
    public Vector2Int[] SpeedRange;
    public Vector2Int[] DamageRange;
    public Vector2Int[] CriticalChanceRange;
    public Vector2Int[] CriticalDamageRange;

    public float[] StatsMultiplier;
}
