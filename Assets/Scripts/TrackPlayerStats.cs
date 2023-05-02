using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrackPlayerStats : MonoBehaviour
{
    [Header("==== Text Object ====")]
    [Space(10)]
    public TextMeshProUGUI killsText;
    public TextMeshProUGUI currentLevel;
    public TextMeshProUGUI damage;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI currentMoney;
    [Header("==== Another Scripts ====")]
    [Space(10)]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ExperienceSystem experienceSystem;
    [SerializeField] private MoneyManager moneyManager;


    void Update()
    {
        UpdateStatsDisplay();
    }

    public void UpdateStatsDisplay()
    {
        killsText.text = "" + gameManager.numKills.ToString();
        currentLevel.text = "" + experienceSystem.currentLevel.ToString();
        damage.text = "" + (experienceSystem.NewDamage + experienceSystem.baseDamage - 1).ToString();
        timerText.text = gameManager.timerText.text;
        currentMoney.text = "" + moneyManager.playerMoney.ToString();
    }


}
