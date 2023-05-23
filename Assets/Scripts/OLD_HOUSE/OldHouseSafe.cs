using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;


public class OldHouseSafe : MonoBehaviour
{
    private bool canOpen = false;
    public string description = "Press E to Open Safe";
    public TextMeshProUGUI descriptionText;
    public GameObject safeClosed;
    public GameObject safeOpened;


    private void Update()
    {
        if (canOpen && Input.GetKeyDown(KeyCode.E))
        {
            safeClosed.SetActive(false);
            safeOpened.SetActive(true);
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canOpen = true;
            descriptionText.text = description;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canOpen = false;
            descriptionText.text = "";
        }
    }
}
