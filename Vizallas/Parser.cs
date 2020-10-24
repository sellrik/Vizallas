using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Vizallas
{
    public class Parser
    {
        public void Parse()
        {
            var result = new List<Data>();

            var files = Directory.GetFiles("../../../Data/");
            foreach (var file in files)
            {
                FileInfo fileInfo = new FileInfo(file);

                var year = int.Parse(fileInfo.Name.Substring(9, 4));

                using (StreamReader sr = new StreamReader(file))
                {
                    var lineCounter = 0;

                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();

                        if (lineCounter > 13 && lineCounter < 45)
                        {
                            if (line == string.Empty)
                            {
                                continue;
                            }

                            Console.WriteLine(line);

                            var day = int.Parse(line.Substring(0, 4).Trim());
                            Console.WriteLine(day);

                            //var content = line.Substring(9, line.Length - 9-1);

                            for (int i = 0; i < 12; i++)
                            {
                                var measurement = line.Substring(6 + i*6, 6).Trim();

                                if (string.IsNullOrEmpty(measurement))
                                {
                                    continue;
                                }

                                Console.WriteLine(measurement);

                                var data = new Data
                                {
                                    Date = new DateTime(year, i + 1, day),
                                    Measurement = int.Parse(measurement)
                                };

                                result.Add(data);
                            }
                        }

                        lineCounter++;
                    }
                }
            }

            WriteToCsv(result);
        }
    
        void WriteToCsv(List<Data> data)
        {
            var ordered = data.OrderBy(i => i.Date);

            using (StreamWriter sw = new StreamWriter("../../../data.csv"))
            {
                sw.WriteLine("Date;Measurement");

                foreach (var d in ordered)
                {
                    sw.WriteLine($"{d.Date.ToString("yyyy.MM.dd")};{d.Measurement}");
                }
            }
        }
    }
}
