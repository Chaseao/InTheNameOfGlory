using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CombatantDisplayer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI combatantName;
    [SerializeField] Image combatantImage;
    [SerializeField] Animator combatantAnimation;
    [SerializeField] Image healthIcon;
    [SerializeField] TextMeshProUGUI combatantHealth;
    [SerializeField] Image goldIcon;
    [SerializeField] TextMeshProUGUI combatantGold;
    [SerializeField] Image backgroundImage;
    [SerializeField] Image backgroundBorder;

    public void Display(Combatant combatant)
    {
        combatantImage.enabled = true;
        combatantAnimation.enabled = true;
        combatantAnimation.runtimeAnimatorController = combatant.CharacterInformation.Animation;
        combatantName.text = combatant.CharacterInformation.CharacterName;
        healthIcon.enabled = true;
        combatantHealth.text = combatant.CurrentHealth + " / " + combatant.CharacterInformation.MaxHealth;
        goldIcon.enabled = true;
        combatantGold.text = "x "+ combatant.CurrentGold;
        backgroundImage.enabled = true;
        backgroundBorder.enabled = true;
    }

    public void Clear()
    {
        combatantName.text = "";
        combatantHealth.text = "";
        combatantGold.text = "";
        backgroundImage.enabled = false;
        combatantImage.enabled = false;
        healthIcon.enabled = false;
        goldIcon.enabled = false;
        backgroundBorder.enabled = false;
    }
}
