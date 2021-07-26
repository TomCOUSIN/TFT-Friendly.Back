using System;
using System.Runtime.Serialization;

namespace TFT_Friendly.Back.Exceptions
{
    /// <summary>
    /// ChampionConflictException class
    /// </summary>
    [Serializable]
    public class ChampionConflictException : Exception
    {
        #region CONSTRUCTOR
        
        /// <summary>
        /// Initialize a new <see cref="ChampionConflictException"/> class
        /// </summary>
        public ChampionConflictException() { }
        
        /// <summary>
        /// Initialize a new <see cref="ChampionConflictException"/> class
        /// </summary>
        /// <param name="message">The message of the exception</param>
        public ChampionConflictException(string message) : base(message) { }
        
        /// <summary>
        /// Initialize a new <see cref="ChampionConflictException"/> class
        /// </summary>
        /// <param name="message">The message of the exception</param>
        /// <param name="innerException">The inner exception of the exception</param>
        public ChampionConflictException(string message, Exception innerException) : base(message, innerException) { }
        
        /// <summary>
        /// Initialize a new <see cref="ChampionConflictException"/> class
        /// </summary>
        /// <param name="info">The serialization info of the exception</param>
        /// <param name="context">The streaming context of the exception</param>
        protected ChampionConflictException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        #endregion CONSTRUCTOR
    }
}