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
using System.Collections.Generic;

namespace iMeta.Windows.Commands
{
   public class CommandManager
   {
      private static ICommandRouter _commandRouter;
      private static List<WeakReference> _requerySuggestedHandlers;

      public static ICommandRouter CommandRouter
      {
         get
         {
            if (_commandRouter == null)
               _commandRouter = new CommandRouter();

            return _commandRouter;
         }
         set
         {
            if (_commandRouter != value)
            {
               _commandRouter = value;
               InvalidateRequerySuggested();
            }
         }
      }

      public static void InvalidateRequerySuggested()
      {
         var dispatcher = ApplicationDispatcher.Current;
         if (dispatcher != null)
            dispatcher.BeginInvoke(() => RaiseWeakEvents(_requerySuggestedHandlers));
      }

      internal static ICommandRouter GetCommandRouter(RoutedCommand command)
      {
         return CommandRouter;
      }

      public static event EventHandler RequerySuggested
      {
         add
         {
            if (_requerySuggestedHandlers == null)
               _requerySuggestedHandlers = new List<WeakReference>();
            AddWeakEventHandler(_requerySuggestedHandlers, value);
         }
         remove
         {
            RemoveWeakEventHandler(_requerySuggestedHandlers, value);
         }
      }

      private static void RemoveWeakEventHandler(IList<WeakReference> handlers, EventHandler handler)
      {
         if (handlers == null)
            return;

         for (var i = handlers.Count - 1; i >= 0; i--)
         {
            var target = handlers[i].Target as EventHandler;
            if (target == null || target == handler)
               handlers.RemoveAt(i);
         }
      }

      private static void AddWeakEventHandler(IList<WeakReference> handlers, EventHandler handler)
      {
         handlers.Add(new WeakReference(handler));
      }

      private static void RaiseWeakEvents(IList<WeakReference> handlers)
      {
         if (handlers != null)
         {
            var handlerArray = new EventHandler[handlers.Count];
            var index = 0;
            for (var i = handlers.Count - 1; i >= 0; i--)
            {
               var target = handlers[i].Target as EventHandler;
               if (target == null)
               {
                  handlers.RemoveAt(i);
               }
               else
               {
                  //  safe target as strong reference
                  handlerArray[index] = target;
                  index++;
               }
            }
            for (var j = 0; j < index; j++)
               handlerArray[j](null, EventArgs.Empty);
         }
      }
   }
}