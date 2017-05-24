﻿/* StringDictionary.cs -- simple string-string dictionary
   Ars Magna project, https://www.assembla.com/spaces/arsmagna */

#region Using directives

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

#endregion

namespace AM.Collections
{
    /// <summary>
    /// Simple "string-string" <see cref="Dictionary{T1,T2}"/>
    /// with saving-restoring facility.
    /// </summary>
    public class StringDictionary : Dictionary < string, string >
    {
        #region Constants

        /// <summary>
        /// End-of-Dictionary mark.
        /// </summary>
        public const string EndOfDictionary = "*****";

        #endregion

        #region Public methods

        /// <summary>
        /// Loads <see cref="StringDictionary"/> from 
        /// the specified <see cref="StreamReader"/>.
        /// </summary>
        /// <param name="reader">Stream reader to load from.</param>
        /// <returns>Loaded <see cref="StringDictionary"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="reader"/> is <c>null</c>.
        /// </exception>
        public static StringDictionary Load ( TextReader reader )
        {
            ArgumentUtility.NotNull
                (
                 reader,
                 "reader" );
            StringDictionary result = new StringDictionary ();
            while ( true )
            {
                string key;
                string value;
                if ( ( ( key = reader.ReadLine () ) == null )
                     || key.StartsWith ( EndOfDictionary )
                     || ( ( value = reader.ReadLine () ) == null ) )
                {
                    break;
                }
                result.Add
                    (
                     key,
                     value );
            }
            return result;
        }

        /// <summary>
        /// Loads <see cref="StringDictionary"/> from the specified file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="encoding">File encoding.</param>
        /// <returns>Loaded <see cref="StringDictionary"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="fileName"/> is <c>null</c> or empty
        /// - or - <paramref name="encoding"/> is <c>null</c>.
        /// </exception>
        public static StringDictionary Load
            (
            string fileName,
            Encoding encoding )
        {
            ArgumentUtility.NotNullOrEmpty
                (
                 fileName,
                 "fileName" );
            ArgumentUtility.NotNull
                (
                 encoding,
                 "encoding" );
            using ( TextReader reader = new StreamReader
                (
                fileName,
                encoding ) )
            {
                return Load ( reader );
            }
        }

        /// <summary>
        /// Saves the <see cref="StringDictionary"/> with specified writer.
        /// </summary>
        /// <param name="writer">Writer to use during saving.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="writer"/> is <c>null</c>.
        /// </exception>
        public void Save ( TextWriter writer )
        {
            ArgumentUtility.NotNull
                (
                 writer,
                 "writer" );
            foreach ( KeyValuePair < string, string > pair in this )
            {
                writer.WriteLine ( pair.Key );
                writer.WriteLine ( pair.Value );
            }
            writer.WriteLine ( EndOfDictionary );
        }

        /// <summary>
        /// Saves the <see cref="StringDictionary"/> to specified file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="encoding">File encoding.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="fileName"/> is <c>null</c>
        /// - or - <paramref name="encoding"/> is <c>null</c>.
        /// </exception>
        public void Save
            (
            string fileName,
            Encoding encoding )
        {
            ArgumentUtility.NotNullOrEmpty
                (
                 fileName,
                 "fileName" );
            ArgumentUtility.NotNull
                (
                 encoding,
                 "encoding" );
            using ( TextWriter writer = new StreamWriter
                (
                fileName,
                false,
                encoding ) )
            {
                Save ( writer );
            }
        }

        #endregion
    }
}
