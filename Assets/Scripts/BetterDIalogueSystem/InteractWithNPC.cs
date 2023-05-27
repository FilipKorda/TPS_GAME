using TMPro;
using UnityEngine;

public class InteractWithNPC : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI interactText;
    private float canInteractWithNPC = 0.5f;
    public LayerMask layerMask;
    public KeyCode interactKey = KeyCode.E;
    public string descriptionInteract = "Press E to interact with NPC";
    public bool isInteracting = false;

    void Update()
    {
        InteractWithNpc();
    }

    private void InteractWithNpc()
    {
        if (Input.GetKeyDown(interactKey))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, canInteractWithNPC, layerMask);
            if (hit.collider != null)
            {
                NPC npc = hit.collider.GetComponent<NPC>();

                if (npc != null)
                {
                    if (isInteracting)
                    {
                        if (npc.areChoicesAvailable)
                        {
                            Debug.Log("Musisz wybraæ!");
                        }
                        else if (npc.isDialogueComplete)
                        {
                            npc.NextFinalDialogue();
                        }
                        else
                        {
                            npc.NextDialogue();
                        }
                    }
                    else
                    {
                        npc.StartDialogue();
                        isInteracting = true;
                    }
                }
            }
        }
    }



}
