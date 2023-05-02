using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    [Header("==== Another Scripts ====")]
    [Space(10)]
    [SerializeField] public TrackPlayerStats trackPlayerStats;
    [SerializeField] public SumaryScreen sumaryScreen;
    [Header("==== Another ====")]
    [Space(10)]
    [SerializeField] private Animator startGame;
    [SerializeField] public int numKills;
    [SerializeField] public GameObject summaryPanel;
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

        int minutesInt = (int)minutes;
        int secondsInt = (int)seconds;

        timerText.text = string.Format("{0:00}:{1:00}", minutesInt, secondsInt);
    }
    #endregion

    public void ShowSummaryPanel()
    {
        summaryPanel.SetActive(true);
        StartCoroutine(ScaleUp());
        sumaryScreen.isSumaryScreenOpen = true;       
        summaryPanel.transform.localScale = Vector3.one * 0.1f;      
    }
    private IEnumerator ScaleUp()
    {
        Vector3 targetScale = Vector3.one; 
        float duration = 5f;
        float timer = 0f;

        while (timer < duration)
        {
            float t = timer / duration;
            summaryPanel.transform.localScale = Vector3.Lerp(summaryPanel.transform.localScale, targetScale, t);
            timer += Time.deltaTime;
            yield return null;
        }

        summaryPanel.transform.localScale = targetScale;
    }

}