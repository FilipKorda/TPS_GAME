using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PickUpSystem : MonoBehaviour
{   
    [Header("==== Text Object ====")]
    [Space(10)]
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI titleText;
    [Header("==== Others ====")]
    [Space(10)]
    public float pickupRange = 2f;
    public KeyCode interactKey = KeyCode.E;
    public Image spriteImage;
    private GameObject currentObject;


    private void Start()
    {
        spriteImage.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            if (currentObject != null)
            {
                IPickupable pickupable = currentObject.GetComponent<IPickupable>();
                pickupable?.OnPickup();
            }
        }

    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Pickupable"))
        {
            currentObject = other.gameObject;
            IPickupable pickupable = currentObject.GetComponent<IPickupable>();
            if (pickupable != null)
            {
                descriptionText.text = string.Join("\n", pickupable.GetDescription());
                titleText.text = pickupable.GetTitle();
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
            titleText.text = "";
            spriteImage.sprite = null;
            spriteImage.gameObject.SetActive(false);
        }
    }
}