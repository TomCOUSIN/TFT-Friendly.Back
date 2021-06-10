using System;

namespace TFT_Friendly.Back.Models.Errors
{
    /// <summary>
    /// HttpError class
    /// </summary>
    public class HttpError
    {
        #region MEMBERS

        /// <summary>
        /// HttpStatus of the error
        /// </summary>
        public int Status { get; }

        /// <summary>
        /// Message of the error
        /// </summary>
        public string Message { get; }

        #endregion MEMBERS

        #region CONSTRUCTOR

        /// <summary>
        /// Initialize a new <see cref="HttpError"/> class
        /// </summary>
        /// <param name="status">The status of the error</param>
        /// <param name="message">The message of the error</param>
        /// <exception cref="ArgumentNullException">Throw an exception if one parameter is null</exception>
        public HttpError(int status, string message)
        {
            Status = status;
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }

        #endregion CONSTRUCTOR
    }
}