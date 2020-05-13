using System;

namespace PoLaKoSz.Ncore.Languages
{
    /// <inheritdoc />
    internal class Hungarian : ITranslatable
    {
        private static string _name = "hu";

        /// <inheritdoc />
        public string Quit => "Kilépés";

        /// <inheritdoc />
        public string NoSearchResults => "Nincs találat!";

        public override string ToString() => _name;
    }
}
