using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldBarsScript : MonoBehaviour
{
    void Start()
    {
        Invoke("EnableTrigger", 1.5f);
    }
    void EnableTrigger()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            MoneyManager.instance.AddMoney(50);
        }
    }
}
