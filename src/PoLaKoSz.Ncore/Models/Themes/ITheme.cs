using System;

namespace PoLaKoSz.Ncore.Themes
{
    /// <summary>
    /// Holds data for a specifc theme supported by nCore.
    /// </summary>
    public interface ITheme
    {
        /// <summary>
        /// Gets the name of the theme.
        /// </summary>
        string Name { get; }
    }
}
