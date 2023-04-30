using TMPro;
using UnityEngine;

public class PickupableObject : MonoBehaviour, IPickupable
{
    public string[] description = { "This is a pickupable object." };
    public Sprite sprite;
    public Sprite dotSprite;
    //Tytu³
    public string title = "Title";
    public Color titleColor = Color.white;

    public float particleLifetime = 2.0f;
    public GameObject destroyParticles; // Reference to the first particle system
    public GameObject pickupParticles;

    public void OnPickup()
    {
        // Disable all other pickupable objects in the scene
        GameObject[] otherPickupables = GameObject.FindGameObjectsWithTag("Pickupable");
        foreach (GameObject pickupable in otherPickupables)
        {
            if (pickupable != gameObject)
            {
                Destroy(pickupable);
                GameObject particleOne = Instantiate(destroyParticles, pickupable.transform.position, Quaternion.identity);
                Destroy(particleOne.gameObject, particleLifetime);
            }
        }
        Destroy(gameObject);
        GameObject particleTwo = Instantiate(pickupParticles, transform.position, Quaternion.identity);
        Destroy(particleTwo.gameObject, particleLifetime);
        Debug.Log("Masz upgrade tego: " + gameObject.name);
    }

    public string[] GetDescription()
    {
        string[] result = new string[description.Length];
        for (int i = 0; i < description.Length; i++)
        {
            result[i] = "<sprite name=" + dotSprite.name + "> " + description[i];
        }
        return result;
    }

    public string GetTitle()
    {
        return "<color=#" + ColorUtility.ToHtmlStringRGBA(titleColor) + ">" + title + "</color>";
    }
    public Sprite GetSprite()
    {
        return sprite;
    }
}

