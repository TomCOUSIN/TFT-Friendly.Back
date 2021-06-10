using System;
using System.Runtime.Serialization;

namespace TFT_Friendly.Back.Exceptions
{
    /// <summary>
    /// InvalidPasswordException class
    /// </summary>
    [Serializable]
    public class InvalidPasswordException : Exception
    {
        
        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="InvalidPasswordException"/> class
        /// </summary>
        public InvalidPasswordException() { }
        
        /// <summary>
        /// Initialize a new <see cref="InvalidPasswordException"/> class
        /// </summary>
        /// <param name="message">The message of the exception</param>
        public InvalidPasswordException(string message) : base(message) { }
        
        /// <summary>
        /// Initialize a new <see cref="InvalidPasswordException"/> class
        /// </summary>
        /// <param name="message">The message of the exception</param>
        /// <param name="innerException">The inner exception of the exception</param>
        public InvalidPasswordException(string message, Exception innerException) : base(message, innerException) { }
        
        /// <summary>
        /// Initialize a new <see cref="InvalidPasswordException"/> class
        /// </summary>
        /// <param name="info">The serialization info of the exception</param>
        /// <param name="context">The streaming context of the exception</param>
        protected InvalidPasswordException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        #endregion CONSTRUCTOR
    }
}