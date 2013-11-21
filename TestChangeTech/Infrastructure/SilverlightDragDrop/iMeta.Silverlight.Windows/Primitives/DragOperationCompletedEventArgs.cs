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
using System.Windows.Input;

namespace iMeta.Windows.Primitives
{
    public class DragOperationCompletedEventArgs : DragCompletedEventArgs, IMouseEventArgs
    {
        private readonly MouseEventArgs _innerEvent;

        public DragOperationCompletedEventArgs(MouseEventArgs innerEvent, double horizontalChange, double verticalChange, bool canceled) : base(horizontalChange, verticalChange, canceled)
        {
            _innerEvent = innerEvent;
        }

        public Point GetPosition(UIElement element)
        {
            return _innerEvent.GetPosition(element);
        }
    }
}
