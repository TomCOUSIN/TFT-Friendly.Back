using System;
using System.Runtime.Serialization;

namespace TFT_Friendly.Back.Exceptions
{
    /// <summary>
    /// UserNotFoundException class
    /// </summary>
    [Serializable]
    public class UserNotFoundException : Exception
    {

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="UserNotFoundException"/> class
        /// </summary>
        public UserNotFoundException() { }
        
        /// <summary>
        /// Initialize a new <see cref="UserNotFoundException"/> class
        /// </summary>
        /// <param name="message">The message of the exception</param>
        public UserNotFoundException(string message) : base(message) { }
        
        /// <summary>
        /// Initialize a new <see cref="UserNotFoundException"/> class
        /// </summary>
        /// <param name="message">The message of the exception</param>
        /// <param name="innerException">The inner exception of the exception</param>
        public UserNotFoundException(string message, Exception innerException) : base(message, innerException) { }
        
        /// <summary>
        /// Initialize a new <see cref="UserConflictException"/> class
        /// </summary>
        /// <param name="info">The serialization info of the exception</param>
        /// <param name="context">The streaming context of the exception</param>
        protected UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        #endregion CONSTRUCTOR
    }
}