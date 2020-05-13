using System;
using System.Net.Http;
using PoLaKoSz.Ncore.Models;
using PoLaKoSz.Ncore.Parsers;

namespace PoLaKoSz.Ncore.EndPoints
{
    /// <inheritdoc cref="ISearchResultTorrent" />
    internal class SearchResultTorrentEndPoint : ProtectedEndPoint, ISearchResultTorrent
    {
        internal SearchResultTorrentEndPoint(int id, string name, DateTime uploadedAt, HttpClient client, LoginParser authChecker)
            : base(client, authChecker)
        {
            ID = id;
            Name = name;
            UploadedAt = uploadedAt;
        }

        /// <inheritdoc />
        public int ID { get; }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public DateTime UploadedAt { get; }
    }
}
