using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBarSprite;
    [SerializeField] private float reduceSpeed;
    private float target = 1;
    
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }
    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        target = currentHealth / maxHealth;
    }

    private void Update()
    {    
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
        healthBarSprite.fillAmount = Mathf.MoveTowards(healthBarSprite.fillAmount, target, reduceSpeed*Time.deltaTime);
    }

    
}
