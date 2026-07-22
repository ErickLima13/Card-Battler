using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "Scriptable Objects/CardData")]
public class CardData : ScriptableObject
{
    public string cardName;
    public string description;

    public int actionCost;

    public int poisonPower;

    public Sprite illustration;

    public List<CardEffect> effects = new();
}
