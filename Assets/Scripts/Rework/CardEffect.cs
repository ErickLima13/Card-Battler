using UnityEngine;

public abstract class CardEffect : ScriptableObject
{
    [SerializeField]
    protected int value;

    public abstract void Execute(CardContext context);
}