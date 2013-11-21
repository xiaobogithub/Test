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
    public sealed class CanExecuteCommandEventArgs : EventArgs
    {
        #region Fields

        private readonly ICommand _command;
        private readonly object _parameter;
        private bool _canExecute;

        #endregion

        internal CanExecuteCommandEventArgs(ICommand command, object parameter)
        {
            #region Validate Arguments

            Guard.ArgumentNull("command", command);

            #endregion
            
            _command = command;
            _parameter = parameter;
        }

        public ICommand Command
        {
            get
            {
                return _command;
            }
        }

        public object Parameter
        {
            get
            {
                return _parameter;
            }
        }

        public bool CanExecute
        {
            get
            {
                return _canExecute;
            }
            set
            {
                _canExecute = value;
            }
        }
    }
}