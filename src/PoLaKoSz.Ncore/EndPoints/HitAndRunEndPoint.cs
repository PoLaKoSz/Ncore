using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using PoLaKoSz.Ncore.Models;
using PoLaKoSz.Ncore.Parsers;

namespace PoLaKoSz.Ncore.EndPoints
{
    /// <inheritdoc cref="IHitAndRunEndPoint" />
    internal class HitAndRunEndPoint : ProtectedEndPoint, IHitAndRunEndPoint
    {
        private readonly HitAndRunParser _parser;

        internal HitAndRunEndPoint(HttpClient client, LoginParser authChecker)
            : base(client, authChecker)
        {
            _parser = new HitAndRunParser();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<HitAndRunTorrent>> List()
        {
            string html = await GetStringAsync("/hitnrun.php").ConfigureAwait(false);

            return await _parser.ExtractResultsFromAsync(html).ConfigureAwait(false);
        }
    }
}
