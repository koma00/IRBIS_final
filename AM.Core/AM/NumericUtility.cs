/* NumericUtility.cs -- bunch of useful number manipulation routines.
   Ars Magna project, https://www.assembla.com/spaces/arsmagna */

#region Using directives

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

#endregion

namespace AM
{
    /// <summary>
    /// Bunch of useful number manipulation routines
    /// </summary>
    public static class NumericUtility
    {
        /// <summary>
        /// Safely parses 32-bit integer.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Parsed or default value.</returns>
        public static int SafeParseInt32
            (
            this string text,
            int defaultValue )
        {
            int result;
            if ( !Int32.TryParse
                      (
                       text,
                       out result ) )
            {
                result = defaultValue;
            }
            return result;
        }

        /// <summary>
        /// Safely parses 32-bit integer.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <returns>Parsed value or 0.</returns>
        public static int SafeParseInt32 ( this string text )
        {
            return SafeParseInt32
                (
                 text,
                 0 );
        }

        /// <summary>
        /// Safely parses 64-bit integer.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Parsed or default value.</returns>
        public static long SafeParseInt64
            (
            this string text,
            long defaultValue )
        {
            long result;
            if ( !Int64.TryParse
                      (
                       text,
                       out result ) )
            {
                result = defaultValue;
            }
            return result;
        }

        /// <summary>
        /// Safely parses 64-bit integer.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <returns>Parsed value or 0.</returns>
        public static long SafeParseInt64 ( this string text )
        {
            return SafeParseInt64
                (
                 text,
                 0 );
        }

        /// <summary>
        /// Converts 32-bit integer string using 
        /// <see cref="CultureInfo.InvariantCulture"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>String representation of the value.</returns>
        public static string ToInvariantString ( this int value )
        {
            return value.ToString ( CultureInfo.InvariantCulture );
        }

        /// <summary>
        /// Converts 32-bit integer string using 
        /// <see cref="CultureInfo.InvariantCulture"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>String representation of the value.</returns>
        public static string ToInvariantString ( this long value )
        {
            return value.ToString ( CultureInfo.InvariantCulture );
        }

        /// <summary>
        /// Formats the range.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="last">The last.</param>
        /// <returns></returns>
        public static string FormatRange
            (
            int first,
            int last )
        {
            if ( first == last )
            {
                return first.ToInvariantString ();
            }
            if ( first == ( last - 1 ) )
            {
                return ( first.ToInvariantString () + ", "
                         + last.ToInvariantString () );
            }
            return ( first.ToInvariantString () + "-"
                     + last.ToInvariantString () );
        }

        /// <summary>
        /// Compresses the range.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <returns></returns>
        public static string CompressRange ( IEnumerable < int > n )
        {
            // ReSharper disable PossibleMultipleEnumeration
            if ( !n.Any () )
            {
                return String.Empty;
            }

            var result = new StringBuilder ();
            var first = true;
            var prev = n.First ();
            var last = prev;
            foreach ( var i in n.Skip ( 1 ) )
            {
                if ( i != ( last + 1 ) )
                {
                    result.AppendFormat
                        (
                         "{0}{1}",
                         ( first
                               ? ""
                               : ", " ),
                         FormatRange
                             (
                              prev,
                              last ) );
                    prev = i;
                    first = false;
                }
                last = i;
            }
            result.AppendFormat
                (
                 "{0}{1}",
                 ( first
                       ? ""
                       : ", " ),
                 FormatRange
                     (
                      prev,
                      last ) );

            return result.ToString ();
            // ReSharper restore PossibleMultipleEnumeration
        }
    }
}
