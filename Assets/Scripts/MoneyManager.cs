using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;

    public int playerMoney = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void AddMoney(int amount)
    {
        playerMoney += amount;
    }

    public void RemoveMoney(int amount)
    {
        if (playerMoney >= amount)
        {
            playerMoney -= amount;
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }
}
