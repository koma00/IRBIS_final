/* DateTimeInterval.cs -- 
   Ars Magna project, https://www.assembla.com/spaces/arsmagna */

#region Using directives

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;

#endregion

namespace AM
{
    /// <summary>
    /// 
    /// </summary>
    public class DateTimeInterval
    {
        #region Properties

        private DateTime _beginning;

        /// <summary>
        /// Gets or sets beginning of the <see cref="DateTimeInterval"/>.
        /// </summary>
        public DateTime Beginning
        {
            [DebuggerStepThrough]
            get
            {
                return _beginning;
            }
            [DebuggerStepThrough]
            set
            {
                _beginning = value;
            }
        }

        /// <summary>
        /// Gets or sets duration of the <see cref="DateTimeInterval"/>.
        /// </summary>
        /// <value>The duration.</value>
        [Browsable ( false )]
        public TimeSpan Duration
        {
            get
            {
                return ( Ending - Beginning );
            }
            set
            {
                Ending = Beginning + value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is empty.
        /// </summary>
        /// <value><c>true</c> if this instance is empty;
        /// otherwise, <c>false</c>.</value>
        public bool IsEmpty
        {
            get
            {
                return ( Beginning < Ending );
            }
        }

        private DateTime _ending;

        /// <summary>
        /// Gets or sets ending of the <see cref="DateTimeInterval"/>.
        /// </summary>
        public DateTime Ending
        {
            [DebuggerStepThrough]
            get
            {
                return _ending;
            }
            [DebuggerStepThrough]
            set
            {
                _ending = value;
            }
        }

        #endregion

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeInterval"/> class.
        /// </summary>
        public DateTimeInterval ( )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeInterval"/> class.
        /// </summary>
        /// <param name="beginning">Beginning of the interval.</param>
        /// <param name="ending">Ending of the interval.</param>
        public DateTimeInterval
            (
            DateTime beginning,
            DateTime ending )
        {
            _beginning = beginning;
            _ending = ending;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeInterval"/> class.
        /// </summary>
        /// <param name="beginning">Beginning of the interval.</param>
        /// <param name="duration">Duration of the interval.</param>
        public DateTimeInterval
            (
            DateTime beginning,
            TimeSpan duration )
        {
            _beginning = beginning;
            Duration = duration;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Determines whether the <see cref="DateTimeInterval"/>
        /// contains the specified moment.
        /// </summary>
        /// <param name="moment">The moment of time.</param>
        /// <returns>
        /// 	<c>true</c> if the <see cref="DateTimeInterval"/> contains
        /// the specified moment; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains ( DateTime moment )
        {
            return ( ( Beginning >= moment ) && ( Ending <= moment ) );
        }

        /// <summary>
        /// Determines whether the <see cref="DateTimeInterval"/>
        /// contains the specified interval.
        /// </summary>
        /// <param name="interval">Interval to check.</param>
        /// <returns>
        /// 	<c>true</c> if the <see cref="DateTimeInterval"/>contains
        /// the specified interval; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains ( DateTimeInterval interval )
        {
            return ( Contains ( interval.Beginning )
                     && Contains ( interval.Ending ) );
        }

        /// <summary>
        /// Extends the specified date time.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        public void Extend ( DateTime dateTime )
        {
            if ( dateTime < Beginning )
            {
                Beginning = dateTime;
            }
            if ( dateTime > Ending )
            {
                Ending = dateTime;
            }
        }

        /// <summary>
        /// Intersections the specified interval.
        /// </summary>
        /// <param name="interval">The interval.</param>
        /// <returns></returns>
        public DateTimeInterval Intersection ( DateTimeInterval interval )
        {
            return new DateTimeInterval
                (
                DateTimeUtility.Max
                    (
                     Beginning,
                     interval.Beginning ),
                DateTimeUtility.Min
                    (
                     Ending,
                     interval.Ending ) );
        }

        /// <summary>
        /// Determines whether the <see cref="DateTimeInterval"/>
        /// overlaps with the specified interval.
        /// </summary>
        /// <param name="interval">Interval to check.</param>
        /// <returns><c>true</c> if two intervals overlaps;
        /// otherwise, <c>false</c>.</returns>
        public bool Overlaps ( DateTimeInterval interval )
        {
            return ( Contains ( interval.Beginning )
                     || Contains ( interval.Ending ) );
        }

        /// <summary>
        /// Parses the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>Parsed <see cref="DateTimeInterval"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="text"/> parameter is <c>null</c>.
        /// </exception>
        public static DateTimeInterval Parse
            (
            string text,
            CultureInfo culture )
        {
            ArgumentUtility.NotNullOrEmpty
                (
                 text,
                 "text" );
            int index = text.IndexOf ( " - " );
            string beginningString = text.Substring
                (
                 0,
                 index );
            DateTime beginningDate = DateTime.Parse
                (
                 beginningString,
                 culture );
            string endingString = text.Substring ( index + 3 );
            DateTime endingDate = DateTime.Parse
                (
                 endingString,
                 culture );
            return new DateTimeInterval
                (
                beginningDate,
                endingDate );
        }

        /// <summary>
        /// Unions the specified interval.
        /// </summary>
        /// <param name="interval">The interval.</param>
        /// <returns></returns>
        public DateTimeInterval Union ( DateTimeInterval interval )
        {
            return new DateTimeInterval
                (
                DateTimeUtility.Min
                    (
                     Beginning,
                     interval.Beginning ),
                DateTimeUtility.Max
                    (
                     Ending,
                     interval.Ending ) );
        }

        #endregion

        #region Object members

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"></see> is equal to the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object"></see> to compare with the current <see cref="T:System.Object"></see>.</param>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"></see> is equal to the current <see cref="T:System.Object"></see>; otherwise, false.
        /// </returns>
        public override bool Equals ( object obj )
        {
            if ( this == obj )
            {
                return true;
            }
            DateTimeInterval dateTimeInterval = obj as DateTimeInterval;
            if ( dateTimeInterval == null )
            {
                return false;
            }
            return Equals
                       (
                        _beginning,
                        dateTimeInterval._beginning ) && Equals
                                                             (
                                                              _ending,
                                                              dateTimeInterval
                                                                  ._ending );
        }

        /// <summary>
        /// Serves as a hash function for a particular type. <see cref="M:System.Object.GetHashCode"></see> is suitable for use in hashing algorithms and data structures like a hash table.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"></see>.
        /// </returns>
        public override int GetHashCode ( )
        {
            return _beginning.GetHashCode () + 29*_ending.GetHashCode ();
        }

        ///<summary>
        /// Returns a <see cref="T:System.String"/> that represents 
        /// the current <see cref="T:System.Object"/>.
        ///</summary>
        ///<returns>
        /// A <see cref="T:System.String"/> that represents the current 
        /// <see cref="T:System.Object"/>.
        ///</returns>
        public override string ToString ( )
        {
            return string.Format
                (
                 "{0} - {1}",
                 Beginning,
                 Ending );
        }

        #endregion
    }
}
