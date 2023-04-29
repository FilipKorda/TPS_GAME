
using UnityEngine;

public interface IPickupable
{
    void OnPickup();
    string GetDescription();
    Sprite GetSprite();

}
