using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player; 
    public float speed = 5f; 
    public float distanceThreshold = 0.35f; 
    public float wanderRadius = 1f; 
    public float wanderJitter = 0.2f; 
    private Vector2 wanderTarget; 

    void Start()
    {
        wanderTarget = (Vector2)transform.position + Random.insideUnitCircle.normalized * wanderRadius;
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        foreach (GameObject enemyObject in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (enemyObject != gameObject) 
            {
                float distanceToEnemy = Vector2.Distance(transform.position, enemyObject.transform.position);
                if (distanceToEnemy < distanceThreshold)
                {
                    Vector2 direction = ((Vector2)transform.position - (Vector2)enemyObject.transform.position).normalized;
                    transform.position += (Vector3)(direction * speed * Time.deltaTime);
                }
            }
        }

        if (distanceToPlayer <= distanceThreshold)
        {
            wanderTarget += (Random.insideUnitCircle * wanderJitter);
            Vector2 direction = (wanderTarget - (Vector2)transform.position).normalized;
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }


}
