using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PickupableObject : MonoBehaviour, IPickupable
{
    [Header("==== Another Scripts ====")]
    [Space(10)]
    [SerializeField] private ExperienceSystem experienceSystem;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private TeleportationToItemRoom teleportationToItemRoom;
    [Header("==== Title ====")]
    [Space(10)]
    public string title = "Title";
    public Color titleColor = Color.white;
    [Header("==== Upgrades ====")]
    [Space(10)]
    private float particleLifetime = 2.0f;
    [SerializeField] private GameObject destroyParticles;
    [SerializeField] private GameObject pickupParticles;
    int cost;
    [Header("==== Another ====")]
    [Space(10)]
    public string[] description = { "This is a pickupable object." };
    [SerializeField] private Color statusMoneyColor = Color.white;
    public string descriptionStatusMoney = "Not enough money to purchase";
    [SerializeField] private Sprite sprite;
    [SerializeField] private Sprite dotSprite;
    [SerializeField] public TextMeshProUGUI statusMoneyText;

    public void OnPickup()
    {
        BuyIncreaseDamage();
        BuyIncreaseAttackSpeed();
        BuyDashUpgrade();
    }
    public void BuyIncreaseDamage()
    {
        //Add damage
        IncreaseDamage increaseDamage = GetComponent<IncreaseDamage>();
        if (increaseDamage != null && experienceSystem != null)
        {
            int cost = increaseDamage.GetCost();
            if (MoneyManager.instance.playerMoney >= cost)
            {
                MoneyManager.instance.RemoveMoney(cost);
                Debug.Log("Tu p³acisz za itema: " + cost);
                increaseDamage.AddDamageToPlayer(experienceSystem);
                Debug.Log("DOdano ci wiêcej obra¿eñ");
                GameObject[] otherPickupables = GameObject.FindGameObjectsWithTag("Pickupable");
                foreach (GameObject pickupable in otherPickupables)
                {
                    if (pickupable != gameObject)
                    {
                        //a tu niszczysz 2 pozosta³e
                        Destroy(pickupable);
                        GameObject particleOne = Instantiate(destroyParticles, pickupable.transform.position, Quaternion.identity);
                        Destroy(particleOne, particleLifetime);
                        //tu zbierasz przedmiot
                        GameObject particleTwo = Instantiate(pickupParticles, transform.position, Quaternion.identity);
                        Destroy(particleTwo, particleLifetime);
                    }
                }
                Destroy(gameObject);
                teleportationToItemRoom.isItemToBuyActive = false;
            }
            else
            {
                statusMoneyText.text = descriptionStatusMoney;
                ColorLastWord(descriptionStatusMoney);
                StartCoroutine(HideStatusText(1f));
            }
        }
    }
    public void BuyIncreaseAttackSpeed()
    {
        //Add attackSpeed
        IncreaseAttackSpeed increaseAttackSpeed = GetComponent<IncreaseAttackSpeed>();
        if (increaseAttackSpeed != null && playerController != null)
        {
            int cost = increaseAttackSpeed.GetCost();
            if (MoneyManager.instance.playerMoney >= cost)
            {
                MoneyManager.instance.RemoveMoney(cost);
                Debug.Log("Tu p³acisz za itema: " + cost);
                increaseAttackSpeed.IncreaseAttackSpeedOfPlayer(playerController);
                Debug.Log("Dostajesz wiekszy Attack Spped");
                GameObject[] otherPickupables = GameObject.FindGameObjectsWithTag("Pickupable");
                foreach (GameObject pickupable in otherPickupables)
                {
                    if (pickupable != gameObject)
                    {
                        //a tu niszczysz 2 pozosta³e
                        Destroy(pickupable);
                        GameObject particleOne = Instantiate(destroyParticles, pickupable.transform.position, Quaternion.identity);
                        Destroy(particleOne, particleLifetime);
                        //tu zbierasz przedmiot
                        GameObject particleTwo = Instantiate(pickupParticles, transform.position, Quaternion.identity);
                        Destroy(particleTwo, particleLifetime);
                    }
                }
                Destroy(gameObject);
                teleportationToItemRoom.isItemToBuyActive = false;
            }
            else
            {
                statusMoneyText.text = descriptionStatusMoney;
                ColorLastWord(descriptionStatusMoney);
                StartCoroutine(HideStatusText(1f));

            }
        }
    }
    public void BuyDashUpgrade()
    {
        //DashUpgrade
        DashUpgrade dashUpgrade = GetComponent<DashUpgrade>();
        if (dashUpgrade != null && playerController != null)
        {
            int cost = dashUpgrade.GetCost();
            if (MoneyManager.instance.playerMoney >= cost)
            {
                MoneyManager.instance.RemoveMoney(cost);
                Debug.Log("Tu p³acisz za itema: " + cost);
                dashUpgrade.PlayerNowHaveDash(playerController);
                Debug.Log("Masz Dasha");
                // Disable all other pickupable objects in the scene
                GameObject[] otherPickupables = GameObject.FindGameObjectsWithTag("Pickupable");
                foreach (GameObject pickupable in otherPickupables)
                {
                    if (pickupable != gameObject)
                    {
                        //a tu niszczysz 2 pozosta³e
                        Destroy(pickupable);
                        GameObject particleOne = Instantiate(destroyParticles, pickupable.transform.position, Quaternion.identity);
                        Destroy(particleOne, particleLifetime);
                        //tu zbierasz przedmiot
                        GameObject particleTwo = Instantiate(pickupParticles, transform.position, Quaternion.identity);
                        Destroy(particleTwo, particleLifetime);
                    }
                }
                Destroy(gameObject);
                teleportationToItemRoom.isItemToBuyActive = false;
            }
            else
            {
                statusMoneyText.text = descriptionStatusMoney;
                ColorLastWord(descriptionStatusMoney);
                StartCoroutine(HideStatusText(1f));
            }
        }
    }

    
    public void ColorLastWord(string text)
    {
        string[] words = text.Split(' ');
        string lastWord = words[words.Length - 1];
        string formattedText = string.Format("{0}<color=#{1}>{2}</color>",
            string.Join(" ", words, 0, words.Length - 1), ColorUtility.ToHtmlStringRGBA(statusMoneyColor), lastWord);
        statusMoneyText.text = formattedText;
    }
    IEnumerator HideStatusText(float delay)
    {
        yield return new WaitForSeconds(delay);
        statusMoneyText.text = "";
    }
    public string[] GetDescription()
    {
        string[] result = new string[description.Length];
        for (int i = 0; i < description.Length; i++)
        {
            result[i] = "<sprite name=" + dotSprite.name + "> " + description[i];
        }
        return result;
    }
    public string GetTitle()
    {
        return "<color=#" + ColorUtility.ToHtmlStringRGBA(titleColor) + ">" + title + "</color>";
    }
    public Sprite GetSprite()
    {
        return sprite;
    }
    public int GetCost() { return cost; }
}

