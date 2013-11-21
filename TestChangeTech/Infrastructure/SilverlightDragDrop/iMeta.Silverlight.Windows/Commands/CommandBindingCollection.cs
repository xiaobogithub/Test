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
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace iMeta.Windows.Commands
{
   public class CommandBindingCollection : Collection<CommandBinding>
   {
      private int _updateCount;

      protected override void ClearItems()
      {
         base.ClearItems();
         Updated();
      }

      protected override void InsertItem(int index, CommandBinding item)
      {
         base.InsertItem(index, item);
         Updated();
      }

      protected override void RemoveItem(int index)
      {
         base.RemoveItem(index);
         Updated();
      }

      protected override void SetItem(int index, CommandBinding item)
      {
         base.SetItem(index, item);
         Updated();
      }

      public CommandBinding FindMatch(ICommand command)
      {
         for (int i=0, n=Count; i<n; i++)
         {
            var binding = this[i];
            if (binding.Command == command)
               return binding;
         }
         return null;
      }

      public int BeginUpdate()
      {
         return ++_updateCount;
      }

      public void EndUpdate()
      {
         if (_updateCount == 0)
            throw new InvalidOperationException("No corresponding end update.");
         _updateCount--;
         
         Updated();
      }

      private void Updated()
      {
         if (_updateCount == 0)
            CommandManager.InvalidateRequerySuggested();
      }
   }
}
