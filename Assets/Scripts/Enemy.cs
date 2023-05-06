using UnityEngine;
using TMPro;
using System.Collections;

public class Enemy : MonoBehaviour, IHitable
{
    [Header("==== Another ====")]
    [Space(10)]
    [SerializeField] private int health = 5;
    [SerializeField] private int damageToPlayer = 1;
    [SerializeField] private GameObject amountOfDamagePrefab;
    public float duration = 1f;
    public float yOffset = 0.5f;
    public float destroyDelay = 0.5f;
    [SerializeField] private GameObject ExpObjectToDrop;
    [SerializeField] private GameObject healthSpritePrefab;
    [SerializeField] private float dropChance;
    public Canvas canvas;
    public int moneyForKill;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damageToPlayer);
            Debug.Log("Dostajesz DMG");
        }
    }
    void BoucingEffectExpObject()
    {
        if (ExpObjectToDrop == null)
        {
            return;
        }
        GameObject droppedObject = Instantiate(ExpObjectToDrop, transform.position, Quaternion.identity);
        Rigidbody2D rb = droppedObject.AddComponent<Rigidbody2D>();
        float bounceVelocity = 0.5f;
        Vector2 bounceDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        rb.velocity = bounceDirection * bounceVelocity;
        rb.isKinematic = true;
        float timeUntilRemove = 0.5f;
        Destroy(rb, timeUntilRemove);
    }

    void BoucingEffectHealthSprite()
    {
        if (healthSpritePrefab == null)
        {
            return;
        }
        GameObject healthSprite = Instantiate(healthSpritePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = healthSprite.AddComponent<Rigidbody2D>();
        float bounceVelocity = 0.5f;
        Vector2 bounceDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        rb.velocity = bounceDirection * bounceVelocity;
        rb.isKinematic = true;
        float timeUntilRemove = 0.5f;
        Destroy(rb, timeUntilRemove);
    }

    private void ShowDamageText(int damageAmount)
    {
        var textObject = Instantiate(amountOfDamagePrefab, canvas.transform);
        textObject.GetComponentInChildren<TextMeshProUGUI>().text = damageAmount.ToString();
        // Dodaj efekt przesuniêcia w pionie
        Vector3 randomOffset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(0f, yOffset), 0);
        textObject.transform.position += randomOffset;
        StartCoroutine(DestroyAfterDelay(textObject, destroyDelay));
    }


    private IEnumerator DestroyAfterDelay(GameObject gameObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        float fadeTime = 0.5f;
        float alpha = 1f;
        while (alpha > 0f)
        {
            alpha -= Time.deltaTime / fadeTime;
            var textColor = gameObject.GetComponentInChildren<TextMeshProUGUI>().color;
            textColor.a = alpha;
            gameObject.GetComponentInChildren<TextMeshProUGUI>().color = textColor;
            yield return null;
        }
        Destroy(gameObject);
    }

    public void ReceiveHit(RaycastHit2D hit)
    {
        if (health <= 0)
        {
            return;
        }
        var hitObject = hit.collider.gameObject;
        var hitComponent = hitObject.GetComponent<IHitable>();
        if (hitComponent == null)
        {
            Debug.LogError($"Object {hitObject.name} does not implement IHitable");
            return;
        }
        var playerLevel = FindObjectOfType<ExperienceSystem>();
        var damage = playerLevel.GetDamage();
        hitComponent.TakeDamage(damage);
    }
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }

        ShowDamageText(damageAmount);
    }
    public void DropHealthForPlayer()
    {
        if (Random.Range(0, 100) < dropChance)
        {
            healthSpritePrefab.GetComponent<HealthSpriteGoToPlayer>().SetTarget(FindObjectOfType<PlayerHealth>().gameObject);
            BoucingEffectHealthSprite();
        }
    }

    public void DropExpForPlayer()
    {
        ExpObjectToDrop.GetComponent<XPItem>().SetTarget(FindObjectOfType<ExperienceSystem>().gameObject);
        BoucingEffectExpObject();
    }

    void Die()
    {
        Destroy(gameObject);
        DropHealthForPlayer();
        DropExpForPlayer();
        //kasa któr¹ dotajesz 
        MoneyManager.instance.AddMoney(moneyForKill);
    }
}
