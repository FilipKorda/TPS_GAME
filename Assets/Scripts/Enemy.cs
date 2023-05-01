using UnityEngine;
using TMPro;
using System.Collections;

public class Enemy : MonoBehaviour, IHitable
{
    [SerializeField] private int health = 5;
    [SerializeField] private int damageToPlayer = 1;
    [SerializeField] private GameObject amountOfDamagePrefab;
    [SerializeField] private GameObject objectToDrop;
    public Canvas canvas;
    private ExperienceSystem experienceSystem;


    private void Awake()
    {
        experienceSystem = FindObjectOfType<ExperienceSystem>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damageToPlayer);
            Debug.Log("Dostajesz DMG");
        }
    }
    void BoucingEffect()
    {
        GameObject droppedObject = Instantiate(objectToDrop, transform.position, Quaternion.identity);
        Rigidbody2D rb = droppedObject.AddComponent<Rigidbody2D>();
        float bounceVelocity = 1f;
        rb.velocity = new Vector2(Random.Range(-bounceVelocity, bounceVelocity), bounceVelocity);
        rb.isKinematic = true;
        float timeUntilRemove = 0.5f;
        Destroy(rb, timeUntilRemove);
    }

    private void ShowDamageText(int damageAmount)
    {
        var textObject = Instantiate(amountOfDamagePrefab, canvas.transform);
        textObject.GetComponentInChildren<TextMeshProUGUI>().text = damageAmount.ToString();
        StartCoroutine(DestroyAfterDelay(textObject));
    }

    private IEnumerator DestroyAfterDelay(GameObject gameObject, float delay = 1f)
    {
        yield return new WaitForSeconds(delay);
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
    void Die()
    {
        Destroy(gameObject);
        BoucingEffect();
        MoneyManager.instance.AddMoney(1);
    }

}
