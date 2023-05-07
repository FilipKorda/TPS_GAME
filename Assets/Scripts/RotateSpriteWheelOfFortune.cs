using UnityEngine;

public class RotateSpriteWheelOfFortune : MonoBehaviour
{
    public float rotationSpeed = 10f; // pr�dko�� obrotu w stopniach na sekund�

    void Update()
    {
        transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
    }
}
