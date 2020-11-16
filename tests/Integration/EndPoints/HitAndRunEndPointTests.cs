using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using PoLaKoSz.Ncore.EndPoints;
using PoLaKoSz.Ncore.Models;

namespace PoLaKoSz.Ncore.Tests.Integration.EndPoints
{
    [TestFixture]
    internal class HitAndRunEndPointTests : IntegrationTestFixture
    {
        private IHitAndRunEndPoint endPoint;

        public HitAndRunEndPointTests()
            : base("HitAndRunEndPoint")
        {
        }

        [SetUp]
        public void SetUp()
        {
            endPoint = base.GetAuthenticatedClient().HitAndRuns;
        }

        [Test]
        public async Task ListReturnsCorrectNumberOfTorrentsWhenThereAreTorrents()
        {
            SetServerResponse("multiple-torrents");

            IEnumerable<HitAndRunTorrent> torrents = await endPoint.List().ConfigureAwait(false);

            Assert.That(torrents, Has.Count.EqualTo(9));
        }

        [Test]
        public async Task ListFirstTorrentIDParsedCorrectly()
        {
            SetServerResponse("multiple-torrents");

            IEnumerable<HitAndRunTorrent> torrents = await endPoint.List().ConfigureAwait(false);
            HitAndRunTorrent firstTorrent = torrents.First();

            Assert.That(830948, Is.EqualTo(firstTorrent.ID));
        }

        [Test]
        public async Task ListFirstTorrentNameParsedCorrectly()
        {
            SetServerResponse("multiple-torrents");

            IEnumerable<HitAndRunTorrent> torrents = await endPoint.List().ConfigureAwait(false);
            HitAndRunTorrent firstTorrent = torrents.First();

            Assert.That("A.karate.kolyok.2010.1080p.RETAiL.BluRay.x264.HUN-BiTZo...", Is.EqualTo(firstTorrent.UploadName));
        }

        [Test]
        public async Task ListFirstTorrentHitAndRunParsedCorrectly()
        {
            SetServerResponse("multiple-torrents");
            DateTime currentTime = DateTime.Now;

            IEnumerable<HitAndRunTorrent> torrents = await endPoint.List().ConfigureAwait(false);
            HitAndRunTorrent firstTorrent = torrents.First();

            Assert.That(currentTime.Subtract(firstTorrent.HitAndRunEnd).TotalSeconds, Is.LessThan(2));
        }

        [Test]
        public async Task ListFirstTorrentRatioParsedCorrectly()
        {
            SetServerResponse("multiple-torrents");
            DateTime currentTime = DateTime.Now;

            IEnumerable<HitAndRunTorrent> torrents = await endPoint.List().ConfigureAwait(false);
            HitAndRunTorrent firstTorrent = torrents.First();

            Assert.That(firstTorrent.Ratio, Is.EqualTo(0.013f));
        }

        [Test]
        public async Task ListReturnsEmptyResultWhenNoHitAndRun()
        {
            SetServerResponse("no-torrent");

            IEnumerable<HitAndRunTorrent> torrents = await endPoint.List();

            Assert.That(torrents.Count(), Is.EqualTo(0));
        }
    }
}
