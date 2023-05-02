using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckDestroyedObjects : MonoBehaviour
{
    public List<GameObject> objectsToCheck;
    public TeleportToTheGameField teleportToTheGameField;
    public TeleportationToItemRoom teleportationToItemRoom;
    public GameObject particleSpawnEffect;
    public KeyCode keyToCheck = KeyCode.E;

    private float checkTime = 1f;
    private float timeSinceLastCheck = 0f;
    private bool canCheckObjects = true;


    private void Update()
    {
        if (teleportationToItemRoom.isObjectActive)
        {
            if (Input.GetKeyDown(keyToCheck))
            {
                canCheckObjects = true;
            }
        }
        if (canCheckObjects)
        {
            timeSinceLastCheck += Time.deltaTime;
            if (timeSinceLastCheck >= checkTime)
            {
                CheckObjectsDestroyed();
                timeSinceLastCheck = 0f;
                canCheckObjects = false;
            }
        }
    }

    private void CheckObjectsDestroyed()
    {
        bool anyObjectDestroyed = false;

        foreach (GameObject obj in objectsToCheck)
        {
            if (obj == null)
            {
                anyObjectDestroyed = true;
                break;
            }
        }
        Debug.Log("Any object destroyed: " + anyObjectDestroyed);
        if (anyObjectDestroyed)
        {
            StartCoroutine(ShowTeleportAfterDelay(2f));
        }
    }


    IEnumerator ShowTeleportAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        teleportToTheGameField.teleport.SetActive(true);
        GameObject particleSpawn = Instantiate(particleSpawnEffect, teleportToTheGameField.teleport.transform.position, Quaternion.identity);
        Destroy(particleSpawn, delay);

    }
}
