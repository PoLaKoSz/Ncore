using System;
using AngleSharp.Dom;

namespace PoLaKoSz.Ncore.Exceptions
{
    /// <summary>
    /// This exception indicates that the parsing algorithm failed
    /// to extract the data from the response sent by the nCore server.
    /// </summary>
    public class DeprecatedWrapperException : Exception, INCoreException
    {
        internal DeprecatedWrapperException(string message)
            : base(message)
        {
        }

        internal DeprecatedWrapperException(string message, IElement faultyElement)
            : this(message)
        {
            FaultyElement = faultyElement;
        }

        internal DeprecatedWrapperException(string message, IElement faultyElement, Exception ex)
            : base(message, ex)
        {
            FaultyElement = faultyElement;
        }

        /// <summary>
        /// Gets the DOM element which caused the parser to throw this exception.
        /// </summary>
        public IElement FaultyElement { get; }
    }
}
