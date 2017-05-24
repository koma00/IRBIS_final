﻿/* VersionStream.cs -- versioning stream.
   Ars Magna project, https://www.assembla.com/spaces/arsmagna */

#region Using directives

using System.Diagnostics;
using System.IO;

#endregion

namespace AM.IO
{
    /// <summary>
    /// Versioning <see cref="Stream"/>.
    /// </summary>
    /// <seealso cref="NotifyStream"/>
    public class VersionStream : NotifyStream
    {
        #region Properties

        private int _version;

        /// <summary>
        /// Number of stream content modifications.
        /// </summary>
        public virtual int Version
        {
            [DebuggerStepThrough]
            get
            {
                return _version;
            }
        }

        #endregion

        #region Construction

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="baseStream"></param>
        public VersionStream ( Stream baseStream )
            : base ( baseStream )
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Set version count to zero.
        /// </summary>
        public virtual void ResetVersion ( )
        {
            _version = 0;
        }

        #endregion

        #region NotifyStream members

        /// <summary>
        /// Called when stream content is changed.
        /// </summary>
        protected override void OnStreamChanged ( )
        {
            _version++;
            base.OnStreamChanged ();
        }

        #endregion
    }
}
