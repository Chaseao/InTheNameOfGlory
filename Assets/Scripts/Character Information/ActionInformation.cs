using UnityEngine;

[System.Serializable]
public class ActionInformation
{
    [SerializeField] string actionName;
    [SerializeField] ActionTypes actionType;
    [SerializeField] bool groupAction;
    [SerializeField] int actionStat;
    [SerializeField, Range(0, 1)] float failChance;

    public string ActionName => actionName;
    public ActionTypes Type => actionType;
    public bool GroupAction => groupAction;
    public int ActionStat => actionStat;
    public float FailChance => failChance;
}

public enum ActionTypes
{
    Damage,
    Heal,
    Steal,
    Pass
}