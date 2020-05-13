using System.Collections.Generic;
using System.Threading.Tasks;
using PoLaKoSz.Ncore.Exceptions;
using PoLaKoSz.Ncore.Models;

namespace PoLaKoSz.Ncore.EndPoints
{
    /// <summary>
    /// Provides access to the Hit'n'Run page.
    /// </summary>
    public interface IHitAndRunEndPoint
    {
        /// <summary>
        /// Gets all the torrents which must be seeded.
        /// </summary>
        /// <returns>Async <see cref="Task" />.</returns>
        /// <exception cref="DeprecatedWrapperException" />
        /// <exception cref="UnauthorizedException" />
        Task<IEnumerable<HitAndRunTorrent>> List();
    }
}
