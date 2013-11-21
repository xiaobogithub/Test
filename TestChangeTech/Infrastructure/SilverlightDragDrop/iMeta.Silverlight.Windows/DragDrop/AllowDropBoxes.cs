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
	/// Internal classes used to represent boxed values of type <see cref="AllowDrop"/>
	/// </summary>
	internal class AllowDropBoxes
	{
		#region Fields

		internal static readonly object Inherited = AllowDrop.Inherited;
		internal static readonly object False = AllowDrop.False;
		internal static readonly object True = AllowDrop.True;    

		#endregion

		internal static object Box(AllowDrop value)
		{
			return value == AllowDrop.Inherited ? Inherited : (value == AllowDrop.True ? True : False);
		}  
	}
}