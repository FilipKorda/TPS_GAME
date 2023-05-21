using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoneyBags : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySFX("PickUpCash");
            Destroy(gameObject);
            MoneyManager.instance.AddMoney(2);
        }
    }
}
