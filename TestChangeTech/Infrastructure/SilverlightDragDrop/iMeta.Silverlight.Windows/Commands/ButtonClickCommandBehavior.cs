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

using System.Windows;
using System.Windows.Controls.Primitives;

namespace iMeta.Windows.Commands
{
    public class ButtonClickCommandBehavior : CommandBehavior<ButtonBase>
    {
        public ButtonClickCommandBehavior(ButtonBase button) : base(button)
        {
            button.Click += OnClick;
        }
      
        private void OnClick(object sender, RoutedEventArgs e)
        {
            ExecuteCommand();
        }
    }
}