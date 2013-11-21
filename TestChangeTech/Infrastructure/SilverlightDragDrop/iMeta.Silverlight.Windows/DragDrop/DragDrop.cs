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
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using iMeta.Windows.Primitives;
using System.Diagnostics;

namespace iMeta.Windows.DragDrop
{
   /// <summary>
   /// Provides support for drag-and-drop operations, including support for initiating drag-and-drop
   /// operations and adding and removing drag-and-drop related event handlers.
   /// </summary>
	public class DragDrop
	{	
		#region Fields
		
		public static readonly DependencyProperty AllowDropProperty = DependencyProperty.RegisterAttached(
			"AllowDrop", typeof (AllowDrop), typeof (DragDrop), new PropertyMetadata(AllowDrop.Inherited));
      
		private UIElement _currentDropTarget;
		private readonly DragCursor _dragCursor;
      private readonly DependencyObject _dragSource;
      private readonly object _data;
		private readonly DragDropEffects _allowedEffects;
		private static DragDrop _current;
		private IMouseEventArgs _currentMouseArgs;
		private UIElement _rootVisual;
		private readonly DragOperation _dragOperation;

		#endregion

		private DragDrop(DependencyObject dragSource, object data, DragDropEffects allowedEffects, DragCursor dragCursor)
		{
		   _dragSource = dragSource;
		   _data = data;
			_allowedEffects = allowedEffects;
			_dragCursor = dragCursor;
			
			//	create drag operation and bind to events
			_dragOperation = new DragOperation();
			_dragOperation.DragStarted += OnDragStarted;
			_dragOperation.DragDelta += OnDragDelta;
			_dragOperation.DragCompleted += OnDragCompleted;
		}

		#region Add/Remove Event Handler Methods

		public static void AddDragOverHandler(DependencyObject element, DragEventHandler handler)
		{
			if (handler != null)
				DragDropBehaviour.GetOrCreateBehaviour(element).DragOver += handler;
		}

		public static void AddDragEnterHandler(DependencyObject element, DragEventHandler handler)
		{
			if (handler != null)
				DragDropBehaviour.GetOrCreateBehaviour(element).DragEnter += handler;
		}

		public static void AddDragLeaveHandler(DependencyObject element, DragEventHandler handler)
		{
			if (handler != null)
				DragDropBehaviour.GetOrCreateBehaviour(element).DragLeave += handler;
		}

		public static void AddDropHandler(DependencyObject element, DragEventHandler handler)
		{
			if (handler != null)
				DragDropBehaviour.GetOrCreateBehaviour(element).Drop += handler;
		}

		public static void RemoveDragOverHandler(DependencyObject element, DragEventHandler handler)
		{
			if (handler != null)
			{
				var adapter = DragDropBehaviour.GetBehaviour(element);
				if (adapter != null)
					adapter.DragOver -= handler;
			}
		}

		public static void RemoveDragEnterHandler(DependencyObject element, DragEventHandler handler)
		{
			if (handler != null)
			{
				var adapter = DragDropBehaviour.GetBehaviour(element);
				if (adapter != null)
					adapter.DragEnter -= handler;
			}
		}

		public static void RemoveDragLeaveHandler(DependencyObject element, DragEventHandler handler)
		{
			if (handler != null)
			{
				var adapter = DragDropBehaviour.GetBehaviour(element);
				if (adapter != null)
					adapter.DragLeave -= handler;
			}
		}

		public static void RemoveDropHandler(DependencyObject element, DragEventHandler handler)
		{
			if (handler != null)
			{
				var adapter = DragDropBehaviour.GetBehaviour(element);
				if (adapter != null)
					adapter.Drop -= handler;
			}
		}

		#endregion

		#region Dependency Property Setters/Getters

		public static void SetAllowDrop(DependencyObject dependencyObject, AllowDrop value)
		{
			dependencyObject.SetValue(AllowDropProperty, AllowDropBoxes.Box(value));
		}

		public static AllowDrop GetAllowDrop(DependencyObject dependencyObject)
		{
			return (AllowDrop)dependencyObject.GetValue(AllowDropProperty);
		}

		#endregion

		#region Visual States

		public static class DragVisualStates
		{
			public const string DragAll = "Drag All";
			public const string DragNone = "Drag None";
			public const string DragCopy = "Drag Copy";
			public const string DragMove = "Drag Move";
			public const string DragLink = "Drag Link";
		}

		public static class DropVisualStates
		{
			public const string DropTarget = "Drop Target";
			public const string DropNormal = "Drop Normal";
		}

		#endregion

		private UIElement GetDropTargetAt(Point intersectionPoint)
		{
			var elements = VisualTreeHelper.FindElementsInHostCoordinates(intersectionPoint, _rootVisual);

			//	exclude drop targets that form part of the cursor
			if (_dragCursor.DragVisualRoots != null)
			{
				foreach (var dragElement in _dragCursor.DragVisualRoots)
					elements = elements.Except(VisualTreeHelper.FindElementsInHostCoordinates(intersectionPoint, dragElement));
			}

			//	retrieve top most target
			var element = elements.FirstOrDefault();
			if (element != null)
			{
				if (AllowsDrop(element))
					return element;
			}
			return null;
		}

		private static bool AllowsDrop(DependencyObject element)
		{
			while (element != null)
			{
				var result = GetAllowDrop(element);
				if (result != AllowDrop.Inherited)
				{
					return result == AllowDrop.True;
				}
				element = VisualTreeHelper.GetParent(element);
			}
			return false;
		}
       
