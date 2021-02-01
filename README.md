# PoLaKoSz.Ncore

[https://ncore.pro](https://ncore.pro) is the largest hungarian torrent side. This .NET Core 2.1 library helps to access its content
(Torrent details, user Hit'n'Runs, Search for Torrent).

## Install

via [NuGet](https://www.nuget.org/packages/PoLaKoSz.Ncore/)

``` sh
$ dotnet add package PoLaKoSz.NCore
```

## Quick start

``` c#
using PoLaKoSz.Ncore;
using PoLaKoSz.Ncore.Models;
using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            NcoreClient nCore = new NcoreClient();

            // parameter values should be extracted from the cookies after logged in from a
            // browser:
            //  constructor parameter     cookie name
            //       nickName                 nick
            //       password                 pass
            //     phpSessionID            PHPSESSID
            nCore.Login.AuthenticateWith(new UserConfig(...))
                .GetAwaiter().GetResult();

            ISearchResultContainer resultContainer = nCore.Search.List()
                .GetAwaiter().GetResult();

            ISearchResultContainer matrixResultContainer = nCore.Search.For("The Matrix")
                .GetAwaiter().GetResult();

            IEnumerable<DownloadedTorrent> hitAndRuns = nCore.HitAndRuns.List()
                .GetAwaiter().GetResult();

            Torrent firstTorrent = nCore.Torrent.Get(2)
                .GetAwaiter().GetResult();

            Console.Read();
        }
    }
}
```
