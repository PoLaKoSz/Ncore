using System.Collections.Generic;

namespace PoLaKoSz.Ncore.Models
{
    /// <summary>
    /// Provides a method to build complex search queries.
    /// </summary>
    public class SearchQueryBuilder
    {
        private SearchQueryBuilder(string query)
        {
            Query = query;
        }

        internal string Query { get; }

        /// <summary>
        /// Initialize a new instance.
        /// </summary>
        /// <param name="query">The search pattern.</param>
        /// <returns>A new <see cref="SearchQueryBuilder" /> instance.</returns>
        public static SearchQueryBuilder Find(string query)
        {
            return new SearchQueryBuilder(query);
        }

        internal List<KeyValuePair<string, string>> ForHttpClient()
        {
            return new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("mire", Query),
                new KeyValuePair<string, string>("miben", "name"),
                new KeyValuePair<string, string>("tipus", "all_own"),
                new KeyValuePair<string, string>("tags", string.Empty),
            };
        }
    }
}
