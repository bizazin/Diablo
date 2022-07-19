using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewStats", menuName = "ItemStats")]
public class StatsValuesContainer : ScriptableObject
{
    public Vector2Int[] rarityChanceRange;
    public Vector2Int[] defenceRange;
    public Vector2Int[] speedRange;
    public Vector2Int[] damageRange;
    public Vector2Int[] criticalChanceRange;
    public Vector2Int[] criticalDamageRange;
    public float[] statsMultiplier;
}
