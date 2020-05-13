using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using PoLaKoSz.Ncore.EndPoints;
using PoLaKoSz.Ncore.Exceptions;
using PoLaKoSz.Ncore.Models;

namespace PoLaKoSz.Ncore.Parsers
{
    internal class SearchParser
    {
        private readonly HttpClient _httpClient;
        private readonly LoginParser _authParser;
        private readonly UserConfig _userConfig;

        internal SearchParser(HttpClient httpClient, LoginParser authParser, UserConfig userConfig)
        {
            _httpClient = httpClient;
            _authParser = authParser;
            _userConfig = userConfig;
        }

        /// <exception cref="DeprecatedWrapperException"></exception>
        internal async Task<ISearchResultContainer> ExtractResultsFrom(string html)
        {
            var context = BrowsingContext.New(Configuration.Default);

            var document = await context.OpenAsync(req => req.Content(html)).ConfigureAwait(false);

            IElement resultsContainerNode = document.QuerySelector("div#main_tartalom div.lista_all div.box_torrent_all");
            if (resultsContainerNode == null)
            {
                throw new DeprecatedWrapperException("Couldn't find the container for the search results!", document.DocumentElement);
            }

            IElement noResultNode = resultsContainerNode.QuerySelector("div.lista_mini_error");
            if (noResultNode != null && noResultNode.TextContent == _userConfig.Language.NoSearchResults)
            {
                return new SearchResultContainer();
            }

            IElement topPaginationElement = document.QuerySelector("#pager_top");
            if (topPaginationElement == null)
            {
                throw new DeprecatedWrapperException("Couldn't find the container for the pagination!");
            }

            SearchResultContainer resultContainer = new SearchResultContainer();
            resultContainer.HasPreviousPage = topPaginationElement.QuerySelector("#pPa") != null;
            resultContainer.HasNextPage = topPaginationElement.QuerySelector("#nPa") != null;

            if (resultContainer.HasPreviousPage)
            {
                IElement prevPageLink = topPaginationElement.QuerySelector("#pPa");
                resultContainer.CurrentPage = GetPageFrom(prevPageLink) + 1;
            }
            else if (resultContainer.HasNextPage)
            {
                IElement nextPageLink = topPaginationElement.QuerySelector("#nPa");
                resultContainer.CurrentPage = GetPageFrom(nextPageLink) - 1;
            }

            IHtmlCollection<IElement> resultNodes = resultsContainerNode.QuerySelectorAll("div.box_torrent");
            if (resultNodes == null)
            {
                throw new DeprecatedWrapperException("Couldn't find the container for the search results!", resultsContainerNode);
            }

            foreach (IElement result in resultNodes)
            {
                resultContainer.Torrents.Add(ParseAsTorrent(result));
            }

            return resultContainer;
        }

        private int GetPageFrom(IElement anchor)
        {
            if (!anchor.HasAttribute("href"))
            {
                throw new DeprecatedWrapperException("No href attribute found for paginator <a>", anchor);
            }

            string hrefAttr = anchor.GetAttribute("href");
            if (!hrefAttr.StartsWith("/torrents.php?oldal=") || hrefAttr.Length == "/torrents.php?oldal=".Length)
            {
                throw new DeprecatedWrapperException("Invalid href attribute for paginator <a>!", anchor);
            }

            if (!int.TryParse(hrefAttr.Substring("/torrents.php?oldal=".Length), out int anchorPage))
            {
                throw new DeprecatedWrapperException("Invalid int page number in paginator <a>!", anchor);
            }

            return anchorPage;
        }

        private SearchResultTorrentEndPoint ParseAsTorrent(IElement torrentElement)
        {
            int torrentID = GetTorrentID(torrentElement);
            string name = GetTorrentName(torrentElement, torrentID);
            DateTime uploadDate = GetTorrentUploadDate(torrentElement, torrentID);

            return new SearchResultTorrentEndPoint(torrentID, name, uploadDate, _httpClient, _authParser);
        }

        private int GetTorrentID(IElement torrentElement)
        {
            IElement torrentIdElement = torrentElement.NextElementSibling?.NextElementSibling;
            if (torrentIdElement == null)
            {
                throw new DeprecatedWrapperException("Couldn't find the node containing the torrent's ID attribute!", torrentElement);
            }

            if (!torrentIdElement.HasAttribute("id"))
            {
                throw new DeprecatedWrapperException("No id attribute found for torrent!", torrentIdElement);
            }

            if (!int.TryParse(torrentIdElement.GetAttribute("id"), out int torrentID))
            {
                throw new DeprecatedWrapperException("Invalid id found for torrent!", torrentIdElement);
            }

            return torrentID;
        }

        private string GetTorrentName(IElement torrentElement, int torrentID)
        {
            IElement primaryNameElement = torrentElement.QuerySelector("div.torrent_txt a nobr");
            if (primaryNameElement != null)
            {
                return primaryNameElement.TextContent;
            }

            IElement secondaryNameElement = torrentElement.QuerySelector("div.torrent_txt2 a nobr");
            if (secondaryNameElement != null)
            {
                return secondaryNameElement.TextContent;
            }

            throw new DeprecatedWrapperException($"Couldn't find the name of the torrent {{ ID = {torrentID} }}!", torrentElement);
        }

        private DateTime GetTorrentUploadDate(IElement torrentElement, int torrentID)
        {
            IElement dateElement = torrentElement.QuerySelector("div.box_feltoltve2");

            if (dateElement == null)
            {
                throw new DeprecatedWrapperException($"Couldn't find the upload date of the torrent {{ ID = {torrentID} }}!", torrentElement);
            }

            if (!DateTime.TryParseExact(dateElement.TextContent, "yyyy-MM-ddHH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime uploadDate))
            {
                throw new DeprecatedWrapperException($"Invalid upload date for torrent {{ ID = {torrentID} }}!", torrentElement);
            }

            return uploadDate;
        }
    }
}
