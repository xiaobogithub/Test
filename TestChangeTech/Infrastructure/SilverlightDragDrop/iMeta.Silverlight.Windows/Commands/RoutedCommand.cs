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
   public class RoutedCommand : ICommand
   {
      private readonly string _name;
      private readonly Type _ownerType;

      public RoutedCommand(string name, Type ownerType)
      {
         _name = name;
         _ownerType = ownerType;
      }

      public string Name
      {
         get
         {
            return _name;
         }
      }

      public Type OwnerType
      {
         get
         {
            return _ownerType;
         }
      }

      public bool CanExecute(object context, object parameter)
      {
         var router = CommandManager.GetCommandRouter(this);
         return router != null &&
            router.CanExecute(this, context, parameter);
      }

      public void Execute(object context, object parameter)
      {
         var router = CommandManager.GetCommandRouter(this);
         if (router != null)
            router.Execute(this, context, parameter);
      }

      bool ICommand.CanExecute(object parameter)
      {
         return CanExecute(null, parameter);
      }

      void ICommand.Execute(object parameter)
      {
         Execute(null, parameter);
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