using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class Health : MonoBehaviour
{
    [Header("Health")]
    [Tooltip("Maximum health.")]
    [Min(0)]
    public int maxHealth = 100;
    [Tooltip("Whether or not health will regenerate.")]
    public bool healthRegeneration = false;

    [Header("Shield")]
    [Tooltip("Whether or not there is a shield.")]
    public bool hasShield = true;
    [Tooltip("Maximum shield.")]
    [Min(0)]
    public int maxShield = 100;
    [Tooltip("Whether or not shield will regenerate.")]
    public bool shieldRegeneration = true;

    [Header("Regeneration")]
    [Tooltip("How long (in seconds) after taking damage before health/shield will begin to regenerate.")]
    [Min(0f)]
    public float regenerationDelay = 2f;
    [Tooltip("How quickly (in units per second) health/shield will regenerate.")]
    [Min(0f)]
    public float regenerationRate = 20f;

    [Header("Death")]
    [Tooltip("Whether or not to destroy this object when health reaches zero.")]
    public bool destroyOnDeath = true;
    [Tooltip("Invoked when health reaches zero.")]
    public UnityEvent<GameObject> onDeath = new();


    [HideInInspector]
    public int health;
    [HideInInspector]
    public int shield;
    [HideInInspector]
    public bool isRegenerating = false;


    private float timeSinceRegenerated = 0f;


    public void Damage(int damage)
    {
        if (hasShield)
        {
            int shieldDamage = Mathf.Min(shield, damage);
            shield -= shieldDamage;
            damage -= shieldDamage;
        }
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            onDeath.Invoke(gameObject);
            if(destroyOnDeath)
            {
                Destroy(gameObject);
            }
        }
        StartCoroutine(DelayRegeneration());
    }


    private void Start()
    {
        health = maxHealth;
        shield = maxShield;
    }

    private void Update()
    {
        if (!isRegenerating) return;
        timeSinceRegenerated += Time.deltaTime;
        int regenerationAmount = Mathf.FloorToInt(timeSinceRegenerated * regenerationRate);
        timeSinceRegenerated -= regenerationAmount / regenerationRate;
        if (healthRegeneration && health < maxHealth)
        {
            int healthRegenerationAmount = Mathf.Min(maxHealth - health, regenerationAmount);
            health += healthRegenerationAmount;
            regenerationAmount -= healthRegenerationAmount;
        }
        if (hasShield && shieldRegeneration && shield < maxShield)
        {
            int shieldRegenerationAmount = Mathf.Min(maxShield - shield, regenerationAmount);
            shield += shieldRegenerationAmount;
            regenerationAmount -= shieldRegenerationAmount;
        }
        if (regenerationAmount > 0)
        {
            isRegenerating = false;
        }
    }


    private IEnumerator DelayRegeneration()
    {
        isRegenerating = false;
        yield return new WaitForSeconds(regenerationDelay);
        isRegenerating = true;
    }
}
