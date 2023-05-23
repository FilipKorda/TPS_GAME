using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Painting : MonoBehaviour
{
    private bool canPick = false;
    public string description = "Press E to Open Safe";
    public TextMeshProUGUI descriptionText;



    private void Update()
    {
        if (canPick && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("zabierasz malowid³o");
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPick = true;
            descriptionText.text = description;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPick = false;
            descriptionText.text = "";
        }
    }
}
