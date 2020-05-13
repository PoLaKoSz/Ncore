using System.Threading.Tasks;
using PoLaKoSz.Ncore.Models;

namespace PoLaKoSz.Ncore.EndPoints
{
    /// <summary>
    /// Provides access to the search page.
    /// </summary>
    public interface ISearchEndPoint
    {
        /// <summary>
        /// Gets the most freshly uploaded torrents.
        /// </summary>
        /// <returns>Async <see cref="Task" />.</returns>
        /// <exception cref="DeprecatedWrapperException" />
        /// <exception cref="UnauthorizedException" />
        Task<ISearchResultContainer> List();

        /// <summary>
        /// Executes a custom search.
        /// </summary>
        /// <param name="query"></param>
        /// <returns>Async <see cref="Task" />.</returns>
        /// <exception cref="DeprecatedWrapperException" />
        /// <exception cref="UnauthorizedException" />
        Task<ISearchResultContainer> For(string query);

        /// <summary>
        /// Executes an advanced search.
        /// </summary>
        /// <returns>Async <see cref="Task" />.</returns>
        /// <exception cref="DeprecatedWrapperException" />
        /// <exception cref="UnauthorizedException" />
        Task<ISearchResultContainer> For(SearchQueryBuilder query);
    }
}
