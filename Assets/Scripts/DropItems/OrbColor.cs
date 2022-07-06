using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbColor : MonoBehaviour
{

    public ParticleSystem[] myColors;
    public Color[] newColors;
    public int[] alphas;
    private void Start()
    {
        var rarity = GetComponentInParent<ItemPickup>().item.Stats.rarity;
        int idColor = (int)rarity;
        foreach (var color in myColors)
        {
            var settings = color.main;
            var tempTransparent = settings.startColor.color.a;
            settings.startColor = new ParticleSystem.MinMaxGradient(newColors[idColor]);
        }

        for (int i = 0; i < myColors.Length; i++)
        {
            
        }
        
        
        // ParticleSystem.MainModule settings = GetComponent<ParticleSystem>().main;
       // settings.startColor = new ParticleSystem.MinMaxGradient( Color.blue );
    }
}
