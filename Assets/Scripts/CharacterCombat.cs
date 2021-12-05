using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterCombat : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = .5f;
    public float attackDelay = .6f;
    public float attackSpeed = 2f;
    public LayerMask enemyLayers;

    private Animator animator;
    private CharacterStats characterStats;
    private float attackCooldown = 0f;

    public void Attack()
    {
        if (attackPoint == null)
        {
            Debug.LogWarning("Attack Point must be set in order to attack!");

            return;
        }

        if (attackCooldown > 0f)
            return;

        animator.SetTrigger("Attack");

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach(var enemy in hitEnemies)
        {
            Debug.Log("Hit to enemy " + enemy.name);
            CharacterStats enemyStats = enemy.GetComponent<CharacterStats>();

            if (enemyStats != null)
                StartCoroutine(DoDamage(enemyStats, attackDelay));

            attackCooldown = 1f / attackSpeed;
        }
    }

    private IEnumerator DoDamage(CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);

        stats.TakeDamage(25 + characterStats.level * 5);
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        characterStats = GetComponent<CharacterStats>();
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
