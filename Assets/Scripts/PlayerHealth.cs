using Cinemachine;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    public int currHealth;
    [SerializeField] public Slider healthSlider;
    [SerializeField] private Animator TakeHit;
    [SerializeField] private Animator TakeHitTwo;
    [SerializeField] private GameObject enemySpawner;
    [SerializeField] private GameObject[] enemyObjects;
    [SerializeField] private GameManager gameManager;

    public CinemachineVirtualCamera vcam;

    public Gradient gradient;

    void Start()
    {
        currHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currHealth -= damage;
        UpdateHealthBar();
        if (currHealth <= 0)
        {
            Die();
        }
    }
   
    private void UpdateHealthBar()
    {
        healthSlider.value = currHealth;
        TakeHit.SetTrigger("TakeHit");
        TakeHitTwo.SetTrigger("TakeHit");

        float percentage = (float)currHealth / maxHealth;
        healthSlider.fillRect.GetComponent<Image>().color = gradient.Evaluate(percentage);
    }
    private void Die()
    {
        
        foreach (GameObject enemyObject in enemyObjects)
        {
            Enemy enemy = enemyObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.enabled = false;
            }
        }
        Destroy(enemySpawner);
        // Change player tag and layer order
        gameObject.tag = "Untagged";
        StartCoroutine(ShakeAndDestroy());
        AudioManager.Instance.backgroundMusicSource.Stop();
        AudioManager.Instance.PlaySFX("YouDie");


        StartCoroutine(ZoomInCamera(2f, 1f));
        gameManager.StopTimer();
        gameManager.ShowSummaryPanel();
    }
    private IEnumerator ShakeAndDestroy()
    {
        float shakeDuration = 0.5f;
        float shakeIntensity = 0.2f;

        Vector3 startPosition = transform.position;

        while (shakeDuration > 0)
        {
            transform.position = startPosition + Random.insideUnitSphere * shakeIntensity;
            shakeDuration -= Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    private IEnumerator ZoomInCamera(float targetOrthoSize, float duration)
    {
        float startOrthoSize = vcam.m_Lens.OrthographicSize;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            vcam.m_Lens.OrthographicSize = Mathf.Lerp(startOrthoSize, targetOrthoSize, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        vcam.m_Lens.OrthographicSize = targetOrthoSize; // set the final value
    }

}
