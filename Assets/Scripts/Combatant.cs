using Sirenix.OdinInspector;
using UnityEngine;

public abstract class Combatant : MonoBehaviour
{
    [SerializeField, ReadOnly] protected int currentGold;
    [SerializeField, ReadOnly] int currentHealth;

    public abstract CharacterInformation CharacterInformation { get; }

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
                if (action.HasRange)
                {
                    target.TakeDamage(Random.Range(action.ActionMin, action.ActionMax + 1));
                }
                else
                {
                    target.TakeDamage(action.ActionStat);
                }
                break;
            case ActionTypes.Heal:
                if (action.HasRange)
                {
                    target.HealDamage(Random.Range(action.ActionMin, action.ActionMax + 1));
                }
                else
                {
                    target.HealDamage(action.ActionStat);
                }
                break;
            case ActionTypes.Steal:
                if (action.HasRange)
                {
                    GainGold(target.LoseGold(Random.Range(action.ActionMin, action.ActionMax + 1)));
                }
                else 
                { 
                    GainGold(target.LoseGold(action.ActionStat));
                }
                break;
            case ActionTypes.Pass:
                break;
        }

        if (target.IsDead)
        {
            GainGold(target.LoseGold(target.CurrentGold));
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
