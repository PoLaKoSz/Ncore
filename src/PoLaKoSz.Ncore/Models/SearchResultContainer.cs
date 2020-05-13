using System.Collections.Generic;
using PoLaKoSz.Ncore.EndPoints;

namespace PoLaKoSz.Ncore.Models
{
    /// <inheritdoc />
    internal class SearchResultContainer : ISearchResultContainer
    {
        /// <summary>
        /// Initialize a new instance while initializing the Torrents property
        /// and setting the <see cref="this.CurrentPage" /> to 1.
        /// </summary>
        internal SearchResultContainer()
        {
            Torrents = new List<SearchResultTorrentEndPoint>();
            CurrentPage = 1;
        }

        /// <inheritdoc />
        public bool HasPreviousPage { get; internal set; }

        /// <inheritdoc />
        public int CurrentPage { get; internal set; }

        /// <inheritdoc />
        public bool HasNextPage { get; internal set; }

        /// <inheritdoc />
        public IReadOnlyCollection<ISearchResultTorrent> Results => Torrents;

        internal List<SearchResultTorrentEndPoint> Torrents { get; }
    }
}
