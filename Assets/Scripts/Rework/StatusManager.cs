using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    private readonly List<StatusEffect> activeEffects = new();

    public event Action OnStatusesChanged;

    [SerializeField]
    private List<StatusDebug> debugEffects = new();

    public List<StatusEffect> ActiveEffects => activeEffects;

    public void AddStatus(StatusEffect effect)
    {
        foreach (var active in activeEffects)
        {
            if (active.Name == effect.Name)
            {
                active.AddStacks(effect.Stacks);
                UpdateStatusDebug();
                return;
            }
        }

        activeEffects.Add(effect);
        effect.OnApply();

        UpdateStatusDebug();

    }

    private void UpdateStatusDebug()
    {
        debugEffects.Clear();

        foreach (StatusEffect e in activeEffects)
        {
            debugEffects.Add(new StatusDebug
            {
                Name = e.Name,
                Stacks = e.Stacks
            });
        }

        OnStatusesChanged?.Invoke();
    }

    public async UniTask OnTurnStart()
    {
        for (int i = activeEffects.Count - 1; i >= 0; i--)
        {
            await activeEffects[i].OnTurnStart();

            if (activeEffects[i].IsFinished)
            {
                activeEffects[i].OnRemove();
                activeEffects.RemoveAt(i);
            }
        }

        UpdateStatusDebug();
    }

    public async UniTask OnTurnEnd()
    {
        for (int i = activeEffects.Count - 1; i >= 0; i--)
        {
            await activeEffects[i].OnTurnEnd();

            if (activeEffects[i].IsFinished)
            {
                activeEffects[i].OnRemove();
                activeEffects.RemoveAt(i);
            }
        }

        UpdateStatusDebug();
    }
}

[System.Serializable]
public class StatusDebug
{
    public string Name;
    public int Stacks;
}