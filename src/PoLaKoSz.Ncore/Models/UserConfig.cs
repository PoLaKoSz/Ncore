using PoLaKoSz.Ncore.Languages;
using PoLaKoSz.Ncore.Themes;

namespace PoLaKoSz.Ncore.Models
{
    /// <summary>
    /// Class to hold (cookie) data about a potential nCore User.
    /// </summary>
    public class UserConfig
    {
        /// <summary>
        /// Initialize a new instance with the default theme as <see cref="BruteCore" />
        /// and language as <see cref="Hungarian" />.
        /// </summary>
        /// <param name="nickName">The nick cookie value.</param>
        /// <param name="password">The pass cookie value.</param>
        /// <param name="phpSessionID">The PHPSESSID cookie value.</param>
        public UserConfig(string nickName, string password, string phpSessionID)
        {
            NickName = nickName;
            Password = password;
            PHPSessionID = phpSessionID;
            Theme = new BruteCore();
            Language = new Hungarian();
        }

        /// <summary>
        /// Initialize a new instance with every required property as null.
        /// </summary>
        internal UserConfig()
            : this(null, null, null)
        {
        }

        /// <summary>
        /// Gets the value of the nick cookie.
        /// </summary>
        public string NickName { get; }

        /// <summary>
        /// Gets the value of the pass cookie.
        /// </summary>
        public string Password { get; }

        /// <summary>
        /// Gets the value of the PHPSESSID cookie.
        /// </summary>
        public string PHPSessionID { get; }

        /// <summary>
        /// Gets the value of the stilus cookie.
        /// </summary>
        public ITheme Theme { get; }

        /// <summary>
        /// Gets the value of the nyelv cookie.
        /// </summary>
        public ITranslatable Language { get; set; }
    }
}
