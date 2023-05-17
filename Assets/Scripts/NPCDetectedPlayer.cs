using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCDetectedPlayer : MonoBehaviour
{
    public float distanceToShowPromptAboveHead = 10f;
    public float distanceToInteractWithPlayer = 0.5f;
    public GameObject interactPrompt;
    public Transform player;
    public bool inRangeToInteract = false;
    [SerializeField] private InteractWithNPC interactWithNPC;
    [SerializeField] private NPCDialogue nPCDialogue;


    private void Update()
    {
        CheckDistance();
    }

    private void CheckDistance()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < distanceToShowPromptAboveHead)
        {
            interactPrompt.SetActive(true);

            if (Vector3.Distance(transform.position, player.transform.position) < distanceToInteractWithPlayer)
            {
                inRangeToInteract = true;
                interactWithNPC.interactText.text = interactWithNPC.descriptionInteract;
            }
            else
            {
                inRangeToInteract = false;
                interactWithNPC.interactText.text = "";
            }
        }  
        else
        {
            interactPrompt.SetActive(false);
        }

        if(interactWithNPC.isInteracting)
        {
            interactPrompt.SetActive(false);
            interactWithNPC.interactText.text = "";
        } 
        if(nPCDialogue.dialogueCompleted)
        {
            interactPrompt.SetActive(false);
            interactWithNPC.interactText.text = "";
        }
    }
}
