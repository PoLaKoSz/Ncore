using System;

namespace PoLaKoSz.Ncore.Themes
{
    /// <inheritdoc />
    public class BruteCore : ITheme
    {
        private static readonly string _name = "brutecore";

        /// <inheritdoc />
        public string Name => _name;

        public override string ToString() => Name;
    }
}
