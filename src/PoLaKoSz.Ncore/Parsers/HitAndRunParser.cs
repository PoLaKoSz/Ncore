using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using PoLaKoSz.Ncore.Exceptions;
using PoLaKoSz.Ncore.Models;

namespace PoLaKoSz.Ncore.Parsers
{
    internal class HitAndRunParser
    {
        private static string torrentUrlShema = "torrents.php?action=details&id=";

        /// <exception cref="DeprecatedWrapperException"></exception>
        internal async Task<IEnumerable<HitAndRunTorrent>> ExtractResultsFromAsync(string html)
        {
            List<HitAndRunTorrent> torrents = new List<HitAndRunTorrent>();

            var context = BrowsingContext.New(Configuration.Default);
            var document = await context.OpenAsync(req => req.Content(html)).ConfigureAwait(false);

            IElement container = document.QuerySelector("div.lista_all div.box_torrent_all");
            if (container == null)
            {
                throw new DeprecatedWrapperException("Couldn't find the torrent container!", document.DocumentElement);
            }

            IHtmlCollection<IElement> torrentNodes = container.QuerySelectorAll("div.hnr_torrents");
            if (torrentNodes == null)
            {
                throw new DeprecatedWrapperException("Couldn't find the Hit-And-Run torrent nodes!", container);
            }

            if (NoHitAndRun(torrentNodes))
            {
                return torrents;
            }

            DateTime currentTime = DateTime.Now;
            foreach (IElement torrentNode in torrentNodes)
            {
                try
                {
                    torrents.Add(ParseAsTorrent(torrentNode, currentTime));
                }
                catch (Exception ex)
                {
                    throw new DeprecatedWrapperException("Couldn't parse torrent details!", torrentNode, ex);
                }
            }

            return torrents;
        }

        private bool NoHitAndRun(IHtmlCollection<IElement> torrentNodes)
        {
            return torrentNodes.Count() == 1
                && torrentNodes.First().TextContent == "Az általad letöltött anyagokat a szabályoknak megfelelően visszaosztottad, a listád ennek köszönhetően üres.";
        }

        private HitAndRunTorrent ParseAsTorrent(IElement torrentNode, DateTime currentTime)
        {
            IElement nameNode = torrentNode.QuerySelector("div.hnr_tname a nobr");

            return new HitAndRunTorrent(
                ParseID(torrentNode),
                nameNode.TextContent,
                ParseEnd(torrentNode, currentTime),
                ParseRatio(torrentNode));
        }

        private int ParseID(IElement torrentNode)
        {
            try
            {
                IElement idNode = torrentNode.QuerySelector("div.hnr_tname a");
                string href = idNode.GetAttribute("href");
                return int.Parse(href.Substring(torrentUrlShema.Length));
            }
            catch (Exception ex)
            {
                throw new DeprecatedWrapperException("Couldn't parse the ID of the torrent", torrentNode, ex);
            }
        }

        private DateTime ParseEnd(IElement torrentNode, DateTime baseTime)
        {
            IElement node = torrentNode.QuerySelector("div.hnr_ttimespent span");
            string endTime = node.TextContent;
            string[] parts = endTime.Split(" ");
            int minutes = 0;
            foreach (string timePart in parts)
            {
                if (timePart.EndsWith("p"))
                {
                    minutes += int.Parse(timePart.Substring(0, timePart.Length - 1));
                }
                else if (timePart.EndsWith("ó"))
                {
                    minutes += int.Parse(timePart.Substring(0, timePart.Length - 1)) * 60;
                }
            }

            return baseTime.AddMinutes(minutes);
        }

        private float ParseRatio(IElement torrentNode)
        {
            try
            {
                IElement ratioNode = torrentNode.QuerySelector("div.hnr_tratio span");
                return float.Parse(ratioNode.TextContent, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                throw new DeprecatedWrapperException("Couldn't parse the ratio of the torrent", torrentNode, ex);
            }
        }
    }
}
