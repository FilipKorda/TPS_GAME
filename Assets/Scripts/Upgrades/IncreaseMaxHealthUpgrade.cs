using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseMaxHealthUpgrade : MonoBehaviour, IPickupable
{
    public int healthToAdd = 50;
    public int cost = 50;

    public void IncreaseMaxHealth(PlayerHealth playerHealth)
    {
        playerHealth.maxHealth += healthToAdd;
        //playerHealth.currHealth = playerHealth.maxHealth;
        playerHealth.UpdateHealthBar();
    }

    public int GetCost()
    {
        return cost;
    }
    public void OnPickup() { }
    public string[] GetDescription() { return new string[0]; }
    public string GetTitle() { return ""; }
    public Sprite GetSprite() { return null; }
}
