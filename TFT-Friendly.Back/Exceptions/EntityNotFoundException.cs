using System;
using System.Runtime.Serialization;

namespace TFT_Friendly.Back.Exceptions
{
    /// <summary>
    /// EntityNotFoundException class
    /// </summary>
    [Serializable]
    public class EntityNotFoundException : Exception
    {
        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="EntityNotFoundException"/> class
        /// </summary>
        public EntityNotFoundException() { }
        
        /// <summary>
        /// Initialize a new <see cref="EntityNotFoundException"/> class
        /// </summary>
        /// <param name="message">The message of the exception</param>
        public EntityNotFoundException(string message) : base(message) { }
        
        /// <summary>
        /// Initialize a new <see cref="EntityNotFoundException"/> class
        /// </summary>
        /// <param name="message">The message of the exception</param>
        /// <param name="innerException">The inner exception of the exception</param>
        public EntityNotFoundException(string message, Exception innerException) : base(message, innerException) { }
        
        /// <summary>
        /// Initialize a new <see cref="EntityNotFoundException"/> class
        /// </summary>
        /// <param name="info">The serialization info of the exception</param>
        /// <param name="context">The streaming context of the exception</param>
        protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        #endregion CONSTRUCTOR
    }
}