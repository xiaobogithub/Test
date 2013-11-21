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
using System.Windows.Threading;

namespace iMeta.Windows
{
    public class ApplicationDispatcher
    {
        private static IApplicationDispatcher _current;

        public static IApplicationDispatcher Current
        {
            get
            {
                if (_current == null)
                    _current = GetDefaultApplicationDispatcher();
                return _current;
            }
            internal set { _current = value; }
        }

        private static IApplicationDispatcher GetDefaultApplicationDispatcher()
        {
            if (Deployment.Current.Dispatcher != null)
            {
                var dispatcher = Deployment.Current.Dispatcher;
                if (dispatcher != null)
                    return new DispatcherAdapter(dispatcher);
            }
        		return null;
        }

        #region Nested type: DispatcherAdapter

        /// <summary>
        /// Adapts a <see cref="Dispatcher"/> to an IApplicationDispatcher
        /// </summary>
        private class DispatcherAdapter : IApplicationDispatcher
        {
            private readonly Dispatcher _dispatcher;

            public DispatcherAdapter(Dispatcher dispatcher)
            {
                _dispatcher = dispatcher;
            }

            #region IApplicationDispatcher Members

            public DispatcherOperation BeginInvoke(Action a)
            {
                return _dispatcher.BeginInvoke(a);
            }

            public DispatcherOperation BeginInvoke(Delegate d, params object[] args)
            {
                return _dispatcher.BeginInvoke(d, args);
            }

           public bool CheckAccess()
           {
              return _dispatcher.CheckAccess();
           }

           #endregion
        }

        #endregion
    }
}