using System;
using System.Threading.Tasks;
using NUnit.Framework;
using PoLaKoSz.Ncore.EndPoints;
using PoLaKoSz.Ncore.Models;

namespace PoLaKoSz.Ncore.Tests.Integration.EndPoints
{
    [TestFixture]
    internal class TorrentEndPointTests : IntegrationTestFixture
    {
        private ITorrentEndPoint endPoint;

        public TorrentEndPointTests()
            : base("TorrentEndPoint")
        {
        }

        [SetUp]
        public void SetUp()
        {
            endPoint = base.GetAuthenticatedClient().Torrent;
        }

        [Test]
        public async Task GetParseIdCorrectly()
        {
            SetServerResponse("movie");

            Torrent torrent = await endPoint.Get(-1).ConfigureAwait(false);

            Assert.That(torrent.ID, Is.EqualTo(1683491));
        }

        [Test]
        public async Task GetParseUploadNameCorrectly()
        {
            SetServerResponse("movie");

            Torrent torrent = await endPoint.Get(-1).ConfigureAwait(false);

            Assert.That(torrent.UploadName, Is.EqualTo("The.Purge.Anarchy.2014.BDRip.XviD.Hungarian-nCORE"));
        }

        [Test]
        public async Task GetParseDownloadUrlCorrectly()
        {
            SetServerResponse("movie");

            Torrent torrent = await endPoint.Get(-1).ConfigureAwait(false);

            Assert.That(torrent.DownloadURL, Is.EqualTo(new Uri("https://ncore.pro/torrents.php?action=download&id=1683491&key=b6d64d585e5615c0721c1b008a7473bc")));
        }

        [Test]
        public async Task GetParseUploadDateCorrectly()
        {
            SetServerResponse("movie");

            Torrent torrent = await endPoint.Get(-1).ConfigureAwait(false);

            Assert.That(torrent.UploadDate, Is.EqualTo(new DateTime(2014, 11, 09, 19, 11, 29, DateTimeKind.Local)));
        }
    }
}
