using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDamage : MonoBehaviour, IPickupable
{
    public int damageToAdd = 5;
    public int cost = 0;
    public void AddDamageToPlayer(ExperienceSystem experienceSystem)
    {
        experienceSystem.baseDamage += damageToAdd;
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