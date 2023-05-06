using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class CheckDestroyedObjects : MonoBehaviour
{
    public List<GameObject> objectsToDetect;
    public float detectionDelay = 0.2f;
    public float teleportDelay = 1f;
    public TeleportToTheGameField teleportToTheGameField;
    public TeleportationToItemRoom teleportationToItemRoom;
    public GameObject particleSpawnEffect;
    public bool itemToBuyAreDestroyed = false;
    public KeyCode keyToCheck = KeyCode.E;

    private void Update()
    {
        if (!teleportationToItemRoom.isItemToBuyActive)
        {
            if (Input.GetKeyDown(keyToCheck))
            {
                StartCoroutine(DetectObjects());
                //Debug.Log("klikasz E");
            }
        }
    }


    IEnumerator DetectObjects()
    {
        while (!itemToBuyAreDestroyed)
        {
            yield return new WaitForSeconds(detectionDelay);

            bool allObjectsIntact = true;
            foreach (GameObject obj in objectsToDetect)
            {
                if (obj == null)
                {
                    allObjectsIntact = false;
                    break;
                }
            }

            if (!allObjectsIntact)
            {
                CleanUpList(objectsToDetect);
                teleportationToItemRoom.CleanUpList(teleportationToItemRoom.list1);
                teleportationToItemRoom.CleanUpList(teleportationToItemRoom.list2);
                teleportationToItemRoom.CleanUpList(teleportationToItemRoom.list3);
                Debug.Log("tu czyœcisz 2 listy");
                itemToBuyAreDestroyed = true;
                Debug.Log("tu Item To Buy s¹ nie zniszczone");
                StartCoroutine(ShowTeleportAfterDelay(0.5f));
                Debug.Log("a tu odpalasz teleport");
            }
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