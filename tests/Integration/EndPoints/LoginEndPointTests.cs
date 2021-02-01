using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using PoLaKoSz.Ncore.EndPoints;
using PoLaKoSz.Ncore.Exceptions;
using PoLaKoSz.Ncore.Models;

namespace PoLaKoSz.Ncore.Tests.Integration.EndPoints
{
    [TestFixture]
    internal class LoginEndPointTests : IntegrationTestFixture
    {
        private ILoginEndPoint endPoint;

        public LoginEndPointTests()
            : base("LoginEndPoint")
        {
        }

        [SetUp]
        public void SetUp()
        {
            endPoint = new NcoreClient(Web).Login;
        }

        [Test]
        public async Task AuthenticateWithAddsRequiredCookies()
        {
            UserConfig userConfig = new UserConfig("PoLáKoSz", "p4ssw0rd", "php");
            SetServerResponse("authenticated");

            await endPoint.AuthenticateWith(userConfig);

            CookieCollection cookies = Web.CookieContainer.GetCookies(new System.Uri("https://ncore.pro"));
            Assert.AreEqual("php", cookies["PHPSESSID"].Value);
            Assert.AreEqual("p4ssw0rd", cookies["pass"].Value);
            Assert.AreEqual("PoLáKoSz", cookies["nick"].Value);
            Assert.AreEqual("brutecore", cookies["stilus"].Value);
            Assert.AreEqual("hu", cookies["nyelv"].Value);
        }

        [Test]
        public void AuthenticateWithThrowsUnauthenticatedExceptionWhenLoginUnsuccessful()
        {
            UserConfig userConfig = new UserConfig(null, null, null);
            SetServerResponse("unauthenticated");

            UnauthorizedException ex = Assert.ThrowsAsync<UnauthorizedException>(async () =>
                await endPoint.AuthenticateWith(userConfig));
        }

        [Test]
        public async Task AuthenticateWithDoesNothingWhenLoginSuccessful()
        {
            UserConfig userConfig = new UserConfig(null, null, null);
            SetServerResponse("authenticated");

            await endPoint.AuthenticateWith(userConfig);
        }
    }
}
