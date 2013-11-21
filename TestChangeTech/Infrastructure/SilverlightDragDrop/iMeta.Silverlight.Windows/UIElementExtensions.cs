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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace iMeta.Windows
{
    public class Adapter 
    {
        internal static readonly DependencyProperty AdapterProperty = DependencyProperty.RegisterAttached(
             "Adapter", typeof(object), typeof(Adapter), new PropertyMetadata(null));

        internal static readonly object NullAdapter = new object();

        internal static object GetOrCreateAdapter(UIElement element)
        {
            object value = element.GetValue(AdapterProperty);
            if (value == null)
            {
                value = CreateAdapter(element);
                element.SetValue(AdapterProperty, value);
            }
            return value;
        }

        internal static object CreateAdapter(UIElement element)
        {
            //	panel
            var panel = element as Panel;
            if (panel != null)
                return new PanelAdapter(panel);

            //	content control
            var contentControl = element as ContentControl;
            if (contentControl != null)
                return new ContentControlAdapter(contentControl);

            //	content presenter
            var contentPresenter = element as ContentPresenter;
            if (contentPresenter != null)
                return new ContentPresenterAdapter(contentPresenter);

            //	null
            return NullAdapter;
        }


        #region Nested type: ContentControlAdapter

        private class ContentControlAdapter : IUIElementContainer
        {
            private readonly ContentControl _adaptee;

            public ContentControlAdapter(ContentControl adaptee)
            {
                _adaptee = adaptee;
            }

            #region IUIElementContainer Members

            bool IUIElementContainer.Contains(UIElement element)
            {
                return ReferenceEquals(_adaptee.Content, element);
            }

            bool IUIElementContainer.IsSingleChildContainer
            {
                get
                {
                    return true;
                }
            }

            bool IUIElementContainer.Remove(UIElement element)
            {
                bool result = ReferenceEquals(_adaptee.Content, element);
                if (result)
                    _adaptee.Content = null;
                return result;
            }

            void IUIElementContainer.Add(UIElement element)
            {
                if (_adaptee.Content != null)
                    throw new InvalidOperationException("");
                _adaptee.Content = element;
            }

            #endregion
        }

        #endregion

        #region Nested type: ContentPresenterAdapter

        private class ContentPresenterAdapter : IUIElementContainer
        {
            private readonly ContentPresenter _adaptee;

            public ContentPresenterAdapter(ContentPresenter adaptee)
            {
                _adaptee = adaptee;
            }

            #region IUIElementContainer Members

            bool IUIElementContainer.Contains(UIElement element)
            {
                return ReferenceEquals(_adaptee.Content, element);
            }

            bool IUIElementContainer.IsSingleChildContainer
            {
                get
                {
                    return true;
                }
            }

            bool IUIElementContainer.Remove(UIElement element)
            {
                bool result = ReferenceEquals(_adaptee.Content, element);
                if (result)
                    _adaptee.Content = null;
                return result;
            }

            void IUIElementContainer.Add(UIElement element)
            {
                if (_adaptee.Content != null)
                    throw new InvalidOperationException("");
                _adaptee.Content = element;
            }

            #endregion
        }

        #endregion

        #region Nested type: PanelAdapter

        private class PanelAdapter : IUIElementContainer
        {
            private readonly Panel _adaptee;

            internal PanelAdapter(Panel container)
            {
                _adaptee = container;
            }

            #region IUIElementContainer Members

            bool IUIElementContainer.Contains(UIElement element)
            {
                return _adaptee.Children.Contains(element);
            }

            bool IUIElementContainer.Remove(UIElement element)
            {
                return _adaptee.Children.Remove(element);
            }

            void IUIElementContainer.Add(UIElement element)
            {
                _adaptee.Children.Add(element);
            }

            bool IUIElementContainer.IsSingleChildContainer
            {
                get
                {
                    return false;
                }
            }

            #endregion
        }

        #endregion
    }

	public static class UIElementExtensions
	{
		  

		public static IUIElementContainer AsElementContainer(this UIElement element)
		{
			return Adapter.GetOrCreateAdapter(element) as IUIElementContainer;
		}

		public static IUIElementContainer GetVisualElementContainer(this UIElement element)
		{
			var parent = VisualTreeHelper.GetParent(element) as UIElement;
			if (parent != null)
				return parent.AsElementContainer();
			return null;
		}

	}
}