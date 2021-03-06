﻿using MoveUp.Commands;
using MoveUp.Factories.Interfaces;
using System;
using System.Linq.Expressions;
using System.Windows.Input;

namespace MoveUp.Factories
{
    public class CommandFactory : ICommandFactory
    {
        public ICommand CreateCommand(Action execute, Expression<Func<bool>> canExecute = null)
        {
            return new Command(execute, canExecute?.Compile());
        }

        public ICommand CreateCommand<T>(Action<T> execute, Expression<Func<T, bool>> canExecute = null)
        {
            return new Command<T>(execute, canExecute?.Compile());
        }
    }
}