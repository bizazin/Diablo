using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewStats", menuName = "ItemStats")]
public class StatsValuesContainer : ScriptableObject
{
    public Vector2Int[] rarityChanceRange;
    public Vector2Int[] statValuesRange;
    public Vector2Int[] criticalChanceRange;
    public float[] statsMultiplier;
}
