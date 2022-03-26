using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

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

public class ActionTargetDisplayer : MonoBehaviour
{
    [SerializeField] Dictionary<Controller.Direction, ActionTargetDisplay> leftDisplays;
    [SerializeField] Dictionary<Controller.Direction, ActionTargetDisplay> rightDisplays;

    public void DisplayAction(Player player)
    {
        Clear();

        foreach(var action in player.Actions)
        {
            leftDisplays[action.Key].Display(action.Value.ActionName);
        }
    }

    public void DisplayTargets(Dictionary<Controller.Direction, Player> players, Dictionary<Controller.Direction, Enemy> enemies)
    {
        Clear();

        foreach(var player in players)
        {
            
        }
        
    }

    public void Clear()
    {
        foreach(var display in leftDisplays.Values)
        {
            display.Clear();
        }
        
        foreach(var display in rightDisplays.Values)
        {
            display.Clear();
        }
    }

}

class ActionTargetDisplay : MonoBehaviour
{
    [SerializeField] Image displayIcon;
    [SerializeField] TextMeshProUGUI displayText;

    public void Display(string text)
    {
        displayIcon.enabled = true;
        displayText.text = text;
    }

    public void Clear()
    {
        displayIcon.enabled = false;
        displayText.text = "";
    }
}
