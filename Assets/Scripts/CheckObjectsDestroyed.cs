using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckObjectsDestroyed : MonoBehaviour
{
    public List<GameObject> objectsToCheck;
    public TeleportToTheGameField teleportToTheGameField;
    public GameObject particleSpawnEffect;

    public bool anyDestroyed = false;


    private void Update()
    {
        CheckDestroyed();
    }
    private void CheckDestroyed()
    {
        bool allDestroyed = true;
        foreach (GameObject obj in objectsToCheck)
        {
            if (obj != null)
            {
                anyDestroyed = false;
                break;
            }
        }

        if (anyDestroyed)
        {
            StartCoroutine(ShowTeleportAfterDelay(2f));
        }
    }


    IEnumerator ShowTeleportAfterDelay(float delay)
    {
        teleportToTheGameField.teleport.SetActive(true);
        GameObject particleSpawn = Instantiate(particleSpawnEffect, teleportToTheGameField.teleport.transform.position, Quaternion.identity);
        Destroy(particleSpawn, delay);
        yield return new WaitForSeconds(delay);

    }
}
