
using UnityEngine;

public interface IPickupable
{
    void OnPickup();
    string[] GetDescription();
    string GetTitle();
    Sprite GetSprite();


}
