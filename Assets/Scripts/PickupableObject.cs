using TMPro;
using UnityEngine;

public class PickupableObject : MonoBehaviour, IPickupable
{
    public string[] description = { "This is a pickupable object." };
    public Sprite sprite;

    //Tytu³
    public string title = "Title";
    public VertexGradient titleColor;
    


    public void OnPickup()
    {
        // Disable all other pickupable objects in the scene
        GameObject[] otherPickupables = GameObject.FindGameObjectsWithTag("Pickupable");
        foreach (GameObject pickupable in otherPickupables)
        {
            if (pickupable != gameObject)
            {
                Destroy(pickupable);
            }
        }
        Destroy(gameObject);
        // Do the pickup action for the selected object
        Debug.Log("Masz upgrade tego: " + gameObject.name);
    }
    public string[] GetDescription()
    {
        return description;
    }
    public string GetTitle()
    {
        return title;
    }
    public Sprite GetSprite()
    {
        return sprite;
    }

}
