using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    public int xp = 0;
    public XpBar xpBar;
    public Text uiLvl;

    public delegate void PlayerKilled();
    public static event PlayerKilled OnPlayerKilled;

    private int nextLevel;

    private void OnEnable()
    {
        nextLevel = GetLevelXp(level + 1);

        uiLvl.text = level.ToString();
        xpBar.SetXp(xp);
        xpBar.SetMinMax(0, nextLevel);
        EnemyStats.OnExperienceEarned += AddExperience;
    }

    private void AddExperience(int enemyLevel)
    {
        xp += enemyLevel * 200;

        if (xp >= nextLevel)
            LevelUp();

        xpBar.SetXp(xp);
    }

    public override void Die()
    {
        base.Die();

        OnPlayerKilled?.Invoke();
    }

    private void LevelUp()
    {
        int prevLevel = nextLevel;
        level++;
        nextLevel = GetLevelXp(level + 1);

        uiLvl.text = level.ToString();
        xpBar.SetMinMax(prevLevel, nextLevel);

        maxHealth += 25;
        healthBar?.SetMaxHealth(maxHealth);
        healthBar?.SetHealth(currentHealth);

        regen = StartCoroutine(RegenHealth());
    }

    private int GetLevelXp(int level)
    {
        return Convert.ToInt32(500 * Math.Pow(level, 2) - (500 * level));
    }
}
