using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeleportationToItemRoom : MonoBehaviour
{
    public Transform teleportTarget;
    public GameObject player;
    private bool canTeleport = false;
    public string description = "Press E to teleport";
    public TextMeshProUGUI descriptionText;

    //Fade Out and In
    private float fadeDuration = 0.3f;
    private float fadeDelay = 1f;
    public Image fadeOutInPanelImage;

    [SerializeField] public List<GameObject> list1;
    [SerializeField] public List<GameObject> list2;
    [SerializeField] public List<GameObject> list3;
    public bool isObjectActive = false;


    void Start()
    {
        fadeOutInPanelImage.color = new Color(0, 0, 0, 0);
    }
    private GameObject[] GetRandomObjectsFromLists()
    {
        GameObject[] result = new GameObject[3];
        result[0] = list1[Random.Range(0, list1.Count)];
        result[1] = list2[Random.Range(0, list2.Count)];
        result[2] = list3[Random.Range(0, list3.Count)];
        return result;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canTeleport = true;
            descriptionText.enabled = true;
            descriptionText.text = description;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canTeleport = false;
            descriptionText.enabled = false;
        }
    }
    void Update()
    {
        if (canTeleport && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(Teleport());
        }
    }
    public void CleanUpList(List<GameObject> list)
    {
        list.RemoveAll(item => item == null);
    }
    IEnumerator Teleport()
    {
        Time.timeScale = 0;

        float t = 0;
        while (t < fadeDuration)
        {
            float alpha = Mathf.Lerp(0, 1f, t / fadeDuration);
            fadeOutInPanelImage.color = new Color(0, 0, 0, alpha);
            t += Time.unscaledDeltaTime;
            yield return null;
        }
        fadeOutInPanelImage.color = new Color(0, 0, 0, 1f);
        //losowanie itemów do pojawienia
        GameObject[] randomObjects = GetRandomObjectsFromLists();
        isObjectActive = true;
        //koniec losowania itemów
        player.transform.position = teleportTarget.position;
        Time.timeScale = 1;
        yield return new WaitForSecondsRealtime(fadeDelay);

        t = 0;
        while (t < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0, t / fadeDuration);
            fadeOutInPanelImage.color = new Color(0, 0, 0, alpha);
            t += Time.unscaledDeltaTime;
            yield return null;
        }
        fadeOutInPanelImage.color = new Color(0, 0, 0, 0);

        foreach (GameObject obj in randomObjects)
        {
            obj.SetActive(true);
        }

    }
}
