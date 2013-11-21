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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using iMeta.Windows.Primitives;

namespace iMeta.Windows.DragDrop
{
	/// <summary>
	/// A simple <see cref="DragCursor"/> that drags an element around the screen using 
	/// a <see cref="Transform"/>.
	/// </summary>
   public class ElementTransformDragCursor : DragCursor
	{
		#region Fields

		private readonly UIElement _dragElement;
		private Point _dragOffset;
		private readonly bool _useEffectVisualStates;

		#endregion

      public ElementTransformDragCursor(UIElement dragElement) : this(dragElement, false) {}
      
      public ElementTransformDragCursor(UIElement dragElement, bool enableVisualStates)
      {
         #region Validate Arguments

         Guard.ArgumentNull("dragElement", dragElement);

         #endregion

         _dragElement = dragElement;
			_useEffectVisualStates = enableVisualStates;
		}

      private bool IsEffectSet(DragDropEffects effect)
      {
         return ((Effects & effect) == effect);
      }

		protected override void OnBeginDrag(IMouseEventArgs args)
		{
			_dragOffset = args.GetPosition(_dragElement);
		}

		protected override void OnDrag(IMouseEventArgs args)
		{
			//	get the position based on the drag cursor
			var movePoint = args.GetPosition(_dragElement);

			//	get translate transform
			var translateTransform = _dragElement.RenderTransform as TranslateTransform ?? new TranslateTransform();

			//	adjust coordinates 
			movePoint = translateTransform.Transform(movePoint);
			movePoint.X -= _dragOffset.X;
			movePoint.Y -= _dragOffset.Y;

			//	update transform
			translateTransform.X = movePoint.X;
			translateTransform.Y = movePoint.Y;
			_dragElement.RenderTransform = translateTransform;
		}

		protected override void OnEndDrag()
		{
			_dragElement.RenderTransform = null;
		}
      
		protected override void OnEffectsChanged()
		{
         base.OnEffectsChanged();
         
         // use visual states?
         if (!_useEffectVisualStates)
            return;

         // drag element must be a control
         var control = DragElement as Control;
			if (control == null)
				return;

			var visualState = DragDrop.DragVisualStates.DragNone;
			if (Effects != DragDropEffects.None)
			{
				visualState = DragDrop.DragVisualStates.DragAll;
				if (_useEffectVisualStates)
				{
					//	set effects based on logical priority of effects
					if (IsEffectSet(DragDropEffects.Copy))
						visualState = DragDrop.DragVisualStates.DragCopy;
					else if (IsEffectSet(DragDropEffects.Move))
						visualState = DragDrop.DragVisualStates.DragMove;
					else if (IsEffectSet(DragDropEffects.Link))
						visualState = DragDrop.DragVisualStates.DragLink;
				}
			}
			VisualStateManager.GoToState(control, visualState, true);
		}

		public override IEnumerable<UIElement> DragVisualRoots
		{
			get
			{
				yield return DragElement;
			}
		}

		protected UIElement DragElement
		{
			get
			{
				return _dragElement;
			}
		}
		
		protected Point DragOffset
		{
			get
			{
				return _dragOffset;
			}
		}

		protected bool UseEffectVisualStates
		{
			get
			{
				return _useEffectVisualStates;
			}
		}
	}
}