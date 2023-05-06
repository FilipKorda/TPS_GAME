using UnityEngine;
using System.Collections;
using TMPro;
using System.Collections.Generic;

public class RandomStats : MonoBehaviour
{
    [Header("==== Another Scripts ====")]
    [Space(10)]
    [SerializeField] private ExperienceSystem experienceSystem;
    [SerializeField] private PlayerController playerController;
    public string description = "Press E to gamble!";
    public TextMeshProUGUI descriptionText;
    public bool isRolling;
    public bool canRoll;

    private int money;
    private int damage;
    private int speed;
    private float attackSpeed;

    private bool hasRolled = false;

    public GameObject gameObjectToActivate;
    private List<object> statsList;

    private float destroyDelay = 5f;
    private void Start()
    {
        hasRolled = false;
        statsList = new List<object> {
            money,
            damage,
            speed,
            attackSpeed
        };
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasRolled)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                descriptionText.enabled = true;
                descriptionText.text = description;
                canRoll = true;
            }
        }
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            descriptionText.enabled = false;
            canRoll = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canRoll && !isRolling && !hasRolled)
        {
            isRolling = true;
            Debug.Log("Zaczynasz losowanie");
            StartCoroutine(RollStats());
        }
        if (hasRolled)
        {
            Destroy(gameObject, destroyDelay);
        }
    }


    private IEnumerator RollStats()
    {
        gameObjectToActivate.SetActive(true);
        float elapsedTime = 0;
        float duration = 5;
        while (elapsedTime < duration)
        {         
            elapsedTime += Time.deltaTime;          
            yield return null;           
        }
        gameObjectToActivate.SetActive(false);
        isRolling = false;
        hasRolled = true;
        int randomIndex = Random.Range(0, statsList.Count);
        object statToIncreaseObj = statsList[randomIndex];
        if (statToIncreaseObj is int)
        {
            int statToIncrease = (int)statToIncreaseObj;
            if (statToIncrease == money)
            {
                int amount = Random.Range(1, 50);
                Debug.Log("Wylosowano: " + amount + " money");
                MoneyManager.instance.AddMoney(amount);
            }
            if (statToIncrease == damage)
            {
                int amount = Random.Range(1, 2);
                Debug.Log("Wylosowano: " + amount + " damage");
                experienceSystem.baseDamage += amount;
            }
            else if (statToIncrease == speed)
            {
                speed += Random.Range(1, 10);
                Debug.Log("Wylosowano: " + speed + " speed");
            }
            else
            {
                Debug.Log("Nic");
            }
        }
        else if (statToIncreaseObj is float)
        {
            float statToIncrease = (float)statToIncreaseObj;
            if (Mathf.Approximately(statToIncrease, attackSpeed))
            {
                float amount = Random.Range((float)0.1, (float)0.15);
                Debug.Log("Wylosowano: " + amount + " attackSpeed");
                playerController._timeBetweenShots *= amount;
            }
            else
            {
                Debug.Log("Nic");
            }
        }


    }
}
