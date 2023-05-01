using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PickupableObject : MonoBehaviour, IPickupable
{
    public string[] description = { "This is a pickupable object." };
    public Sprite sprite;
    public Sprite dotSprite;
    //Tytu³
    public string title = "Title";
    public Color titleColor = Color.white;

    private float particleLifetime = 2.0f;
    public GameObject destroyParticles;
    public GameObject pickupParticles;
    int cost;
    [Header("==== Another Scripts ====")]
    [Space(10)]
    [SerializeField] private ExperienceSystem experienceSystem;
    [SerializeField] private PlayerController playerController;     

    public void OnPickup()
    {       
        //Add damage
        IncreaseDamage increaseDamage = GetComponent<IncreaseDamage>();
        if (increaseDamage != null && experienceSystem != null)
        {
            int cost = increaseDamage.GetCost();
            if (MoneyManager.instance.playerMoney >= cost)
            {
                MoneyManager.instance.RemoveMoney(cost);
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
                        Destroy(particleOne.gameObject, particleLifetime);
                        //tu zbierasz przedmiot
                        GameObject particleTwo = Instantiate(pickupParticles, transform.position, Quaternion.identity);
                        Destroy(particleTwo.gameObject, particleLifetime);
                        Debug.Log("Masz upgrade tego: " + gameObject.name);
                    }
                }
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Not enough money to purchase increaseDamage!");
            }
        }
        //Add attackSpeed
        IncreaseAttackSpeed increaseAttackSpeed = GetComponent<IncreaseAttackSpeed>();
        if (increaseAttackSpeed != null && playerController != null)
        {
            int cost = increaseAttackSpeed.GetCost();
            if (MoneyManager.instance.playerMoney >= cost)
            {
                MoneyManager.instance.RemoveMoney(cost);
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
                        Destroy(particleOne.gameObject, particleLifetime);
                        //tu zbierasz przedmiot
                        GameObject particleTwo = Instantiate(pickupParticles, transform.position, Quaternion.identity);
                        Destroy(particleTwo.gameObject, particleLifetime);
                        Debug.Log("Masz upgrade tego: " + gameObject.name);
                    }
                }
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Not enough money to purchase increaseAttackSpeed!");
            }
        }
        //DashUpgrade
        DashUpgrade dashUpgrade = GetComponent<DashUpgrade>();
        if (dashUpgrade != null && playerController != null)
        {
            int cost = dashUpgrade.GetCost();
            if (MoneyManager.instance.playerMoney >= cost)
            {              
                MoneyManager.instance.RemoveMoney(cost);
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
                        Destroy(particleOne.gameObject, particleLifetime);
                        //tu zbierasz przedmiot
                        GameObject particleTwo = Instantiate(pickupParticles, transform.position, Quaternion.identity);
                        Destroy(particleTwo.gameObject, particleLifetime);
                        Debug.Log("Masz upgrade tego: " + gameObject.name);
                    }
                }             
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Not enough money to purchase Dash Upgrade!");
            }
        }
       
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

