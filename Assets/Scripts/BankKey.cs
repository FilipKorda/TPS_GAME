using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BankKey : MonoBehaviour
{
    private bool canPickUp = false;
    public string description = "Press E to PickUp Key";
    public TextMeshProUGUI descriptionText;
    public GameObject keyObject;
    public EnterTheBankScript enterTheBankScript;

    void Update()
    {
        if (canPickUp && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(keyObject);
            enterTheBankScript.haveKey = true;
        }    
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPickUp = true;
            descriptionText.text = description;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPickUp = false;
            descriptionText.text = "";
        }
    }
}

