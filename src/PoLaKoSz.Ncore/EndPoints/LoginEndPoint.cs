using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using PoLaKoSz.Ncore.Models;
using PoLaKoSz.Ncore.Parsers;

namespace PoLaKoSz.Ncore.EndPoints
{
    /// <inheritdoc />
    internal class LoginEndPoint : ILoginEndPoint
    {
        private static readonly string _cookiePath;
        private static readonly string _cookieDomain;
        private readonly HttpClient _httpClient;
        private readonly CookieContainer _cookies;
        private readonly LoginParser _parser;
        private readonly UserConfig _userConfig;

        static LoginEndPoint()
        {
            _cookiePath = "/";
            _cookieDomain = "ncore.pro";
        }

        internal LoginEndPoint(
            HttpClient httpClient,
            CookieContainer cookies,
            LoginParser parser,
            UserConfig userConfig)
        {
            _httpClient = httpClient;
            _cookies = cookies;
            _parser = parser;
            _userConfig = userConfig;
        }

        /// <inheritdoc />
        public async Task AuthenticateWith(UserConfig userConfig)
        {
            _userConfig.Language = userConfig.Language;
            _parser.Set(userConfig.Language);

            _cookies.Add(new Cookie("PHPSESSID", userConfig.PHPSessionID, _cookiePath, _cookieDomain));
            _cookies.Add(new Cookie("pass", userConfig.Password, _cookiePath, _cookieDomain));
            _cookies.Add(new Cookie("nick", userConfig.NickName, _cookiePath, _cookieDomain));
            _cookies.Add(new Cookie("stilus", userConfig.Theme.Name, _cookiePath, _cookieDomain));
            _cookies.Add(new Cookie("nyelv", userConfig.Language.ToString(), _cookiePath, _cookieDomain));
            _cookies.Add(new Cookie("adblock_tested", "false", _cookiePath, _cookieDomain));
            _cookies.Add(new Cookie("adblock_stat", "1", _cookiePath, _cookieDomain));

            string html = await _httpClient.GetStringAsync("/index.php").ConfigureAwait(false);

            await _parser.CheckIfLoggedIn(html).ConfigureAwait(false);
        }
    }
}
