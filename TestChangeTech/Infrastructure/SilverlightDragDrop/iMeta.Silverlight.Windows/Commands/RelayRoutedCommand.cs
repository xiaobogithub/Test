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
	public class RelayRoutedCommand : ICommand
	{
		private readonly object _context;
		private readonly RoutedCommand _command;

		public RelayRoutedCommand(object context, RoutedCommand command)
		{
			_context = context;
			_command = command;
		}

		bool ICommand.CanExecute(object parameter)
		{
			return _command.CanExecute(_context, parameter);
		}

		void ICommand.Execute(object parameter)
		{
			_command.Execute(_context, parameter);
		}

		public event EventHandler CanExecuteChanged
		{
			add
			{
				_command.CanExecuteChanged += value;
			}
			remove
			{
				_command.CanExecuteChanged -= value;
			}
		}
	}
}
