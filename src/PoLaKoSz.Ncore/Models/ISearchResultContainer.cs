using System.Collections.Generic;

namespace PoLaKoSz.Ncore.Models
{
    /// <summary>
    /// Provides pagination functionality for the search page.
    /// </summary>
    public interface ISearchResultContainer
    {
        /// <summary>
        /// Gets whenever there is a previous page.
        /// </summary>
        bool HasPreviousPage { get; }

        /// <summary>
        /// Gets the current page number.
        /// </summary>
        int CurrentPage { get; }

        /// <summary>
        /// Gets whenever there is a nexxt page.
        /// </summary>
        bool HasNextPage { get; }

        /// <summary>
        /// Gets the actual torrent results for this page.
        /// </summary>
        IReadOnlyCollection<ISearchResultTorrent> Results { get; }
    }
}
