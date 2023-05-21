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
    [Header("==== Text Objects ====")]
    [Space(10)]
    public string description = "Press E to gamble!";
    public string descriptionStatusMoney = "Not enaf money!";
    public string descriptionWhatYouGot = "You got this!";
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI statusMoneyText;
    public TextMeshProUGUI showWhatYouGotText;
    [Header("==== Others ====")]
    [Space(10)]
    public bool isRolling;
    public bool canRoll;
    [SerializeField] private int minMoneyToRoll = 100;
    private int money;
    private int damage;
    private int speed;
    private float attackSpeed;
    private bool hasRolled = false;
    private bool isRotating = false;
    private List<object> statsList;
    private float destroyDelay = 2f;

    private void Start()
    {
        hasRolled = false;
        money = 0;
        damage = 0;
        speed = 0;
        attackSpeed = 0;
        statsList = new List<object> {
            money,
            damage,
            speed,
            attackSpeed
        };
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (!hasRolled)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                descriptionText.enabled = true;
                descriptionText.text = description;
                canRoll = true;
            }
            if (isRolling)
            {
                descriptionText.enabled = false;
                descriptionText.text = "";
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
            if (MoneyManager.instance.playerMoney >= minMoneyToRoll)
            {
                MoneyManager.instance.RemoveMoney(minMoneyToRoll);
                canRoll = true;
                isRolling = true;
                Debug.Log("Zaczynasz losowanie");
                StartCoroutine(RollStats());
            }
            else
            {
                statusMoneyText.enabled = true;
                statusMoneyText.text = descriptionStatusMoney;
                StartCoroutine(HideStatusText(1f));
                canRoll = false;
            }
        }
        if (hasRolled)
        {
            Destroy(gameObject, destroyDelay);
        }
    }

    private object ChooseStatToIncrease()
    {
        int randomIndex = Random.Range(0, statsList.Count);
        return statsList[randomIndex];
    }

    private void IncreaseStat(object statToIncreaseObj)
    {
        if (statToIncreaseObj is int)
        {
            int statToIncrease = (int)statToIncreaseObj;
            if (statToIncrease == money)
            {
                IncreaseMoney();
            }
            else if (statToIncrease == damage)
            {
                IncreaseDamage();
            }
            else if (statToIncrease == speed)
            {
                IncreaseSpeed();
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
                IncreaseAttackSpeed();
            }
            else
            {
                Debug.Log("Nic");
            }
        }
    }
    //jak chcesz wiecej losowañ dodawaj nowe voidy i potem te voidy wklepuj do void IncreaseStat
    private void IncreaseMoney()
    {
        int amount = Random.Range(1, 50);
        string logMessage = amount + " money has been drawn";
        Debug.Log(logMessage);
        showWhatYouGotText.text = logMessage;
        MoneyManager.instance.AddMoney(amount);
    }
    private void IncreaseDamage()
    {
        int amount = Random.Range(1, 2);
        string logMessage = "You roll a damage increase of: " + amount;
        Debug.Log(logMessage);
        showWhatYouGotText.text = logMessage;
        experienceSystem.baseDamage += amount;
    }
    private void IncreaseSpeed()
    {
        speed += Random.Range(1, 10);
        string logMessage = "You roll a Move Speed increase of: " + speed;
        Debug.Log(logMessage);
        showWhatYouGotText.text = logMessage;
    }
    private void IncreaseAttackSpeed()
    {
        float amount = Random.Range((float)0.1, (float)0.15);
        string logMessage = "You roll a Attack Speed increase of: " + amount;
        Debug.Log(logMessage);
        showWhatYouGotText.text = logMessage;
        playerController._timeBetweenShots *= amount;
    }

    private IEnumerator RollStats()
    {
        float elapsedTime = 0;
        float duration = 8;
        isRotating = true;
        while (elapsedTime < duration)
        {
            if (isRotating)
            {
                float t = elapsedTime / duration;
                float sinusoidalValue = Mathf.Sin(t * Mathf.PI / 2);
                float startRotationSpeed = 1000;
                float endRotationSpeed = 0;
                float rotationSpeed = Mathf.Lerp(startRotationSpeed, endRotationSpeed, sinusoidalValue);
                transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        StopRotatingAndShowWhatUGot();
        isRotating = false;
        isRolling = false;
        hasRolled = true;
        object statToIncreaseObj = ChooseStatToIncrease();
        IncreaseStat(statToIncreaseObj);

    }
    void StopRotatingAndShowWhatUGot()
    {
        StartCoroutine(HideWhatYouGotText(2f));
        isRotating = false;
    }
    IEnumerator HideStatusText(float delay)
    {
        yield return new WaitForSeconds(delay);
        statusMoneyText.text = "";
    }
    IEnumerator HideWhatYouGotText(float delay)
    {
        yield return new WaitForSeconds(delay);
        showWhatYouGotText.text = "";
    }
}