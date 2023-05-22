using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PickUpHospitalQuest : MonoBehaviour
{
    private bool canPickUp = false;
    public string description = "Press E to PickUp Key";
    public TextMeshProUGUI descriptionText;
    public static bool haveHammer = false;
    public static bool haveChisel = false;
    public static bool haveCrowbar = false;

    private void Update()
    {
        if (canPickUp && Input.GetKeyDown(KeyCode.E) && gameObject.CompareTag("Hammer"))
        {
            Destroy(gameObject);
            haveHammer = true;
            Debug.Log("podniosles mlot");

        }
        if (canPickUp && Input.GetKeyDown(KeyCode.E) && gameObject.CompareTag("Chisel"))
        {
            Destroy(gameObject);
            haveChisel = true;
            Debug.Log("podniosles d³uto");
        }
        if (canPickUp && Input.GetKeyDown(KeyCode.E) && gameObject.CompareTag("Crowbar"))
        {
            Destroy(gameObject);
            haveCrowbar = true;
            Debug.Log("podniosles ³om");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPickUp = true;
            descriptionText.text = description;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPickUp = false;
            descriptionText.text = "";
        }
    }
}
