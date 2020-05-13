using System.Threading.Tasks;
using PoLaKoSz.Ncore.Models;

namespace PoLaKoSz.Ncore.EndPoints
{
    /// <summary>
    /// Provides access to a specific torrent.
    /// </summary>
    public interface ITorrentEndPoint
    {
        /// <summary>
        /// Gets the details of a torrent.
        /// </summary>
        /// <param name="id">The requested torrent unique ID.</param>
        /// <returns>Async <see cref="Task" />.</returns>
        Task<Torrent> Get(int id);

        /// <summary>
        /// Gets the details of a torrent.
        /// </summary>
        /// <param name="torrent">The requested torrent.</param>
        /// <returns>Async <see cref="Task" />.</returns>
        Task<Torrent> Get(ISearchResultTorrent torrent);

        /// <summary>
        /// Gets the details of a torrent.
        /// </summary>
        /// <param name="torrent">The requested torrent.</param>
        /// <returns>Async <see cref="Task" />.</returns>
        Task<Torrent> Get(HitAndRunTorrent torrent);
    }
}
