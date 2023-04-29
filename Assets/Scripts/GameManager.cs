using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Animator startGame;
    [SerializeField] public int numKills;
    [SerializeField] public GameObject summaryPanel;
    [SerializeField] public TrackPlayerStats trackPlayerStats;

    public TextMeshProUGUI timerText;
    private float elapsedTime;
    public bool isTimerRunning;


    private void Start()
    {
        StartTimer();
        startGame.Play("ShowHpBarAnimation");
    }

    void Update()
    {
        if (isTimerRunning)
        {
            elapsedTime += Time.deltaTime;
            DisplayTime();
        }
    }
    #region
    public void StartTimer()
    {
        isTimerRunning = true;
    }
    public void StopTimer()
    {
        isTimerRunning = false;
    }
    public void ResetTimer()
    {
        elapsedTime = 0f;
        isTimerRunning = false;
        DisplayTime();
    }
    private void DisplayTime()
    {
        float minutes = Mathf.FloorToInt(elapsedTime / 60f);
        float seconds = Mathf.FloorToInt(elapsedTime % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    #endregion


    public void ShowSummaryPanel()
    {
        summaryPanel.SetActive(true); // activate the summary panel
        trackPlayerStats.UpdateStatsDisplay(); // update the stats in the summary panel
    }
}