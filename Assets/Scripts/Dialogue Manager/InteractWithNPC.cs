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
    [SerializeField] private Dialogue dialogue;

    void Update()
    {
        InteractWithNpcc();
    }

    private void InteractWithNpcc()
    {

        if (Input.GetKeyDown(interactKey))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, canInteractWithNPC, layerMask);
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

}
