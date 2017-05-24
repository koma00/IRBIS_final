﻿/* CultureSaver.cs -- saves and restores current thread culture.
   Ars Magna project, https://www.assembla.com/spaces/arsmagna */

#region Using directives

using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;

#endregion

namespace AM.Globalization
{
    /// <summary>
    /// Saves and restores current thread culture.
    /// </summary>
    /// <example>
    /// <para>This example changes current thread culture to
    /// for a while.
    /// </para>
    /// <code>
    /// using System.Globalization;
    /// using AM.Globalization;
    /// 
    /// using ( new CultureSaver ( "ru-RU" ) )
    /// {
    ///		// do something
    /// }
    /// // here old culture is restored.
    /// </code>
    /// </example>
    public sealed class CultureSaver : IDisposable
    {
        #region Properties

        private CultureInfo _previousCulture;

        /// <summary>
        /// Gets the previous culture.
        /// </summary>
        /// <value>The previous culture.</value>
        public CultureInfo PreviousCulture
        {
            [DebuggerStepThrough]
            get
            {
                return _previousCulture;
            }
        }

        #endregion

        #region Construction

        /// <summary>
        /// Saves current thread culture for a while.
        /// </summary>
        public CultureSaver ( )
        {
            _previousCulture = Thread.CurrentThread.CurrentCulture;
        }

        /// <summary>
        /// Sets new current thread culture to the given 
        /// <see cref="T:System.Globalization.CultureInfo"/>.
        /// </summary>
        /// <param name="newCulture">The new culture.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="newCulture"/> is <c>null</c>.
        /// </exception>
        public CultureSaver ( CultureInfo newCulture )
            : this ()
        {
            ArgumentUtility.NotNull
                (
                 newCulture,
                 "newCulture" );
            Thread.CurrentThread.CurrentCulture = newCulture;
        }

        /// <summary>
        /// Sets current thread culture to based on the given name.
        /// </summary>
        /// <param name="cultureName">Name of the culture.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="cultureName"/> is <c>null</c>.
        /// </exception>
        public CultureSaver ( string cultureName )
            : this ( new CultureInfo ( cultureName ) )
        {
        }

        /// <summary>
        /// Sets current thread culture to based on the given identifier.
        /// </summary>
        /// <param name="cultureIdentifier">The culture identifier.</param>
        public CultureSaver ( int cultureIdentifier )
            : this ( new CultureInfo ( cultureIdentifier ) )
        {
        }

        #endregion

        #region IDisposable members

        /// <summary>
        /// Restores old current thread UI culture.
        /// </summary>
        public void Dispose ( )
        {
            Thread.CurrentThread.CurrentCulture = _previousCulture;
        }

        #endregion
    }
}
