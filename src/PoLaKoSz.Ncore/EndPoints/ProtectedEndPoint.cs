using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using PoLaKoSz.Ncore.Parsers;

namespace PoLaKoSz.Ncore.EndPoints
{
    /// <summary>
    /// Provides authenticated GET and POST methods
    /// with fake button clicking if needed.
    /// </summary>
    internal abstract class ProtectedEndPoint
    {
        private static readonly Random _random;
        private readonly HttpClient _client;
        private readonly LoginParser _parser;

        static ProtectedEndPoint()
        {
            _random = new Random();
        }

        protected ProtectedEndPoint(HttpClient client, LoginParser authChecker)
        {
            _client = client;
            _parser = authChecker;
        }

        /// <exception cref="UnauthorizedException"></exception>
        protected async Task<string> GetStringAsync(string requestUri)
        {
            string response = await _client.GetStringAsync(requestUri).ConfigureAwait(false);

            await _parser.CheckIfLoggedIn(response).ConfigureAwait(false);

            return response;
        }

        protected async Task<string> PostAsync(string requestUri, List<KeyValuePair<string, string>> formData)
        {
            HttpResponseMessage response = await _client
                .PostAsync(requestUri, new FormUrlEncodedContent(formData))
                .ConfigureAwait(false);

            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Adds a few parameters to the form to look like a manual click.
        /// </summary>
        /// <param name="buttonName">Submit button's name attribute value.</param>
        /// <param name="buttonWidth">Submit button's width (in pixels) on the UI.</param>
        /// <param name="buttonHeight">Submit button's height (in pixels) on the UI.</param>
        /// <param name="formData">Where to add the additional values.</param>
        protected void AddFakeClickTo(string buttonName, int buttonWidth, int buttonHeight, List<KeyValuePair<string, string>> formData)
        {
            int leftMargin = Convert.ToInt32(buttonWidth * 0.3f);
            int topMargin = Convert.ToInt32(buttonHeight * 0.3f);

            int x = leftMargin + _random.Next(buttonWidth - (leftMargin * 2));
            int y = topMargin + _random.Next(buttonHeight - (topMargin * 2));

            formData.Add(new KeyValuePair<string, string>($"{buttonName}.x", x.ToString()));
            formData.Add(new KeyValuePair<string, string>($"{buttonName}.y", y.ToString()));
        }
    }
}
