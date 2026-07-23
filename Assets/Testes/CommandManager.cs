using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    public IEntity entity;
    public ICommand command;
    public List<ICommand> commands;

    readonly CommandInvoker commandInvoker = new CommandInvoker();

    private bool isExecuting;

    private void Start()
    {
        entity = GetComponent<IEntity>();

        command = HeroCommand.Create<AttackCommand>(entity);

        commands = new()
            {
                HeroCommand.Create<AttackCommand>(entity),
                HeroCommand.Create<ReturnCommand>(entity),
                HeroCommand.Create<HitCommand>(entity)
            };
    }

    async UniTask ExecuteCommand(List<ICommand> commands)
    {
        isExecuting = true;
        await commandInvoker.ExecuteCommand(commands);
        isExecuting = false;
    }

    private void Update()
    {
        if (isExecuting) { return; }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ExecuteCommand(new() { command });
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            ExecuteCommand(commands);
        }
    }
}
