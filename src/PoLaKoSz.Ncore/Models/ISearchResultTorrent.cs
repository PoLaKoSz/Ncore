using System;

namespace PoLaKoSz.Ncore.Models
{
    /// <summary>
    /// Class to hold the data for a torrent in the search results.
    /// </summary>
    public interface ISearchResultTorrent
    {
        /// <summary>
        /// Gets the torrent ID.
        /// </summary>
        int ID { get; }

        /// <summary>
        /// Gets the torrent uploaded name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the UTC time when this torrent was uploaded.
        /// </summary>
        DateTime UploadedAt { get; }
    }
}
