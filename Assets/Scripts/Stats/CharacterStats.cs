using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public Stat damage;
    public Stat armor;

    private RagdollController ragdollController;

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " died.");
        
        ragdollController.DoRagdoll();
    }

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        ragdollController = GetComponent<RagdollController>();
    }
}
