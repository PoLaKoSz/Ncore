using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PoLaKoSz.Ncore.Tests.Integration
{
    internal class LocalHttpMessageHandler : HttpClientHandler
    {
        private string _content;

        internal void SetResponse(string content)
        {
            _content = content;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var httpResponse = new HttpResponseMessage();
            httpResponse.Content = new StringContent(_content);
            return Task.FromResult(httpResponse);
        }
    }
}
