using System;

namespace PoLaKoSz.Ncore.Models
{
    /// <summary>
    /// Class for each Hit'n'Run torrent.
    /// </summary>
    public class HitAndRunTorrent
    {
        /// <summary>
        /// Initialize a new instance.
        /// </summary>
        /// <param name="id">The unique ID given by nCore.</param>
        /// <param name="uploadName">The name given by the uploader.</param>
        /// <param name="hitAndRunEnd">The local time when this torrent will disappear
        /// from the Hit'n'Run menu.</param>
        /// <param name="ratio">The current share ratio.</param>
        public HitAndRunTorrent(
            int id,
            string uploadName,
            DateTime hitAndRunEnd,
            float ratio)
        {
            ID = id;
            UploadName = uploadName;
            HitAndRunEnd = hitAndRunEnd;
            Ratio = ratio;
        }

        /// <summary>
        /// Gets the torrent unique ID given by nCore.
        /// </summary>
        public int ID { get; }

        /// <summary>
        /// Gets the uploadname given by the uploader.
        /// </summary>
        public string UploadName { get; }

        /// <summary>
        /// Gets the local time when this torrent will be vanished from
        /// the Hit'n'Run menu.
        /// </summary>
        public DateTime HitAndRunEnd { get; }

        /// <summary>
        /// Gets the torrent current share ratio.
        /// </summary>
        public float Ratio { get; }
    }
}
