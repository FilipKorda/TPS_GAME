using UnityEngine;

public class XPItem : MonoBehaviour
{
    public int xpAmount = 10;

    public float pickupRange = 0.3f;

    private void Update()
    {
        // Check if the player is within pickupRange of the object
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && Vector2.Distance(transform.position, player.transform.position) <= pickupRange)
        {
            ExperienceSystem experienceManager = player.GetComponent<ExperienceSystem>();
            if (experienceManager != null)
            {
                Debug.Log("masz XP");
                experienceManager.AddXP(xpAmount);
            }

            Destroy(gameObject);
        }
    }
}