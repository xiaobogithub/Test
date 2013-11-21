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
    public sealed class ExecuteCommandEventArgs : EventArgs
    {
        private readonly ICommand _command;
        private readonly object _parameter;
       
        internal ExecuteCommandEventArgs(ICommand command, object parameter)
        {
            _command = command;
            _parameter = parameter;
        }

        
        public ICommand Command { get { return _command; }}
        public object Parameter { get { return _parameter; }}
    }
}