using System;
using System.Runtime.Serialization;

namespace TFT_Friendly.Back.Exceptions
{
    /// <summary>
    /// ChampionNotFoundException class
    /// </summary>
    [Serializable]
    public class ChampionNotFoundException : Exception
    {
        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="ChampionNotFoundException"/> class
        /// </summary>
        public ChampionNotFoundException() { }
        
        /// <summary>
        /// Initialize a new <see cref="ChampionNotFoundException"/> class
        /// </summary>
        /// <param name="message">The message of the exception</param>
        public ChampionNotFoundException(string message) : base(message) { }
        
        /// <summary>
        /// Initialize a new <see cref="ChampionNotFoundException"/> class
        /// </summary>
        /// <param name="message">The message of the exception</param>
        /// <param name="innerException">The inner exception of the exception</param>
        public ChampionNotFoundException(string message, Exception innerException) : base(message, innerException) { }
        
        /// <summary>
        /// Initialize a new <see cref="ChampionNotFoundException"/> class
        /// </summary>
        /// <param name="info">The serialization info of the exception</param>
        /// <param name="context">The streaming context of the exception</param>
        protected ChampionNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        #endregion CONSTRUCTOR
    }
}