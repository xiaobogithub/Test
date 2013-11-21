//===============================================================================
// iMeta Technologies Ltd. (www.imeta.co.uk)
//===============================================================================
// Copyright © iMeta Technologies Ltd.  All rights reserved. 
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
// Please see LICENSE.txt distributed with this software for full licensing 
// details.
//===============================================================================

using System;
using System.Windows.Input;

namespace iMeta.Windows.Commands
{
    public class CommandBinding
    {
        #region Fields
        
        private readonly ICommand _command;
        private readonly EventHandler<ExecuteCommandEventArgs> _executed;
        private readonly EventHandler<CanExecuteCommandEventArgs> _canExecute;

        #endregion

        public CommandBinding(ICommand command, EventHandler<ExecuteCommandEventArgs> executed):
            this (command, executed, null) {}

        public CommandBinding(ICommand command, EventHandler<ExecuteCommandEventArgs> executed,
                              EventHandler<CanExecuteCommandEventArgs> canExecute)
        {
            #region Validate Arguments

            Guard.ArgumentNull("command", command);

            #endregion

            _command = command;
            _executed = executed;
            _canExecute = canExecute;
        }

        public ICommand Command
        {
            get 
            {
                return _command;
            }
        }

        internal void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                var handler = _executed;
                if (handler != null)
                    handler(this, new ExecuteCommandEventArgs(_command, parameter));
            }
        }

        internal bool CanExecute(object parameter)
        {
            //  cannot execute without an Executed handler
            if (_executed == null)
                return false;
            
            //  assume can execute is true if Executed is assigned and CanExecute is not
            var handler = _canExecute;
            if (handler == null)
                return true;
                
            //  query via event
            var args = new CanExecuteCommandEventArgs(_command, parameter);
            handler(this, args);
            return args.CanExecute;
        }
    }
}