using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpriteGoToPlayer : MonoBehaviour
{
    public float speed = 5f;
    public float pickupDistance = 1f;
    [SerializeField] private Transform playerTarget;
    public int healthToAdd = 5;

    void Update()
    {
        if (playerTarget == null)
        {
            return;
        }

        PlayerHealth playerHealth = playerTarget.GetComponent<PlayerHealth>();
        if (playerHealth != null && playerHealth.currHealth == playerHealth.maxHealth)
        {
            return;
        }

        float distance = Vector2.Distance(transform.position, playerTarget.position);
        if (distance <= pickupDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTarget.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null && playerHealth.currHealth < playerHealth.maxHealth)
            {
                playerHealth.AddHealth(healthToAdd);
                Destroy(gameObject);
                Debug.Log("Dostajesz tak¹ iloœæ HP: " + healthToAdd);
            }
        }
    }

    public void SetTarget(GameObject target)
    {
        playerTarget = target.transform;
    }
}
