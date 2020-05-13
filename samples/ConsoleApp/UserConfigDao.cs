using PoLaKoSz.Ncore.Models;

namespace PoLaKoSz.Ncore.Samples.ConsoleApp
{
    public class UserConfigDao : UserConfig
    {
        public UserConfigDao(string nickName, string password, string phpSessionID)
            : base(nickName, password, phpSessionID)
        {
        }

        public bool ShouldSerializeTheme()
        {
            return false;
        }

        public bool ShouldSerializeLanguage()
        {
            return false;
        }
    }
}
