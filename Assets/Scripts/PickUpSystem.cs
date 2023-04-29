using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickUpSystem : MonoBehaviour
{
    public float pickupRange = 2f; // the range in which the player can pick up objects
    public KeyCode interactKey = KeyCode.E; // the key to press to pick up objects
    public TextMeshProUGUI descriptionText;
    public Image spriteImage;
    private GameObject currentObject; // the object that the player is currently able to pick up

    public TeleportationToItemRoom teleportationToItemRoom;
    void Start()
    {
        spriteImage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            if (currentObject != null)
            {
                IPickupable pickupable = currentObject.GetComponent<IPickupable>();
                if (pickupable != null)
                {
                    pickupable.OnPickup();
                    descriptionText.text = "";
                    spriteImage.sprite = null;
                    spriteImage.gameObject.SetActive(false);
                    teleportationToItemRoom.CleanUpList(teleportationToItemRoom.list1);
                    teleportationToItemRoom.CleanUpList(teleportationToItemRoom.list2);
                    teleportationToItemRoom.CleanUpList(teleportationToItemRoom.list3);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pickupable"))
        {
            currentObject = other.gameObject;
            IPickupable pickupable = currentObject.GetComponent<IPickupable>();
            if (pickupable != null)
            {
                descriptionText.text = pickupable.GetDescription();
                spriteImage.sprite = pickupable.GetSprite();
                spriteImage.gameObject.SetActive(true);               
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Pickupable"))
        {
            currentObject = null;
            descriptionText.text = "";
            spriteImage.sprite = null;
            spriteImage.gameObject.SetActive(false);
        }
    }

}