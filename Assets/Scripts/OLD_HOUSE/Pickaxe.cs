using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    private bool canPickUP = false;
    public static bool havePickaxe = false;
    public string description = "Press E to Pick UP Pickaxe";
    public TextMeshProUGUI descriptionText;

    private void Update()
    {
        if (canPickUP && Input.GetKeyDown(KeyCode.E))
        {
            havePickaxe = true;
            Destroy(gameObject);
        }

    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPickUP = true;
            descriptionText.text = description;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPickUP = false;
            descriptionText.text = "";
        }
    }
}
