using System;
using System.Runtime.Serialization;

namespace TFT_Friendly.Back.Exceptions
{
    /// <summary>
    /// SetConflictException class
    /// </summary>
    [Serializable]
    public class SetConflictException : Exception
    {
        #region CONSTRUCTOR
        
        /// <summary>
        /// Initialize a new <see cref="SetConflictException"/> class
        /// </summary>
        public SetConflictException() { }
        
        /// <summary>
        /// Initialize a new <see cref="SetConflictException"/> class
        /// </summary>
        /// <param name="message">The message of the exception</param>
        public SetConflictException(string message) : base(message) { }
        
        /// <summary>
        /// Initialize a new <see cref="SetConflictException"/> class
        /// </summary>
        /// <param name="message">The message of the exception</param>
        /// <param name="innerException">The inner exception of the exception</param>
        public SetConflictException(string message, Exception innerException) : base(message, innerException) { }
        
        /// <summary>
        /// Initialize a new <see cref="SetConflictException"/> class
        /// </summary>
        /// <param name="info">The serialization info of the exception</param>
        /// <param name="context">The streaming context of the exception</param>
        protected SetConflictException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        #endregion CONSTRUCTOR
    }
}