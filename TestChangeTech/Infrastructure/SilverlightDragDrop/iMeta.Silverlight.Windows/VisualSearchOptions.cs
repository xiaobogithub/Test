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

namespace iMeta.Windows
{
   [Flags]
   public enum VisualSearchOptions
   {
      None = 0,
      /// <summary>
      /// Continue to search deeped within matches.
      /// </summary>
      WithinMatched = 1,
      /// <summary>
      /// All matches are expected to be at the same depth, the search will not go deeper than the first match.
      /// </summary>
      MatchDepth = 2
   }
}