using UnityEngine;

public abstract class Combatant : MonoBehaviour
{
    [SerializeField] protected CharacterInformation characterInformation;

    public void TakeDamage(int damage)
    {
        characterInformation.CurrentHealth -= damage;

        if (characterInformation.IsDead)
        {
            Die();
        }
    }

    protected abstract void Die();
}
