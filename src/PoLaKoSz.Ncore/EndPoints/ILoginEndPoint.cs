using System.Threading.Tasks;
using PoLaKoSz.Ncore.Models;

namespace PoLaKoSz.Ncore.EndPoints
{
    /// <summary>
    /// Provides access to the login page.
    /// </summary>
    public interface ILoginEndPoint
    {
        /// <summary>
        /// Authenticates the User on nCore.
        /// </summary>
        /// <param name="userConfig">Credentials for an nCore user.</param>
        /// <returns>Async <see ref="Task" />.</returns>
        /// <exception ref="UnauthorizedException" />
        Task AuthenticateWith(UserConfig userConfig);
    }
}
