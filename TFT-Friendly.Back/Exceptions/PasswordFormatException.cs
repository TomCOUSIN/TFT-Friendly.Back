using System;
using System.Runtime.Serialization;

namespace TFT_Friendly.Back.Exceptions
{
    /// <summary>
    /// PasswordFormatException class
    /// </summary>
    [Serializable]
    public class PasswordFormatException : Exception
    {

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="PasswordFormatException"/> class
        /// </summary>
        public PasswordFormatException() { }
        
        /// <summary>
        /// Initialize a new <see cref="PasswordFormatException"/> class
        /// </summary>
        /// <param name="message">The message of the exception</param>
        public PasswordFormatException(string message) : base(message) { }
        
        /// <summary>
        /// Initialize a new <see cref="PasswordFormatException"/> class
        /// </summary>
        /// <param name="message">The message of the exception</param>
        /// <param name="innerException">The inner exception of the exception</param>
        public PasswordFormatException(string message, Exception innerException) : base(message, innerException) { }
        
        /// <summary>
        /// Initialize a new <see cref="PasswordFormatException"/> class
        /// </summary>
        /// <param name="info">The serialization info of the exception</param>
        /// <param name="context">The streaming context of the exception</param>
        protected PasswordFormatException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        #endregion CONSTRUCTOR
    }
}