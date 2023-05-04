using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseAttackSpeed : MonoBehaviour, IPickupable
{
    private float _timeBetweenShots = 0.5f;
    public int cost = 0;
    public void IncreaseAttackSpeedOfPlayer(PlayerController playerController)
    {
        playerController._timeBetweenShots *= 0.25f;
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
