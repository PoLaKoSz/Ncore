using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PoLaKoSz.Ncore.Models;

namespace PoLaKoSz.Ncore.Samples.ConsoleApp
{
    public class Program
    {
        private static readonly JsonSerializerSettings jsonSerializerSettings;

        static Program()
        {
            jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
            jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Error;
        }

        public static void Main(string[] args)
        {
            UserConfig userConfig = LoadCredentialsFromFile();

            NcoreClient nCore = GetAuthenticatedClientFor(userConfig)
                .ConfigureAwait(false).GetAwaiter().GetResult();

            Thread.Sleep(TimeSpan.FromSeconds(2));

            ISearchResultContainer resultContainer = nCore.Search.List()
                .GetAwaiter().GetResult();

            Thread.Sleep(TimeSpan.FromSeconds(2));

            ISearchResultContainer innaResults = nCore.Search.For("Inna")
                .ConfigureAwait(false).GetAwaiter().GetResult();

            IEnumerable<HitAndRunTorrent> hitAndRunTorrents = nCore.HitAndRuns.List()
                .ConfigureAwait(false).GetAwaiter().GetResult();

            Torrent torrent = nCore.Torrent.Get(1683491)
                .ConfigureAwait(false).GetAwaiter().GetResult();

            FileStream destination = new FileStream($"{torrent.UploadName}.torrent", FileMode.CreateNew);

            torrent.DownloadAsFileTo(destination)
                .ConfigureAwait(false).GetAwaiter().GetResult();

            Console.Read();
        }

        private static UserConfig LoadCredentialsFromFile()
        {
            DirectoryInfo rootFolder = new DirectoryInfo(Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "PoLáKoSz's Corp.",
                "nCore"));
            rootFolder.Create();

            FileInfo configFile = new FileInfo(Path.Combine(rootFolder.FullName, "credentials.json"));

            if (!configFile.Exists)
            {
                UserConfig userConfig = CreateUserConfig();
                string json = JsonConvert.SerializeObject(userConfig, jsonSerializerSettings);
                File.WriteAllText(configFile.FullName, json);
                return userConfig;
            }

            try
            {
                string json = File.ReadAllText(configFile.FullName);
                return JsonConvert.DeserializeObject<UserConfigDao>(json, jsonSerializerSettings);
            }
            catch (JsonSerializationException)
            {
                Console.WriteLine("Saved credentials file corrupted! Recreating ...");
                return CreateUserConfig();
            }
        }

        private static UserConfig CreateUserConfig()
        {
            Console.Write("PHPSESSID = ");
            string phpSessionID = Console.ReadLine();
            Console.Write("pass = ");
            string password = Console.ReadLine();
            Console.Write("nick = ");
            string nickName = Console.ReadLine();

            return new UserConfigDao(nickName, password, phpSessionID);
        }

        private static async Task<NcoreClient> GetAuthenticatedClientFor(UserConfig userConfig)
        {
            NcoreClient client = new NcoreClient();
            await client.Login.AuthenticateWith(userConfig).ConfigureAwait(false);
            return client;
        }
    }
}
