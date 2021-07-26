using System;
using System.Runtime.Serialization;

namespace TFT_Friendly.Back.Exceptions
{
    /// <summary>
    /// SetNotFoundException class
    /// </summary>
    [Serializable]
    public class SetNotFoundException : Exception
    {
        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="SetNotFoundException"/> class
        /// </summary>
        public SetNotFoundException() { }
        
        /// <summary>
        /// Initialize a new <see cref="SetNotFoundException"/> class
        /// </summary>
        /// <param name="message">The message of the exception</param>
        public SetNotFoundException(string message) : base(message) { }
        
        /// <summary>
        /// Initialize a new <see cref="SetNotFoundException"/> class
        /// </summary>
        /// <param name="message">The message of the exception</param>
        /// <param name="innerException">The inner exception of the exception</param>
        public SetNotFoundException(string message, Exception innerException) : base(message, innerException) { }
        
        /// <summary>
        /// Initialize a new <see cref="SetNotFoundException"/> class
        /// </summary>
        /// <param name="info">The serialization info of the exception</param>
        /// <param name="context">The streaming context of the exception</param>
        protected SetNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        #endregion CONSTRUCTOR
    }
}