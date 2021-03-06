/* ConsoleUtility.cs -- useful routines for console manipulation
   Ars Magna project, https://www.assembla.com/spaces/arsmagna */

#region Using directives

using System;
using System.Diagnostics;
using System.IO;
using System.Text;

#endregion

namespace AM
{
    /// <summary>
    /// Useful routines for text console manipulation.
    /// </summary>
    [Done]
    public static class ConsoleUtility
    {
        /// <summary>
        /// Redirects standard output stream to the file.
        /// </summary>
        /// <param name="fileName">Name of the file to use.</param>
        /// <param name="encoding"><see cref="Encoding"/>
        /// to use.</param>
        public static void RedirectStandardOutput
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

            StreamWriter stdOutput = new StreamWriter
                (
                fileName,
                false,
                encoding )
                                     {
                                         AutoFlush = true
                                     };
            Console.SetOut ( stdOutput );
        }

        /// <summary>
        /// Set the console output codepage.
        /// </summary>
        /// <param name="encoding"><see cref="Encoding"/>
        /// to use.</param>
        public static void SetOutputCodePage ( Encoding encoding )
        {
            ArgumentUtility.NotNull
                (
                 encoding,
                 "encoding" );

            StreamWriter stdOutput = new StreamWriter
                (
                Console.OpenStandardOutput (),
                encoding )
                                     {
                                         AutoFlush = true
                                     };
            Console.SetOut ( stdOutput );
            StreamWriter stdError = new StreamWriter
                (
                Console.OpenStandardError (),
                encoding )
                                    {
                                        AutoFlush = true
                                    };
            Console.SetError ( stdError );
        }

        /// <summary>
        /// Set the console output codepage.
        /// </summary>
        /// <param name="codePage">Codepage to use.</param>
        public static void SetOutputCodePage ( int codePage )
        {
            SetOutputCodePage ( Encoding.GetEncoding ( codePage ) );
        }

        /// <summary>
        /// Set the console output codepage.
        /// </summary>
        /// <param name="codePage">Codepage to use.</param>
        public static void SetOutputCodePage ( string codePage )
        {
            ArgumentUtility.NotNullOrEmpty
                (
                 codePage,
                 "codePage" );
            SetOutputCodePage ( Encoding.GetEncoding ( codePage ) );
        }

        /// <summary>
        /// Fix Visual Studio 2005 erroneous 
        /// console redirection handling:
        /// needed for non-latin characters only.
        /// </summary>
        public static void FixVisualStudio2005Bug ( )
        {
            if ( Debugger.IsAttached )
            {
                SetOutputCodePage ( Encoding.Default );
            }
        }
    }
}
