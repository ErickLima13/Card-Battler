using Assets.Scripts.Rework;

public class CardContext
{
    public Unit Source { get; }
    public BattleManager BattleManager { get; }

    public CardData Card { get; }

    public CardContext(Unit source, CardData card, BattleManager battle)
    {
        Source = source;
        Card = card;
        BattleManager = battle;
    }
}