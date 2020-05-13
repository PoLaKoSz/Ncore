using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace PoLaKoSz.Ncore.Models
{
    /// <summary>
    /// Class to hold detailed infromation about an nCore torrent.
    /// </summary>
    public class Torrent
    {
        private readonly HttpClient _client;

        /// <summary>
        /// Initialize a new instance.
        /// </summary>
        /// <param name="id">The unique ID given by nCore.</param>
        /// <param name="uploadName">The name given by the uploader.</param>
        /// <param name="uploadDate">The local time when this torrent was uploaded.</param>
        /// <param name="downloadURL">The absolute path to the .torrent file.</param>
        /// <param name="client">The <see cref="HttpClient" /> to download the .torrent file.</param>
        public Torrent(
            int id,
            string uploadName,
            DateTime uploadDate,
            Uri downloadURL,
            HttpClient client)
        {
            UploadDate = uploadDate;
            ID = id;
            UploadName = uploadName;
            DownloadURL = downloadURL;
            _client = client;
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
        /// Gets when this torrent was uploaded to nCore (local time).
        /// </summary>
        public DateTime UploadDate { get; }

        /// <summary>
        /// Gets the absolute path for the .torrent file.
        /// </summary>
        public Uri DownloadURL { get; }

        /// <summary>
        /// Downloads this torrent .torrent file.
        /// </summary>
        /// <param name="destination">The path where the file should be placed.</param>
        /// <returns>Async <see cref="Task" />.</returns>
        public async Task DownloadAsFileTo(FileStream destination)
        {
            var response = await _client.GetAsync(DownloadURL).ConfigureAwait(false);

            await response.Content.CopyToAsync(destination).ConfigureAwait(false);
        }
    }
}
