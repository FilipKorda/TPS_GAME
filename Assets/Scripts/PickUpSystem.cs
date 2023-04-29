using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickUpSystem : MonoBehaviour
{
    public float pickupRange = 2f;
    public KeyCode interactKey = KeyCode.E;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI titleText;
    public Gradient titleColor;
    public Image spriteImage;
    private GameObject currentObject;
    public TeleportationToItemRoom teleportationToItemRoom;
    public TeleportToTheGameField teleportToTheGameField;
    public GameObject particleSpawnEffect;

    void Start()
    {
        spriteImage.gameObject.SetActive(false);

        VertexGradient vertexGradient = new VertexGradient(titleColor.Evaluate(0f), titleColor.Evaluate(1f), Color.white, Color.white);

        // przypisz utworzony obiekt do w³aœciwoœci colorGradient obiektu TextMeshProUGUI
        titleText.colorGradient = vertexGradient;
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
                    //poka¿ teleport
                    StartCoroutine(ShowTeleportAfterDelay(2f));
                    pickupable.OnPickup();
                    descriptionText.text = string.Join("\n", pickupable.GetDescription());
                    titleText.text = "";
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
                titleText.text = pickupable.GetTitle();
                PickupableObject pickupableObject = currentObject.GetComponent<PickupableObject>();
                if (pickupableObject != null)
                {
                    titleText.colorGradient = pickupableObject.titleColor;
                }
                descriptionText.text = string.Join("\n", pickupable.GetDescription());
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

    IEnumerator ShowTeleportAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        teleportToTheGameField.teleport.SetActive(true);
        Instantiate(particleSpawnEffect, teleportToTheGameField.teleport.transform.position, Quaternion.identity);

    }

}