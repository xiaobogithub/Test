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
using iMeta.Windows.Primitives;

namespace iMeta.Windows.DragDrop
{
	public class DragEventArgs : RoutedEventArgs, IMouseEventArgs
	{
		#region Fields

		private readonly IMouseEventArgs _fromArgs;
		private readonly object _source;
		private bool _handled;
		private readonly object _data;
		private readonly DragDropEffects _allowedEffects;
		private DragDropEffects _effects;
      
		#endregion

		internal DragEventArgs(IMouseEventArgs fromArgs, object source, object data, DragDropEffects allowedEffects)
		{
			_fromArgs = fromArgs;
			_source = source;
			_data = data;
			_effects = allowedEffects;
			_allowedEffects = allowedEffects;
		}

      /// <summary>
      /// Gets the source of the event
      /// </summary>
      public object Source
      {
         get
         {
            return _source;
         }
      }
		
		/// <summary>
		/// Gets the allowed <see cref="DragDropEffects"/> as specified by the originator of the drag event.
		/// </summary>
      public DragDropEffects AllowedEffects
		{
			get
			{
				return _allowedEffects;
			}
		}

      /// <summary>
      /// Gets or sets the <see cref="DragDropEffects"/> supported by the current drop target.
      /// </summary>
		public DragDropEffects Effects
		{
			get
			{
				return _effects;
			}
			set
			{
				_effects = value;
			}
		}
		
		/// <summary>
      /// Gets the data associated with the drag-and-drop operation.
		/// </summary>
      public object Data
		{
			get { return _data; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether the event has been handled.
		/// </summary>
		/// <remarks>
		/// Setting <see cref="Handled"/> to <c>true</c> will prevent the event 
		/// from being bubbled up any further within the visual tree.
		/// </remarks>
		public bool Handled
		{
			get
			{
				return _handled;
			}
			set
			{
				_handled = value;
			}
		}

		/// <summary>
		/// Gets the current position of the mouse relative to a specified <see cref="UIElement"/>.
		/// </summary>
		/// <param name="element">An <see cref="UIElement"/> from which to get a relative position.</param>
		/// <returns>A point that is relativie to the element specified in <paramref name="element"/></returns>
      public Point GetPosition(UIElement element)
		{
			return _fromArgs.GetPosition(element);
		}
	}
}