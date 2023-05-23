using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectToCompleteRadioQuest : MonoBehaviour
{
    private bool canPickUP = false;
    private bool isDerstroyed = false;
    public string description = "Press E to Pick Up:";

    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private string descriptionTextObjectName;


    public static bool haveWires;
    public static bool haveSpeaker;
    public static bool haveScrews;
    public static bool haveGears;

    void Start()
    {
        Invoke("EnableTrigger", 1.2f);
        descriptionText = GameObject.Find(descriptionTextObjectName)?.GetComponent<TextMeshProUGUI>();
        if (descriptionText == null )
        {
            Debug.LogError("Nie mo¿na znaleŸæ obiektu TextMeshProUGUI o nazwie: " + descriptionTextObjectName);

        }
    }

    void Update()
    {
        if (canPickUP && Input.GetKeyDown(KeyCode.E) && gameObject.CompareTag("Wires"))
        {
            if (!isDerstroyed)
            {
                haveWires = true;
                Destroy(gameObject);
                Debug.Log("podnosisz Wires");
                descriptionText.text = "";
            }
        }
        if (canPickUP && Input.GetKeyDown(KeyCode.E) && gameObject.CompareTag("Speaker"))
        {
            if (!isDerstroyed)
            {
                haveSpeaker = true;
                Destroy(gameObject);
                Debug.Log("podnosisz speaker");
                descriptionText.text = "";
            }
        }
        if (canPickUP && Input.GetKeyDown(KeyCode.E) && gameObject.CompareTag("Screws"))
        {
            if (!isDerstroyed)
            {
                haveScrews = true;
                Destroy(gameObject);
                Debug.Log("podnosisz Screws");
                descriptionText.text = "";
            }
        }
        if (canPickUP && Input.GetKeyDown(KeyCode.E) && gameObject.CompareTag("Gears"))
        {
            if (!isDerstroyed)
            {
                haveGears = true;
                Destroy(gameObject);
                Debug.Log("podnosisz Screws");
                descriptionText.text = "";
            }
        }
    }
    void EnableTrigger()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPickUP = true;
            descriptionText.text = description;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPickUP = false;
            descriptionText.text = "";
        }
    }
}
