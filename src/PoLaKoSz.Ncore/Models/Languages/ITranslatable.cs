using System;

namespace PoLaKoSz.Ncore.Languages
{
    /// <summary>
    /// Provides abtraction over each supported language by nCore.
    /// </summary>
    public interface ITranslatable
    {
        /// <summary>
        /// Gets the last menu name.
        /// </summary>
        string Quit { get; }

        /// <summary>
        /// Gets the string displayed when the search result doesn't contain any result.
        /// </summary>
        string NoSearchResults { get; }
    }
}
