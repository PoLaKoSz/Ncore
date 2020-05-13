using System;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using PoLaKoSz.Ncore.Exceptions;
using PoLaKoSz.Ncore.Models;

namespace PoLaKoSz.Ncore.Parsers
{
    internal class TorrentParser
    {
        private static string torrentUrlShema = "torrents.php?action=addnews&id=";
        private readonly HttpClient _client;

        public TorrentParser(HttpClient client)
        {
            _client = client;
        }

        /// <exception cref="DeprecatedWrapperException"></exception>
        internal async Task<Torrent> ExtractResultFrom(string html)
        {
            var context = BrowsingContext.New(Configuration.Default);
            IDocument document = await context.OpenAsync(req => req.Content(html)).ConfigureAwait(false);

            IElement uploadNameNode = document.QuerySelector("div#details1 div.fobox_tartalom div.torrent_reszletek_cim");
            if (uploadNameNode == null)
            {
                throw new DeprecatedWrapperException("Couldn't find upload name!", document.DocumentElement);
            }

            IElement uploadDateTitleNode = document.QuerySelector("#details1 > div.fobox_tartalom > div.torrent_reszletek > div.torrent_col1 > div:nth-child(3)");
            if (uploadDateTitleNode == null)
            {
                throw new DeprecatedWrapperException("Couldn't find upload date title!", document.DocumentElement);
            }

            IElement uploadDateNode = uploadDateTitleNode.NextElementSibling;
            if (uploadDateNode == null)
            {
                throw new DeprecatedWrapperException("Couldn't find the upload date!", uploadDateTitleNode.ParentElement);
            }

            if (!DateTime.TryParse(uploadDateNode.TextContent, out DateTime uploadDate))
            {
                throw new DeprecatedWrapperException("Invalid upload date!", uploadDateNode);
            }

            return new Torrent(
                ParseID(document),
                uploadNameNode.TextContent,
                uploadDate,
                ParseTorrentFileUrl(document),
                _client);
        }

        private int ParseID(IDocument document)
        {
            IElement idNode = document.QuerySelector("div#details1 div.fobox_tartalom div.torrent_reszletek_konyvjelzo a:nth-child(2)");
            if (idNode == null)
            {
                throw new DeprecatedWrapperException("Couldn't find node to extract torrent ID!", document.DocumentElement);
            }

            if (!idNode.HasAttribute("href"))
            {
                throw new DeprecatedWrapperException("Node which should contain the ID is invalid!", idNode);
            }

            string href = idNode.GetAttribute("href");
            if (href.Length <= torrentUrlShema.Length)
            {
                throw new DeprecatedWrapperException("Node which should contain the ID is invalid!", idNode);
            }

            string id = href.Substring(torrentUrlShema.Length);
            id = id.Substring(0, id.IndexOf("&"));

            return int.Parse(id);
        }

        private Uri ParseTorrentFileUrl(IDocument document)
        {
            IElement anchorNode = document.QuerySelector("div#details1 div.fobox_tartalom div.download a");
            if (anchorNode == null)
            {
                throw new DeprecatedWrapperException("Couldn't find node to extract torrent file URL!", document.DocumentElement);
            }

            if (!anchorNode.HasAttribute("href"))
            {
                throw new DeprecatedWrapperException("Invalidd torrent file URL node!", anchorNode);
            }

            return new Uri($"{_client.BaseAddress}{anchorNode.GetAttribute("href")}");
        }
    }
}