using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpriteGoToPlayer : MonoBehaviour
{
    public float speed = 5f;
    public float pickupDistance = 1f;
    public float pickupDistanceToHeal = 0.1f;
    private GameObject playerTarget;
    public int healthToAdd = 5;

    void Update()
    {
        if (playerTarget == null)
        {
            return;
        }

        float distance = Vector2.Distance(transform.position, playerTarget.transform.position);
        if (distance <= pickupDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTarget.transform.position, speed * Time.deltaTime);
        }
    }
    public void HealPlayer()
    {
        if (playerTarget == null)
        {
            return;
        }
        float distance = Vector2.Distance(transform.position, playerTarget.transform.position);
        if (distance <= pickupDistanceToHeal)
        {
            PlayerHealth playerHealth = playerTarget.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.AddHealth(healthToAdd);
            }
            Destroy(gameObject);
        }
    }

    public void SetTarget(GameObject target)
    {
        playerTarget = target;
    }
}
