using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CorrectSecondSafeNote : MonoBehaviour
{
    private bool canRead = false;
    private bool isReading = false;
    public string description = "Press E to Read Note";

    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private string descriptionTextObjectName;

    [SerializeField] private TextMeshProUGUI descriptionOfTheNoteText;
    [SerializeField] private string descriptionOfTheNoteName;

    [SerializeField] private SecondSentanceNote goodSentanceNote;

    public static bool knowCodeToSafeTwo = false;

    private void Awake()
    {
        knowCodeToSafeTwo = false;
    }

    void Start()
    {
        Invoke("EnableTrigger", 1.2f);
        descriptionText = GameObject.Find(descriptionTextObjectName)?.GetComponent<TextMeshProUGUI>();
        descriptionOfTheNoteText = GameObject.Find(descriptionOfTheNoteName)?.GetComponent<TextMeshProUGUI>();
        if (descriptionText == null && descriptionOfTheNoteText == null)
        {
            Debug.LogError("Nie mo¿na znaleŸæ obiektu TextMeshProUGUI o nazwie: " + descriptionTextObjectName);
            Debug.LogError("Nie mo¿na znaleŸæ obiektu TextMeshProUGUI o nazwie: " + descriptionOfTheNoteName);
        }
    }

    void Update()
    {
        if (canRead && Input.GetKeyDown(KeyCode.E))
        {
            if (!isReading)
            {
                ReadTheNotes();
                Debug.Log("Zaczynasz czytaæ");
                descriptionText.text = "";
            }
            else
            {
                StopReadTheNotes();
                Debug.Log("Przestajesz czytaæ");
            }
        }
    }
    private void ReadTheNotes()
    {
        Time.timeScale = 0;
        knowCodeToSafeTwo = true;
        descriptionOfTheNoteText.text = goodSentanceNote.sentences;
        isReading = true;
    }
    private void StopReadTheNotes()
    {
        Time.timeScale = 1;
        descriptionOfTheNoteText.text = "";
        isReading = false;

    }
    void EnableTrigger()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canRead = true;
            descriptionText.text = description;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canRead = false;
            descriptionText.text = "";
        }
    }
}

[System.Serializable]
public class SecondSentanceNote
{
    [TextArea(1, 5)]
    public string sentences;
}