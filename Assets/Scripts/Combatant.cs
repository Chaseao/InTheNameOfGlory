using Sirenix.OdinInspector;
using UnityEngine;

public abstract class Combatant : MonoBehaviour
{
    [SerializeField, ReadOnly] protected int currentGold;
    [SerializeField, ReadOnly] int currentHealth;

    protected abstract CharacterInformation CharacterInformation { get; }

    public int CurrentGold
    {
        get
        {
            return currentGold;
        }
        set
        {
            currentGold = Mathf.Max(0, value);
        }
    }
    public int CurrentHealth
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = Mathf.Max(0, value);
            currentHealth = Mathf.Min(currentHealth, CharacterInformation.MaxHealth);
        }
    }

    public bool IsDead => currentHealth <= 0;

    private void OnEnable()
    {
        ResetCharacter();
    }

    public void ResetCharacter()
    {
        ResetHealth();
        ResetGold();
    }

    protected void PerformAction(Combatant target, ActionInformation action)
    {
        switch (action.Type)
        {
            case ActionTypes.Damage:
                target.TakeDamage(action.ActionStat);
                break;
            case ActionTypes.Heal:
                target.HealDamage(action.ActionStat);
                break;
            case ActionTypes.Steal:
                GainGold(target.LoseGold(action.ActionStat));
                break;
            case ActionTypes.Pass:
                break;
        }
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        if (IsDead)
        {
            Die();
        }
    }

    public void HealDamage(int healAmmount)
    {
        CurrentHealth += healAmmount;
    }

    public void ResetHealth()
    {
        currentHealth = CharacterInformation.MaxHealth;
    }

    protected abstract void Die();

    public void GainGold(int amount)
    {
        CurrentGold += amount;
    }

    public int LoseGold(int amount)
    {
        int goldStolen = amount;

        if (amount > CurrentGold)
        {
            goldStolen = CurrentGold;
        }

        CurrentGold -= amount;

        return goldStolen;
    }

    protected abstract void ResetGold();


}
