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
    public Image spriteImage;
    private GameObject currentObject;
    public TeleportationToItemRoom teleportationToItemRoom;
    public TeleportToTheGameField teleportToTheGameField;
    public GameObject particleSpawnEffect;


    private void Start()
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
                    //poka¿ teleport
                    StartCoroutine(ShowTeleportAfterDelay(2f));
                    pickupable.OnPickup();
                    descriptionText.text = "";
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

    IEnumerator ShowTeleportAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        teleportToTheGameField.teleport.SetActive(true);
        GameObject particleSpawn = Instantiate(particleSpawnEffect, teleportToTheGameField.teleport.transform.position, Quaternion.identity);
        Destroy(particleSpawn, delay);
    }

}