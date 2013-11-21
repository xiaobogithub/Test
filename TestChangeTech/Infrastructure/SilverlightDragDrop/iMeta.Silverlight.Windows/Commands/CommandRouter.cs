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

using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace iMeta.Windows.Commands
{
   public class CommandRouter : ICommandRouter
   {
      private readonly CommandBindingCollection _commandBindings;

      public CommandRouter()
      {
         _commandBindings = new CommandBindingCollection();
      }

      public void Execute(ICommand command, object context, object parameter)
      {
         var binding = FindBinding(context, command);
         if (binding != null)
            binding.Execute(parameter);
      }
      
      public bool CanExecute(ICommand command, object context, object parameter)
      {
         var binding = FindBinding(context, command);
         return binding != null &&
            binding.CanExecute(parameter);
      }

      private CommandBinding FindBinding(object context, ICommand command)
      {
         while (context != null)
         {
            var contextAsBindingSource = context as ICommandBindingSource;
            if (contextAsBindingSource != null)
            {
               var result = contextAsBindingSource.FindBinding(command);
               if (result != null)
                  return result;
            }
            context = GetContextParent(context);
         }
         return _commandBindings.FirstOrDefault((cb) => cb.Command == command);
      }

      protected virtual object GetContextParent(object context)
      {
         var dependencyObject = context as DependencyObject;

         //   walk up visual tree
         return dependencyObject != null ? VisualTreeHelper.GetParent(dependencyObject) : null;
      }


      public CommandBindingCollection CommandBindings
      {
         get { return _commandBindings; }
      }
   }
}