using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashUpgrade : MonoBehaviour, IPickupable
{
    public int cost = 50;
    public void PlayerNowHaveDash(PlayerController playerController)
    {
        playerController.isDashUpgradeIsActive = true;
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
