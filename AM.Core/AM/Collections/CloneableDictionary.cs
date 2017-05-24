﻿/* CloneableDictionary.cs -- cloneable dictionary.
   Ars Magna project, https://www.assembla.com/spaces/arsmagna */

#region Using directives

using System;
using System.Collections.Generic;
using System.Diagnostics;

#endregion

namespace AM.Collections
{
    /// <summary>
    /// Cloneable dictionary.
    /// </summary>
    [Done]
    [Serializable]
    [DebuggerDisplay ( "Count={Count}" )]
    public class CloneableDictionary < TKey, TValue >
        : Dictionary < TKey, TValue >,
          ICloneable
    {
        #region ICloneable members

        ///<summary>
        /// Creates a new object that is a copy of the current instance.
        ///</summary>
        ///<returns>
        /// A new object that is a copy of this instance.
        ///</returns>
        public object Clone ( )
        {
            CloneableDictionary < TKey, TValue > result =
                new CloneableDictionary < TKey, TValue > ();
            Type keyType = typeof ( TKey );
            Type valueType = typeof ( TValue );
            bool cloneKeys = false;
            bool cloneValues = false;

            if ( !keyType.IsValueType )
            {
                if ( keyType.IsAssignableFrom ( typeof ( ICloneable ) ) )
                {
                    throw new ArgumentException ( keyType.Name );
                }
                cloneKeys = true;
            }
            if ( !valueType.IsValueType )
            {
                if ( valueType.IsAssignableFrom ( typeof ( ICloneable ) ) )
                {
                    throw new ArgumentException ( valueType.Name );
                }
                cloneValues = true;
            }

            foreach ( KeyValuePair < TKey, TValue > pair in this )
            {
                TKey keyCopy = pair.Key;
                TValue valueCopy = pair.Value;
                if ( cloneKeys )
                {
                    keyCopy = (TKey) ( (ICloneable) pair.Key ).Clone ();
                }
                if ( cloneValues )
                {
                    valueCopy = (TValue) ( (ICloneable) pair.Value ).Clone ();
                }
                result.Add
                    (
                     keyCopy,
                     valueCopy );
            }

            return result;
        }

        #endregion
    }
}
