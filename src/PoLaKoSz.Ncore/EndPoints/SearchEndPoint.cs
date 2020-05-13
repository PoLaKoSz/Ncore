using System.Net.Http;
using System.Threading.Tasks;
using PoLaKoSz.Ncore.Models;
using PoLaKoSz.Ncore.Parsers;

namespace PoLaKoSz.Ncore.EndPoints
{
    /// <inheritdoc cref="ISearchEndPoint" />
    internal class SearchEndPoint : ProtectedEndPoint, ISearchEndPoint
    {
        private readonly SearchParser _parser;

        internal SearchEndPoint(HttpClient client, LoginParser authChecker, UserConfig userConfig)
            : base(client, authChecker)
        {
            _parser = new SearchParser(client, authChecker, userConfig);
        }

        /// <inheritdoc />
        public async Task<ISearchResultContainer> List()
        {
            string html = await GetStringAsync("/torrents.php").ConfigureAwait(false);

            return await _parser.ExtractResultsFrom(html).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<ISearchResultContainer> For(string query)
        {
            return For(SearchQueryBuilder.Find(query));
        }

        /// <inheritdoc />
        public async Task<ISearchResultContainer> For(SearchQueryBuilder query)
        {
            var formData = query.ForHttpClient();

            AddFakeClickTo("submit", 85, 33, formData);

            string html = await PostAsync("/torrents.php", formData)
                .ConfigureAwait(false);

            return await _parser.ExtractResultsFrom(html).ConfigureAwait(false);
        }
    }
}
