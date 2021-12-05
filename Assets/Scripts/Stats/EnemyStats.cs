using UnityEngine;

public class EnemyStats : CharacterStats
{
    public delegate void EnemyKilled(int instanceId);
    public static event EnemyKilled OnEnemyKilled;

    public delegate void ExperienceEarned(int enemyLevel);
    public static event ExperienceEarned OnExperienceEarned;

    public override void Die()
    {
        base.Die();

        OnEnemyKilled?.Invoke(gameObject.GetInstanceID());
        OnExperienceEarned?.Invoke(level);
    }
}
