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
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace iMeta.Windows
{
   public static class VisualSearch
   {
      public static FrameworkElement FindName(DependencyObject reference, string name)
      {
         return FindName<FrameworkElement>(reference, name);
      }

      public static T FindName<T>(DependencyObject reference, string name) where T : FrameworkElement
      {
         Guard.ArgumentNull("name", name);
         Guard.ArgumentNull("reference", reference);
         return (T)Find(reference, dp => MatchName<T>(dp, name));
      }

      public static DependencyObject Find(DependencyObject reference, Func<DependencyObject, bool> predicate)
      {
         return FindAll(reference, predicate, VisualSearchOptions.None).FirstOrDefault();
      }

      public static DependencyObject Find<T>(DependencyObject reference, Func<T, bool> predicate) where T : DependencyObject
      {
         return Find(reference, dp => MatchPredicate(dp, predicate));
      }

      public static IEnumerable<DependencyObject> FindAll(DependencyObject reference, Func<DependencyObject, bool> predicate,
         VisualSearchOptions options)
      {
         var queue = new Queue<DependencyObject>(128);
         queue.Enqueue(reference);

         var found = false;
         var currDepthCount = 1;
         var nextDepthCount = 0;
         while (queue.Count > 0)
         {
            if (currDepthCount == 0)
            {
               if (found && IsSet(options, VisualSearchOptions.MatchDepth))
                  yield break;

               currDepthCount = nextDepthCount;
               nextDepthCount = 0;
            }

            //	dequeue next item
            var parent = queue.Dequeue();
            currDepthCount--;

            //	search within children
            var childCount = VisualTreeHelper.GetChildrenCount(parent);
            for (var i = 0; i < childCount; i++)
            {
               //	get nextr child
               var child = VisualTreeHelper.GetChild(parent, i);
               //	is it a match?
               var isMatch = predicate(child);
               //	enqueue item if there was no match or we intend to search deeper
               if (!isMatch || (IsSet(options, VisualSearchOptions.WithinMatched) && !IsSet(options, VisualSearchOptions.MatchDepth)))
               {
                  queue.Enqueue(child);
                  nextDepthCount++;
               }
               if (isMatch)
               {
                  found = true;
                  yield return child;
               }
            }
         }
      }

      public static IEnumerable<T> FindAll<T>(DependencyObject reference, Func<T, bool> predicate,
         VisualSearchOptions options) where T : FrameworkElement
      {
         return FindAll(reference, dp => MatchPredicate<T>(dp, predicate), options).Cast<T>();
      }

      public static IEnumerable<T> FindAll<T>(DependencyObject reference, VisualSearchOptions options) where T : FrameworkElement
      {
         return FindAll(reference, dp1 => MatchPredicate<T>(dp1, dp2 => true), options).Cast<T>();
      }

      private static bool IsSet(VisualSearchOptions options, VisualSearchOptions option)
      {
         return (options & option) == option;
      }

      private static bool MatchName<T>(DependencyObject dp, string name) where T : FrameworkElement
      {
         var element = dp as T;
         return element != null && element.Name == name;
      }

      private static bool MatchPredicate<T>(DependencyObject dp, Func<T, bool> predicate) where T : DependencyObject
      {
         var element = dp as T;
         return element != null && predicate(element);
      }
   }
}