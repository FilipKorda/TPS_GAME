using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class InteractWithNPC : MonoBehaviour
{
    public float showAboveHead = 10f;
    public float interactionDistance = 1f;
    public GameObject interactPrompt;
    public GameObject NPC;
    public LayerMask layerMask; 
    public KeyCode interactKey = KeyCode.E;
    private bool canInteract = false;
    public string descriptionInteract = "Press E to interact with NPC";
    [SerializeField] public TextMeshProUGUI interactText;

    void Update()
    {

        InteractWithNpc();
    }

    private void InteractWithNpc()
    {
        CheckDistance();
        if (canInteract && Input.GetKeyDown(interactKey))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, interactionDistance, layerMask);
            if (hit.collider != null)
            {
                NPCDialogue npcDialogue = hit.collider.GetComponent<NPCDialogue>();
                if (npcDialogue != null)
                {
                    npcDialogue.StartDialogue();
                }
        
            }
        }
    }

    private void CheckDistance()
    {     
        if (Vector3.Distance(transform.position, NPC.transform.position) < showAboveHead)
        {
            if (Vector3.Distance(transform.position, NPC.transform.position) < interactionDistance)
            {
                canInteract = true;
                interactText.text = descriptionInteract;
            }
            else
            {
                canInteract = false;
                interactText.text = "";
            }
            interactPrompt.SetActive(true);
        }
        else
        {
            canInteract = false;
            interactPrompt.SetActive(false);
            interactText.text = "";
        }       
    }    
}
