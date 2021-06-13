using System;
using System.Runtime.Serialization;

namespace TFT_Friendly.Back.Exceptions
{
    /// <summary>
    /// ItemNotFoundException class
    /// </summary>
    [Serializable]
    public class ItemNotFoundException : Exception
    {

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="ItemNotFoundException"/> class
        /// </summary>
        public ItemNotFoundException() { }
        
        /// <summary>
        /// Initialize a new <see cref="ItemNotFoundException"/> class
        /// </summary>
        /// <param name="message">The message of the exception</param>
        public ItemNotFoundException(string message) : base(message) { }
        
        /// <summary>
        /// Initialize a new <see cref="ItemNotFoundException"/> class
        /// </summary>
        /// <param name="message">The message of the exception</param>
        /// <param name="innerException">The inner exception of the exception</param>
        public ItemNotFoundException(string message, Exception innerException) : base(message, innerException) { }
        
        /// <summary>
        /// Initialize a new <see cref="ItemNotFoundException"/> class
        /// </summary>
        /// <param name="info">The serialization info of the exception</param>
        /// <param name="context">The streaming context of the exception</param>
        protected ItemNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        #endregion CONSTRUCTOR
    }
}