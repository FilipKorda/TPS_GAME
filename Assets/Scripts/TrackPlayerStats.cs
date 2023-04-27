using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrackPlayerStats : MonoBehaviour
{
    public TextMeshProUGUI killsText;
    public TextMeshProUGUI currentLevel;
    public TextMeshProUGUI damage;
    public TextMeshProUGUI timerText;
    
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ExperienceSystem experienceSystem;


    void Update()
    {
        UpdateStatsDisplay();
    }

    public void UpdateStatsDisplay()
    {
        killsText.text = "" + gameManager.numKills.ToString();
        currentLevel.text = "" + experienceSystem.currentLevel.ToString();
        damage.text = "" + experienceSystem.baseDamage.ToString();
        timerText.text = gameManager.timerText.text;
    }

}
