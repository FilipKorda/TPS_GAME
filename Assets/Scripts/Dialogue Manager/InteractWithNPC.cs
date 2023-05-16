using System.Xml.Serialization;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class InteractWithNPC : MonoBehaviour
{
    [SerializeField] private NPCDetectedPlayer nPCDetectedPlayer;
    public LayerMask layerMask;
    public KeyCode interactKey = KeyCode.E;
    public bool canInteract = false;

    void Update()
    {
        InteractWithNpcc();
    }
    private void InteractWithNpcc()
    {
        if (canInteract && Input.GetKeyDown(interactKey))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, nPCDetectedPlayer.interactionDistance, layerMask);
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
