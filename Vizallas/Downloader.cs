using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace Vizallas
{
    public class Downloader
    {
        public void Download()
        {
            var client = new HttpClient();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var encoding = Encoding.GetEncoding("iso-8859-1");
            encoding = Encoding.GetEncoding("windows-1252");
            //encoding = Encoding.UTF8;

            for (int i = 2002; i < 2020; i++)
            {
                Console.WriteLine(i);

                var path = $"../../../Data/Budapest_{i}.html";

                if (File.Exists(path))
                {
                    Console.WriteLine("Already downloaded");
                    continue;
                }

                var url = $"https://eumet.hu/prx-hydroinfo/Html/archivum/tb{i}/tb442027.htm";

                Console.WriteLine($"Downloading: {url}");

                var html = client.GetStringAsync(url).Result;

                File.WriteAllText(path, html, encoding);

                Thread.Sleep(5000);
            }
        }
    }
}
