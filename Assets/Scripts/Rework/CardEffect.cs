using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class CardEffect : ScriptableObject
{
    [SerializeField]
    protected int value;

    [SerializeField]
    protected StatusData statusData;


    public abstract UniTask Execute(CardContext context);
}