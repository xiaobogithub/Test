﻿//===============================================================================
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

namespace iMeta.Windows.DragDrop
{
   /// <summary>
   /// Specifies the effects of the drag-and-drop operation.
   /// </summary>
	[Flags]
	public enum DragDropEffects
	{
      /// <summary>
      /// The data is not accepted by the drop target.
      /// </summary>
		None = 0,
      /// <summary>
      /// The data is copied to the drop target.
      /// </summary>
		Copy = 1,
      /// <summary>
      /// The data is linked to the drop target .
      /// </summary>
		Link = 4,
      /// <summary>
      /// The data is moved to the drop target.
      /// </summary>
		Move = 2,
      /// <summary>
      /// Scrolling is occuring in the drop target.
      /// </summary>
		Scroll = -2147483648,
		/// <summary>
		/// All of the above.
		/// </summary>
      All = Copy | Link | Move | Scroll
	}
}