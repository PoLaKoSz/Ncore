using System.Net.Http;
using System.Threading.Tasks;
using PoLaKoSz.Ncore.Models;
using PoLaKoSz.Ncore.Parsers;

namespace PoLaKoSz.Ncore.EndPoints
{
    /// <inheritdoc cref="ITorrentEndPoint" />
    internal class TorrentEndPoint : ProtectedEndPoint, ITorrentEndPoint
    {
        private readonly TorrentParser _parser;

        internal TorrentEndPoint(HttpClient client, LoginParser authChecker)
            : base(client, authChecker)
        {
            _parser = new TorrentParser(client);
        }

        /// <inheritdoc />
        public async Task<Torrent> Get(int id)
        {
            string html = await GetStringAsync($"/torrents.php?action=details&id={id}").ConfigureAwait(false);

            return await _parser.ExtractResultFrom(html).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<Torrent> Get(ISearchResultTorrent torrent)
        {
            return Get(torrent.ID);
        }

        /// <inheritdoc />
        public Task<Torrent> Get(HitAndRunTorrent torrent)
        {
            return Get(torrent.ID);
        }
    }
}
