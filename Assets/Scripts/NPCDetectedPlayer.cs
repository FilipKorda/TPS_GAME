using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCDetectedPlayer : MonoBehaviour
{
    public float showAboveHead = 10f;
    public float interactionDistance = 1f;
    public GameObject interactPrompt;
    public Transform player;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private InteractWithNPC interactWithNPC;
    public string descriptionInteract = "Press E to interact with NPC";
    private bool isInteracting = false;


    private void Update()
    {
        CheckDistance();
    }

    private void CheckDistance()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < showAboveHead)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < interactionDistance)
            {
                interactWithNPC.canInteract = true;
                interactText.text = descriptionInteract;
            }
            else
            {
                interactWithNPC.canInteract = false;
                interactText.text = "";
            }
            interactPrompt.SetActive(true);
        }
        else
        {
            interactWithNPC.canInteract = false;
            if (!isInteracting)
            {
                interactPrompt.SetActive(false);
                interactText.text = "";
            }
        }
        if (isInteracting)
        {
            interactText.gameObject.SetActive(false);
            interactPrompt.SetActive(false);
        }
    }
}
