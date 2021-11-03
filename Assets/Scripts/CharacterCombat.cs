using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterCombat : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    private Animator animator;

    public void Attack()
    {
        if (attackPoint == null)
        {
            Debug.LogWarning("Attack Point must be set in order to attack!");

            return;
        }

        animator.SetTrigger("Attack");

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach(var enemy in hitEnemies)
        {
            Debug.Log("Hit to enemy " + enemy.name);
            CharacterStats characterStats = enemy.GetComponent<CharacterStats>();

            if (characterStats != null)
                characterStats.TakeDamage(30);
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
