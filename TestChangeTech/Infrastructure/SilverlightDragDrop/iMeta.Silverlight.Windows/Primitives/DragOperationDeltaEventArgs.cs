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
    public class DragOperationDeltaEventArgs : DragDeltaEventArgs, IMouseEventArgs
    {
        private readonly MouseEventArgs _innerEvent;

        public DragOperationDeltaEventArgs(MouseEventArgs innerEvent, double horizontalChange, double verticalChange) : base(horizontalChange, verticalChange)
        {
            _innerEvent = innerEvent;
        }

        public Point GetPosition(UIElement element)
        {
            return _innerEvent.GetPosition(element);
        }
    }
}
