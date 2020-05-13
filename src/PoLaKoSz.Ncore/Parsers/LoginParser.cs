using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using PoLaKoSz.Ncore.Exceptions;
using PoLaKoSz.Ncore.Languages;

namespace PoLaKoSz.Ncore.Parsers
{
    internal class LoginParser
    {
        private ITranslatable _uiLanguage;

        internal void Set(ITranslatable uiLanguage)
        {
            _uiLanguage = uiLanguage;
        }

        /// <exception cref="UnauthorizedException"></exception>
        internal async Task CheckIfLoggedIn(string html)
        {
            var context = BrowsingContext.New(Configuration.Default);

            var document = await context.OpenAsync(req => req.Content(html)).ConfigureAwait(false);

            IElement quitMenu = document.QuerySelector("div#menu_center div.menu_text:last-of-type");

            if (quitMenu == null || quitMenu.TextContent != _uiLanguage.Quit)
            {
                throw new UnauthorizedException();
            }
        }
    }
}
