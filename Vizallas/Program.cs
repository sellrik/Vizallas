using System;

namespace Vizallas
{
    class Program
    {
        static void Main(string[] args)
        {
            var downloader = new Downloader();
            downloader.Download();

            var parser = new Parser();
            parser.Parse();
        }
    }
}
