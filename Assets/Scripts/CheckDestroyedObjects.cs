using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class CheckDestroyedObjects : MonoBehaviour
{   
    [Header("==== Another Scripts ====")]
    [Space(10)]
    public TeleportToTheGameField teleportToTheGameField;
    public TeleportationToItemRoom teleportationToItemRoom;
    [Header("==== List ====")]
    [Space(10)]
    public List<GameObject> objectsToCheck;
    [Header("==== Others ====")]
    [Space(10)]
    public GameObject particleSpawnEffect;
    public KeyCode keyToCheck = KeyCode.E;
    private float checkTime = 0.5f;
    private float timeSinceLastCheck = 0f;
    private bool canCheckObjects = true;
    public bool anyObjectDestroyed = false;

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
                CleanUpList(objectsToCheck);
                teleportationToItemRoom.CleanUpList(teleportationToItemRoom.list1);
                teleportationToItemRoom.CleanUpList(teleportationToItemRoom.list2);
                teleportationToItemRoom.CleanUpList(teleportationToItemRoom.list3);
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
            StartCoroutine(ShowTeleportAfterDelay(1f));
        }
    }
    private void CleanUpList(List<GameObject> listToClean)
    {
        listToClean.RemoveAll(item => item == null);
    }
    IEnumerator ShowTeleportAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        teleportToTheGameField.teleport.SetActive(true);
        GameObject particleSpawn = Instantiate(particleSpawnEffect, teleportToTheGameField.teleport.transform.position, Quaternion.identity);
        Destroy(particleSpawn, delay);
    }
}
