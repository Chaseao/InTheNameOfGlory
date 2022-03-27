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

    int lastGold;

    public void Display(Combatant combatant, bool hideGold = false)
    {
        combatantImage.enabled = true;
        combatantAnimation.enabled = true;
        combatantAnimation.runtimeAnimatorController = combatant.CharacterInformation.Animation;
        combatantName.text = combatant.CharacterInformation.CharacterName;
        healthIcon.enabled = true;
        combatantHealth.text = combatant.CurrentHealth + " / " + combatant.CharacterInformation.MaxHealth;
        goldIcon.enabled = true;

        if (!hideGold)
        {
            lastGold = combatant.CurrentGold;
            combatantGold.text = "x " + combatant.CurrentGold;
        }
        else
        {
            combatantGold.text = "x " + lastGold;
        }

        backgroundImage.enabled = true;
        backgroundBorder.enabled = true;
    }

    public void Clear(bool hideGold = false)
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
