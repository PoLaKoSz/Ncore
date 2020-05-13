using System.IO;
using PoLaKoSz.Ncore.Models;

namespace PoLaKoSz.Ncore.Tests.Integration
{
    internal abstract class IntegrationTestFixture
    {
        protected readonly LocalHttpMessageHandler Web;
        private string _moduleName;

        internal IntegrationTestFixture(string moduleName)
        {
            _moduleName = moduleName;
            Web = new LocalHttpMessageHandler();
        }

        protected NcoreClient GetAuthenticatedClient()
        {
            var client = new NcoreClient(Web);
            
            SetServerResponse("LoginEndPoint", "authenticated");
            
            client.Login.AuthenticateWith(new UserConfig(null, null, null))
                .ConfigureAwait(false).GetAwaiter().GetResult();

            return client;
        }

        protected void SetServerResponse(string sourceFile)
        {
            SetServerResponse(_moduleName, sourceFile);
        }

        private void SetServerResponse(string moduleName, string sourceFile)
        {
            string content = File.ReadAllText(Path.Combine("StaticResources", moduleName, sourceFile + ".html"));
            Web.SetResponse(content);
        }
    }
}
