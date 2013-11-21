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

namespace iMeta.Windows.DragDrop
{
	/// <summary>
	/// Represents an element that can act as a target of drag-and-drop operations.
	/// </summary>
   public interface IDropTarget
	{
		/// <summary>
		/// Called when an object is dragged out of the targets's bounds.
		/// </summary>
		/// <param name="e">The <see cref="DragEventArgs"/> instance containing the event data.</param>
		void DragLeave(DragEventArgs e);
		
		/// <summary>
		/// Called when an object is dragged into the targets's bounds.
		/// </summary>
		/// <param name="e">The <see cref="DragEventArgs"/> instance containing the event data.</param>
		void DragEnter(DragEventArgs e);

		/// <summary>
		/// Called when an object is dragged over the targets's bounds.
		/// </summary>
		/// <param name="e">The <see cref="DragEventArgs"/> instance containing the event data.</param>
		void DragOver(DragEventArgs e);
      
		/// <summary>
		/// Called when a drag-and-drop operation is completed.
		/// </summary>
		/// <param name="e">The <see cref="DragEventArgs"/> instance containing the event data.</param>
		void Drop(DragEventArgs e);

		/// <summary>
		/// Gets the underlying object the <see cref="IDropTarget"/> represents.
		/// </summary>
		object Target
		{ 
			get;
		}
	}
}