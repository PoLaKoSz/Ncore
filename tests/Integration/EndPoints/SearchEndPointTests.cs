using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using PoLaKoSz.Ncore.EndPoints;
using PoLaKoSz.Ncore.Models;

namespace PoLaKoSz.Ncore.Tests.Integration.EndPoints
{
    [TestFixture]
    internal class SearchEndPointTests : IntegrationTestFixture
    {
        private ISearchEndPoint endPoint;

        public SearchEndPointTests()
            : base("SearchEndPoint")
        {
        }

        [SetUp]
        public void SetUp()
        {
            endPoint = base.GetAuthenticatedClient().Search;
        }
        
        [Test]
        public async Task ListWithMultiplePagesPopulatesPrimitiveProperties()
        {
            SetServerResponse("results-with-multiple-pages");

            ISearchResultContainer searchResult = await endPoint.List().ConfigureAwait(false);
            
            Assert.IsFalse(searchResult.HasPreviousPage, "HasPreviousPage");
            Assert.IsTrue(searchResult.HasNextPage, "HasNextPage");
            Assert.AreEqual(1, searchResult.CurrentPage, "CurrentPage");
        }

        [Test]
        public async Task ListWithoutResultsPopulatesPrimitiveProperties()
        {
            SetServerResponse("without-results");

            ISearchResultContainer searchResult = await endPoint.List().ConfigureAwait(false);
            
            Assert.IsFalse(searchResult.HasPreviousPage, "HasPreviousPage");
            Assert.IsFalse(searchResult.HasNextPage, "HasNextPage");
            Assert.AreEqual(1, searchResult.CurrentPage, "CurrentPage");
        }
        
        [Test]
        public async Task ListPopulatesFirstResultCorrectly()
        {
            SetServerResponse("results-with-multiple-pages");

            ISearchResultContainer searchResult = await endPoint.List();
            
            ISearchResultTorrent firstTorrent = searchResult.Results.First();
            Assert.AreEqual(3_012_464, firstTorrent.ID);
            Assert.AreEqual("Max.Payne.2008.UNRATED.BD25.AVC.DTSHD.HUN-TRiNiTY", firstTorrent.Name);
            Assert.AreEqual(new DateTime(2020, 4, 13, 13, 8, 29, DateTimeKind.Local), firstTorrent.UploadedAt);
        }
        
        [Test]
        public async Task ListPopulatesLastResultCorrectly()
        {
            SetServerResponse("results-with-multiple-pages");

            ISearchResultContainer searchResult = await endPoint.List();
            
            ISearchResultTorrent lastTorrent = searchResult.Results.Last();
            Assert.AreEqual(3_012_427, lastTorrent.ID);
            Assert.AreEqual("Alarcos.enekes.S01E01.WEB-DL.H264.Hun-TheMilkyWay", lastTorrent.Name);
            Assert.AreEqual(new DateTime(2020, 4, 13, 12, 25, 27, DateTimeKind.Local), lastTorrent.UploadedAt);
        }
    }
}