		private void UpdateCurrentDropTarget()
		{
			var value = GetDropTargetAt(_currentMouseArgs.GetPosition(null));
			if (_currentDropTarget != value)
			{
				if (_currentDropTarget != null)
					OnDragLeave();
			   _currentDropTarget = value;
			   if (_currentDropTarget != null)
			      OnDragEnter();
			}
			if (_currentDropTarget != null)
         {
            OnDragOver();
         }
         else
         {
            _dragCursor.Effects = DragDropEffects.None;
         }
		}

		private void BubbleDragEvent(DragEventArgs args, Action<IDropTarget, DragEventArgs> handler)
		{
			var source = _currentDropTarget;
			while (source != null && GetAllowDrop(source) != AllowDrop.False)
			{
				var dropTarget = source.GetDropTarget();
				if (dropTarget != null)
				{
					handler(dropTarget, args);
					if (args.Handled)
						return;
				}
				source = VisualTreeHelper.GetParent(source) as UIElement;
			}
		}

		private DragEventArgs BubbleDragEvent(Action<IDropTarget, DragEventArgs> handler)
		{
			//	create event args
			var args = new DragEventArgs(_currentMouseArgs, _currentDropTarget, 
				_data, _allowedEffects);		
			
			//	bubble event
			BubbleDragEvent(args, handler);

			//	return
			return args;
		}

		private void UpdateEffects(DragDropEffects effects)
		{
			_dragCursor.Effects = effects & _allowedEffects;	
		}
      
		private void OnDragEnter()
		{
			var args = BubbleDragEvent((x, y) => x.DragEnter(y));
			UpdateEffects(args.Effects);
		} 
  
		private void OnDragLeave()
		{
			BubbleDragEvent((x, y) => x.DragLeave(y));
			UpdateEffects(DragDropEffects.None);
		}

		private void OnDragOver()
		{
			var args = BubbleDragEvent((x, y) => x.DragOver(y));
			UpdateEffects(args.Effects);
		}

		private void OnDrop()
		{
			BubbleDragEvent((x, y) => x.Drop(y));
		}
      
		private bool UpdateRootVisual()
		{
			_rootVisual = Application.Current != null ? Application.Current.RootVisual : null;
			if (_rootVisual == null)
			{
				Debug.WriteLine("Cannot perform drag drop operations without a root visual.");
				return false;
			}
			return true;
		}

		#region DragOperation Event Handlers

		private void OnDragStarted(object sender, DragOperationStartedEventArgs e)
		{
			//	notify drag cursor
			_dragCursor.BeginDrag(e);
			Dragging(e);
		}

		private void OnDragCompleted(object sender, DragOperationCompletedEventArgs e)
		{
			if (!e.Canceled)
				Dragging(e);
			EndDrag(!e.Canceled);
		}

		private void OnDragDelta(object sender, DragOperationDeltaEventArgs e)
		{
			Dragging(e);
		}

		#endregion

		private bool BeginDrag(MouseEventArgs startArgs) // todo: should be IMouseEventArgs?
		{
			//	must have a root visual for calls to VisualTreeHelper.FindElementsInHostCoordinates
			if (!UpdateRootVisual())
				return false;

			//	start dragging
			return _dragOperation.Start(startArgs);
		}
		
		private void Dragging(IMouseEventArgs e)
		{
			_currentMouseArgs = e;
			UpdateCurrentDropTarget();
			_dragCursor.Drag(e);
		}
      
		private void EndDrag(bool drop)
		{
			try
			{
				//	notify the drag cursor
				_dragCursor.EndDrag();

				//	drop or cancel?
				if (drop)
				{
					if (_currentDropTarget != null)
						OnDrop();
				}
				else if (_currentDropTarget != null)
				{
					OnDragLeave();
				}
			}
			finally
			{
				_current = null;
				_rootVisual = null;
			}
		}

		private static void VerifyAccess()
		{
			if (!ApplicationDispatcher.Current.CheckAccess())
				throw new InvalidOperationException("Wrong thread."); // todo: externalise
		}

		/// <summary>
		/// Updates the state of the current drag drop operation.
		/// </summary>
		public static void Update()
		{
			VerifyAccess();
			if (_current != null)
			   _current.UpdateCurrentDropTarget();
		}

		public static bool DoDragDrop(MouseEventArgs fromArgs, DependencyObject dragSource, object data, DragCursor dragCursor, DragDropEffects allowedEffects)
		{
			#region Validate Arguments

			Guard.ArgumentNull("fromArgs", fromArgs);
         
			#endregion

			//	verify call has been made on UI thread.
			VerifyAccess();
			
			//	cancel current (if any)
			Cancel();
			
			//	begin!!
			var dragDrop = new DragDrop(dragSource, data, allowedEffects, dragCursor);
			if (dragDrop.BeginDrag(fromArgs))
			{
				_current = dragDrop;
				return true;
			}
			return false;
		}

		/// <summary>
		/// Gets a value indicating whether a drag operation is currently in progress.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if a drag operation is in progress; otherwise, <c>false</c>.
		/// </value>
		public static bool IsDragging
		{
			get
			{
				return _current != null;
			}
		}

		/// <summary>
		/// Cancels the current drag operation
		/// </summary>
		public static void Cancel()
		{
			VerifyAccess();
			if (_current != null)
				_current._dragOperation.Cancel();
		}
	}
}