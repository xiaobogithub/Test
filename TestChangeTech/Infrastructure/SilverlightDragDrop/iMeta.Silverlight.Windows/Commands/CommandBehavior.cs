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
using System.Windows.Controls;
using System.Windows.Input;

namespace iMeta.Windows.Commands
{
   public class CommandBehavior<T> where T : Control
   {
      #region Fields

      private ICommand _command;
      private object _commandParameter;
      private readonly WeakReference _target;
      private readonly EventHandler _canExecuteChangedHandler;

      #endregion

      public CommandBehavior(T target)
      {
         _target = new WeakReference(target);

         /* =============================================================================
          * We must host a strong reference to the handler as the command architecture 
          * uses a weak event strategy for CanExecuteChanged events.
          ============================================================================ */
         _canExecuteChangedHandler = new EventHandler(CommandCanExecuteChanged);
      }

      public ICommand Command
      {
         get { return _command; }
         set
         {
            if (_command != null)
            {
               _command.CanExecuteChanged -= _canExecuteChangedHandler;
            }
            _command = value;
            if (_command != null)
            {
               _command.CanExecuteChanged += _canExecuteChangedHandler;
               StateChanged();
            }
         }
      }

      public object CommandParameter
      {
         get
         {
            return _commandParameter;
         }
         set
         {
            if (_commandParameter != value)
            {
               _commandParameter = value;
               StateChanged();
            }
         }
      }

      protected T Target
      {
         get
         {
            return _target.Target as T;
         }
      }

      protected virtual void StateChanged()
      {
         //  prevent collection of target
         var target = Target;
         if (target != null)
         {
            target.IsEnabled = CanExecuteCommand(target);
         }
         else
         {
            Command = null;
            CommandParameter = null;
         }

         //  keep alive until this point
         GC.KeepAlive(target);
      }

      protected virtual bool CanExecuteCommand(object target)
      {
         if (_command == null)
            return false;
         
         // if bound to a routed command then invoke CanExecute with target as the context
         var routedCommand = _command as RoutedCommand;
         if (routedCommand != null)
            return routedCommand.CanExecute(target, _commandParameter);
         
         // if not a routed command call default CanExecute (No Context)
         return _command.CanExecute(_commandParameter);
      }

      protected virtual void ExecuteCommand()
      {
         // get strong reference to target
         var target = Target;
         
         // verify command and target
         if (_command == null || target == null)
            return;

         // if bound to a routed commend then invoke Execute with target as the context, otherwise call default ICommand.Execute
         var routedCommand = _command as RoutedCommand;
         if (routedCommand != null)
            routedCommand.Execute(target, CommandParameter);
         else
            _command.Execute(CommandParameter);
         
         // keep alive until this point
         GC.KeepAlive(target);
      }

      private void CommandCanExecuteChanged(object sender, EventArgs e)
      {
         StateChanged();
      }
   }
}