using System.Collections;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public HealthBar healthBar;
    public float healthRegenDelay = 0.1f;
    public int currentHealth { get; private set; }
    public bool isDead { get; private set; }

    public Stat damage;
    public Stat armor;
    public int level = 1;

    private RagdollController ragdollController;
    private WaitForSeconds regenTick;
    protected Coroutine regen;

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;
        healthBar?.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            StopCoroutine(regen);
            Die();
        }
        else
        {
            if (regen != null)
                StopCoroutine(regen);
                
            regen = StartCoroutine(RegenHealth());
        }
    }

    public virtual void Die()
    {
        ragdollController.DoRagdoll();
        isDead = true;
    }

    protected IEnumerator RegenHealth()
    {
        yield return new WaitForSeconds(2);

        while (currentHealth < maxHealth)
        {
            currentHealth += maxHealth / 100;
            healthBar?.SetHealth(currentHealth);

            yield return regenTick;
        }

        regen = null;
    }

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        ragdollController = GetComponent<RagdollController>();
        regenTick = new WaitForSeconds(healthRegenDelay);

        healthBar?.SetMaxHealth(maxHealth);
    }
}
