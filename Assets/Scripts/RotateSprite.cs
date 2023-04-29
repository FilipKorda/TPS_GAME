using UnityEngine;

public class RotateSprite : MonoBehaviour
{
    public Collider2D playerCollider;
    private float rotationSpeed = 50.0f;
    private float fasterRotationMultiplier = 5.0f;
    [SerializeField] private GameManager gameManager;

    void Update()
    {
        if(gameManager.isTimerRunning)
        {
            if (playerCollider.bounds.Contains(transform.position))
            {
                transform.Rotate(Vector3.forward * rotationSpeed * fasterRotationMultiplier * Time.deltaTime);
            }
            else
            {
                transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            }
        }
        
    }
}
