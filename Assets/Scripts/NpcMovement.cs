using System.Collections;
using UnityEngine;

public class NpcMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2f;
    public float waitTime = 5f;
    private Transform targetWaypoint;
    private bool isWaiting = false;
    public float avoidanceRadius = 1f;

    private void Start()
    {
        PickRandomWaypoint();
    }

    private void Update()
    {
        if (targetWaypoint != null)
        {
            if (!isWaiting)
            {
                // Poruszanie siê w kierunku punktu docelowego
                transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

                // Sprawdzanie kolizji z przeszkodami
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, avoidanceRadius);
                foreach (Collider2D collider in colliders)
                {
                    if (collider.CompareTag("Obstacle"))
                    {
                        // Wykryto przeszkodê - omijanie
                        Vector2 avoidanceDirection = (transform.position - collider.transform.position).normalized;
                        transform.position += (Vector3)avoidanceDirection * speed * Time.deltaTime;
                    }
                }

                // Sprawdzanie, czy dotarto do punktu docelowego
                if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
                {
                    // Rozpoczêcie oczekiwania
                    isWaiting = true;
                    // Uruchomienie timera
                    Invoke("PickRandomWaypoint", waitTime);
                }
            }
        }
    }


    private void PickRandomWaypoint()
    {
        int randomIndex = Random.Range(0, waypoints.Length);
        targetWaypoint = waypoints[randomIndex];
        isWaiting = false;
    }

}
