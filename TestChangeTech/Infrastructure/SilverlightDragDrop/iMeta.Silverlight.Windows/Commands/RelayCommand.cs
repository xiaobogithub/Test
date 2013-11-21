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
	public class RelayCommand<T> : ICommand
	{
		#region Fields

		private readonly Action<T> _execute;
		private readonly Func<T, bool> _canExecute;

		#endregion

		public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
		{
			#region Validate Arguments
			
			Guard.ArgumentNull("execute", canExecute);
			
			#endregion

			_execute = execute;
			_canExecute = canExecute;
		}

		public bool CanExecute(T parameter)
		{
			return _canExecute == null || 
				_canExecute(parameter);
		}

		public void Execute(T parameter)
		{
			if (CanExecute(parameter))
				_execute(parameter);
		}

		bool ICommand.CanExecute(object parameter)
		{
			return CanExecute((T)parameter);
		}

		void ICommand.Execute(object parameter)
		{
			Execute((T)parameter);
		}

		public event EventHandler CanExecuteChanged
		{
			add
			{
				CommandManager.RequerySuggested += value;
			}
			remove
			{
				CommandManager.RequerySuggested -= value;
			}
		}
	}
}
