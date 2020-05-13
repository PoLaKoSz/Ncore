using System;

namespace PoLaKoSz.Ncore.Exceptions
{
    /// <summary>
    /// Exception when the requested resource is only accessible after authentication.
    /// </summary>
    public class UnauthorizedException : Exception, INCoreException
    {
    }
}
