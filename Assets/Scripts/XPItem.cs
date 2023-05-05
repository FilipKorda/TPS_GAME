using UnityEngine;

public class XPItem : MonoBehaviour
{
    public float speed = 5f;
    public float pickupDistance = 1f;
    [SerializeField] private Transform playerTarget;
    public int xpAmount = 5;

    void Update()
    {
        if (playerTarget == null)
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
            ExperienceSystem experienceSystem = other.GetComponent<ExperienceSystem>();
            if (experienceSystem != null)
            {
                experienceSystem.AddXP(xpAmount);
                Destroy(gameObject);
                Debug.Log("Dostajesz tak¹ iloœæ HP: " + xpAmount);
            }
        }
    }

    public void SetTarget(GameObject target)
    {
        playerTarget = target.transform;
    }
}
