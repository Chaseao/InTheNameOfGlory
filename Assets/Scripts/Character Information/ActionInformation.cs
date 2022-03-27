using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class ActionInformation
{
    [SerializeField] string actionName;
    [SerializeField] ActionTypes actionType;

    [SerializeField] bool hasRange;
    [ShowIf("@hasRange == true")]
    [SerializeField] int actionMin;
    [ShowIf("@hasRange == true")]
    [SerializeField] int actionMax;
    [ShowIf("@hasRange == false")]
    [SerializeField] int actionStat;

    public string ActionName => actionName;
    public ActionTypes Type => actionType;
    public bool HasRange => hasRange;
    public int ActionMin => actionMin;
    public int ActionMax => actionMax;
    public int ActionStat => actionStat;
}

public enum ActionTypes
{
    Damage,
    Heal,
    Steal,
    Pass
}