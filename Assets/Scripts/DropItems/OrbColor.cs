using UnityEngine;

public class OrbColor : MonoBehaviour
{
    public ParticleSystem[] MyColors;
    public Color[] NewColors;

    public void SetColor()
    {
        var rarity = GetComponentInParent<ItemPickup>().Item.Stats.Rar;
        int idColor = (int)rarity;
        foreach (var color in MyColors)
        {
            var settings = color.main;
            settings.startColor = new ParticleSystem.MinMaxGradient(NewColors[idColor]);
        }
    }
}
