using UnityEngine;

public class RotateSpriteWheelOfFortune : MonoBehaviour
{
    public float rotationSpeed = 10f; // prêdkoœæ obrotu w stopniach na sekundê

    void Update()
    {
        transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
    }
}
