using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CombatantDisplayer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI combatantName;
    [SerializeField] TextMeshProUGUI combatantHealth;
    [SerializeField] TextMeshProUGUI combatantGold;
    [SerializeField] Image backgroundImage;
    
    public void Display(Combatant combatant)
    {
        combatantName.text = combatant.CharacterInformation.CharacterName;
        combatantHealth.text = "Health: " + combatant.CurrentHealth + " / " + combatant.CharacterInformation.MaxHealth;
        combatantGold.text = "Current Gold: " + combatant.CurrentGold;
        backgroundImage.enabled = true;
    }

    public void Clear()
    {
        combatantName.text = "";
        combatantHealth.text = "";
        combatantGold.text = "";
        backgroundImage.enabled = false;
    }
}
