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
	/// Indicates whether a control allows drop operations.
	/// </summary>
	public enum AllowDrop
	{
		/// <summary>
		/// Indicates the control inherits the value from its parent.
		/// </summary>
		Inherited,
		/// <summary>
		/// Indicates the control can't accept data that a user drags over it.
		/// </summary>
		False,
		/// <summary>
		/// Indicates the control can accept data that a user drags over it.
		/// </summary>
		True
	}
}