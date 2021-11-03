using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public Stat damage;
    public Stat armor;

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
        // Implement die
        Debug.Log(transform.name + " died.");

        GetComponent<Collider>().enabled = false;
        this.enabled = false;
        GetComponent<Animator>().enabled = false;
    }

    private void Awake()
    {
        currentHealth = maxHealth;
    }
}
