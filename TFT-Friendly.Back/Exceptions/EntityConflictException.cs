using System;
using System.Runtime.Serialization;

namespace TFT_Friendly.Back.Exceptions
{
    /// <summary>
    /// EntityConflictException class
    /// </summary>
    [Serializable]
    public class EntityConflictException : Exception
    {
        #region CONSTRUCTOR
        
        /// <summary>
        /// Initialize a new <see cref="EntityConflictException"/> class
        /// </summary>
        public EntityConflictException() { }
        
        /// <summary>
        /// Initialize a new <see cref="EntityConflictException"/> class
        /// </summary>
        /// <param name="message">The message of the exception</param>
        public EntityConflictException(string message) : base(message) { }
        
        /// <summary>
        /// Initialize a new <see cref="ChampionConflictException"/> class
        /// </summary>
        /// <param name="message">The message of the exception</param>
        /// <param name="innerException">The inner exception of the exception</param>
        public EntityConflictException(string message, Exception innerException) : base(message, innerException) { }
        
        /// <summary>
        /// Initialize a new <see cref="ChampionConflictException"/> class
        /// </summary>
        /// <param name="info">The serialization info of the exception</param>
        /// <param name="context">The streaming context of the exception</param>
        protected EntityConflictException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        #endregion CONSTRUCTOR
    }
}