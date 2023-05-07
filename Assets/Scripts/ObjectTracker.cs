using UnityEngine;

public class ObjectTracker : MonoBehaviour
{
    public Transform[] objectsToTrack;
    public Sprite arrowSprite;
    public Material arrowMaterial;

    private Transform closestObject;
    private GameObject arrow;

    void Start()
    {
        arrow = new GameObject("TrackerArrow");
        arrow.AddComponent<SpriteRenderer>().sprite = arrowSprite;
        arrow.GetComponent<SpriteRenderer>().material = arrowMaterial;
    }

    void Update()
    {
        float closestDistance = Mathf.Infinity;
        foreach (Transform obj in objectsToTrack)
        {
            float distance = Vector3.Distance(transform.position, obj.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = obj;
            }
        }

        Vector3 direction = closestObject.position - Camera.main.transform.position;
        float angle = Vector3.Angle(direction, Camera.main.transform.forward);

        if (angle > 30f)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(closestObject.position);
            float margin = 20f;
            float arrowX = Screen.width - margin;
            float arrowY = margin;
            arrowX /= Screen.width;
            arrowY /= Screen.height;
            Vector3 arrowPos = Camera.main.ViewportToWorldPoint(new Vector3(arrowX, arrowY, Camera.main.nearClipPlane));
            arrowPos.z = 0f;
            arrow.transform.position = arrowPos;
            Quaternion arrowRotation = Quaternion.LookRotation(Vector3.forward, direction) * Quaternion.Euler(0, -20, 0);
            arrow.transform.rotation = arrowRotation;
        }
        else
        {
            arrow.SetActive(false);
        }
    }

}
