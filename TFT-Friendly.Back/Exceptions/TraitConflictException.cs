using System;
using System.Runtime.Serialization;

namespace TFT_Friendly.Back.Exceptions
{
    /// <summary>
    /// TraitConflictException class
    /// </summary>
    [Serializable]
    public class TraitConflictException : Exception
    {
        #region CONSTRUCTOR
        
        /// <summary>
        /// Initialize a new <see cref="TraitConflictException"/> class
        /// </summary>
        public TraitConflictException() { }
        
        /// <summary>
        /// Initialize a new <see cref="TraitConflictException"/> class
        /// </summary>
        /// <param name="message">The message of the exception</param>
        public TraitConflictException(string message) : base(message) { }
        
        /// <summary>
        /// Initialize a new <see cref="TraitConflictException"/> class
        /// </summary>
        /// <param name="message">The message of the exception</param>
        /// <param name="innerException">The inner exception of the exception</param>
        public TraitConflictException(string message, Exception innerException) : base(message, innerException) { }
        
        /// <summary>
        /// Initialize a new <see cref="UserConflictException"/> class
        /// </summary>
        /// <param name="info">The serialization info of the exception</param>
        /// <param name="context">The streaming context of the exception</param>
        protected TraitConflictException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        #endregion CONSTRUCTOR
    }
}