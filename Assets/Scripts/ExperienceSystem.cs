using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceSystem : MonoBehaviour
{
    public int currentLevel = 0;
    public int currentXP = 0;
    public int xpToNextLevel = 100;

    public TextMeshProUGUI levelText;
    public Slider xpSlider;

    public Gradient gradient;
    [SerializeField] private Animator levelUp;
    [SerializeField] private GameObject levelUpParticles;
    [SerializeField] private PlayerController playerController;

    //Increase damage
    public int baseDamage = 1;
    public float damageIncreaseFactor = 0.1f;

    private void Start()
    {
        levelText.text = "" + currentLevel.ToString();
    }
    public int GetDamage()
    {
        return Mathf.RoundToInt(baseDamage * Mathf.Pow(1 + damageIncreaseFactor, currentLevel - 1));
    }

    public void AddXP(int xpAmount)
    {
        currentXP += xpAmount;
        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }

        UpdateUI();
    }

    private void LevelUp()
    {
        currentLevel++;
        currentXP -= xpToNextLevel;
        levelUp.SetTrigger("LevelUp");
        GameObject particles = Instantiate(levelUpParticles, transform.position, Quaternion.identity);
        // Play the particle system
        particles.GetComponent<ParticleSystem>().Play();
        xpToNextLevel *= 2;

        //Increase damage
        int previousDamage = Mathf.RoundToInt(baseDamage * Mathf.Pow(1 + damageIncreaseFactor, currentLevel - 2));
        int newDamage = Mathf.RoundToInt(baseDamage * Mathf.Pow(1 + damageIncreaseFactor, currentLevel - 1));
        int damageIncrease = newDamage - previousDamage;
        Debug.Log($"Damage increased by {damageIncrease} after leveling up to level {currentLevel}");

        //Increase  TimeBetweenShots
        playerController._timeBetweenShots *= 0.5f;
    }

    private void UpdateUI()
    {
        levelText.text = "" + currentLevel.ToString();
        xpSlider.maxValue = xpToNextLevel;
        xpSlider.value = currentXP;

        float percentage = (float)currentXP / xpToNextLevel;
        xpSlider.fillRect.GetComponent<Image>().color = gradient.Evaluate(percentage);
    }
}
