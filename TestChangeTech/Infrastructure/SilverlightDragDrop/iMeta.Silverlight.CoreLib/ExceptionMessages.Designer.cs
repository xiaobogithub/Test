﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace iMeta {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ExceptionMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ExceptionMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("iMeta.ExceptionMessages", typeof(ExceptionMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Value must be above {0}..
        /// </summary>
        internal static string ArgumentAboveLowerBound {
            get {
                return ResourceManager.GetString("ArgumentAboveLowerBound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Value must be below {0}..
        /// </summary>
        internal static string ArgumentBelowUpperBound {
            get {
                return ResourceManager.GetString("ArgumentBelowUpperBound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Value cannot be empty..
        /// </summary>
        internal static string ArgumentEmpty {
            get {
                return ResourceManager.GetString("ArgumentEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Value contains illegal characters..
        /// </summary>
        internal static string ArgumentIllegalCharacters {
            get {
                return ResourceManager.GetString("ArgumentIllegalCharacters", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The specified value is not defined for enumeration type {0:s}..
        /// </summary>
        internal static string ArgumentInvalidEnum {
            get {
                return ResourceManager.GetString("ArgumentInvalidEnum", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Value must be of type {0}..
        /// </summary>
        internal static string ArgumentInvalidType {
            get {
                return ResourceManager.GetString("ArgumentInvalidType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Value must be assignable as type {0:s}..
        /// </summary>
        internal static string ArgumentTypeIsAssignableFrom {
            get {
                return ResourceManager.GetString("ArgumentTypeIsAssignableFrom", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Destination array was not long enough. Check the destination index and length, and the array&apos;s lower bounds..
        /// </summary>
        internal static string CollectionCopyToArrayInsufficientSpace {
            get {
                return ResourceManager.GetString("CollectionCopyToArrayInsufficientSpace", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Only single dimension arrays are supported here..
        /// </summary>
        internal static string CollectionCopyToRankInvalid {
            get {
                return ResourceManager.GetString("CollectionCopyToRankInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Collection was modified; enumeration operation may not execute..
        /// </summary>
        internal static string EnumeratorModified {
            get {
                return ResourceManager.GetString("EnumeratorModified", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The string {0} could not be parsed into a valid {1}..
        /// </summary>
        internal static string InvalidString {
            get {
                return ResourceManager.GetString("InvalidString", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Index was out of range. Must be non-negative and less than the size of the collection..
        /// </summary>
        internal static string ListIndexOutOfRange {
            get {
                return ResourceManager.GetString("ListIndexOutOfRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Index must be within the bounds of the list..
        /// </summary>
        internal static string ListInsertIndexOutOfRange {
            get {
                return ResourceManager.GetString("ListInsertIndexOutOfRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Type {0:s} could not be loaded..
        /// </summary>
        internal static string TypeLoadError {
            get {
                return ResourceManager.GetString("TypeLoadError", resourceCulture);
            }
        }
    }
}