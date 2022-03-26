using UnityEngine;

public abstract class Combatant : MonoBehaviour
{
    [SerializeField] protected CharacterInformation characterInformation;

    private void OnEnable()
    {
        characterInformation.ResetHealth();
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
        characterInformation.CurrentHealth -= damage;

        if (characterInformation.IsDead)
        {
            Die();
        }
    }

    public void HealDamage(int healAmmount)
    {
        characterInformation.CurrentHealth += healAmmount;
    }

    public void GainGold(int amount)
    {
        characterInformation.CurrentGold += amount;
    }

    public int LoseGold(int amount)
    {
        int goldStolen = amount;

        if (amount > characterInformation.CurrentGold)
        {
            goldStolen = characterInformation.CurrentGold;
        }

        characterInformation.CurrentGold -= amount;

        return goldStolen;
    }


    protected abstract void Die();
}
