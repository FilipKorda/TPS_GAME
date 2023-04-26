using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currHealth;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Animator TakeHit;
    [SerializeField] private Animator TakeHitTwo;
    [SerializeField] private Animator startGame;

    public Gradient gradient;

    void Start()
    {
        currHealth = maxHealth;
        UpdateHealthBar();
        startGame.Play("ShowHpBarAnimation");
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
        Destroy(gameObject);
        AudioManager.Instance.backgroundMusicSource.Stop();
        AudioManager.Instance.PlaySFX("YouDie");
    }

}
