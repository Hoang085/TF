using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandHandler
{
    private Stack<ICommand> _commandList;

    public CommandHandler()
    {
        _commandList = new Stack<ICommand>();
    }
    
    public void AddCommand(ICommand command)
    {
        command.Execute();
        _commandList.Push(command);
    }

    public void UndoCommand()
    {
        if (_commandList.Count > 0)
        {
            ICommand lastestCommand = _commandList.Pop();
            lastestCommand.Undo();
        }
    }
}



