using System;
using System.Runtime.Serialization;

namespace TFT_Friendly.Back.Exceptions
{
    /// <summary>
    /// TraitNotFoundException class
    /// </summary>
    [Serializable]
    public class TraitNotFoundException : Exception
    {
        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="TraitNotFoundException"/> class
        /// </summary>
        public TraitNotFoundException() { }
        
        /// <summary>
        /// Initialize a new <see cref="TraitNotFoundException"/> class
        /// </summary>
        /// <param name="message">The message of the exception</param>
        public TraitNotFoundException(string message) : base(message) { }
        
        /// <summary>
        /// Initialize a new <see cref="TraitNotFoundException"/> class
        /// </summary>
        /// <param name="message">The message of the exception</param>
        /// <param name="innerException">The inner exception of the exception</param>
        public TraitNotFoundException(string message, Exception innerException) : base(message, innerException) { }
        
        /// <summary>
        /// Initialize a new <see cref="TraitNotFoundException"/> class
        /// </summary>
        /// <param name="info">The serialization info of the exception</param>
        /// <param name="context">The streaming context of the exception</param>
        protected TraitNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        #endregion CONSTRUCTOR
    }
}