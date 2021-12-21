using ConsoleTables;
using CovidScrapper.Models;
using CsvHelper;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace CovidScrapper
{
    public class Scrapper
    {
        public string TypeRegion()
        {
            Console.WriteLine("Please type one of the following: All, Europe, North America, Asia, South America, Africa, Oceania");

            string[] regions = { "All", "Europe", "North America", "Asia", "South America", "Africa", "Oceania" };

            string continentName = Console.ReadLine();

            if (regions.Contains(continentName))
            {
                if (continentName == "All")
                {
                    continentName = "";
                }
            }
            else
            {
                throw new IndexOutOfRangeException("Wrong region has been typed.");
            }
            return continentName;

        }

        public List<List<string>> GetData(string continentName)
        {
            HtmlWeb web = new HtmlWeb();

            HtmlDocument doc = web.Load("https://www.worldometers.info/coronavirus/");

            var extractedData = doc.DocumentNode.SelectNodes("//table[@id='main_table_countries_today']//tbody//tr");

            List<List<string>> regionCollection = new List<List<string>>();

            foreach (var item in extractedData)
            {
                if (item.InnerHtml.Contains(continentName))
                {
                    regionCollection.Add(item.InnerText.Split("\n").Take(16).ToList());

                }

            }

            if (string.IsNullOrEmpty(continentName))
            {
                regionCollection.RemoveRange(0, 8);
                regionCollection.RemoveRange(223, 9);
                continentName = "All";
            }
            else
            {

                regionCollection.RemoveAt(0);
                regionCollection.RemoveAt(regionCollection.Count - 1);
            }

            return regionCollection;
        }

        public List<CovidStatistics> PopulateDatabse(List<List<string>> headerList, string continentName)
        {
            var data = new List<CovidStatistics>();

            for (int i = 0; i < headerList.Count; i++)
            {

                data.Add(new CovidStatistics
                {
                    Region = continentName == "" ? "All" : continentName,
                    Country = headerList[i].ElementAt(2),
                    TotalCases = headerList[i].ElementAt(3),
                    TotalTests = headerList[i].ElementAt(9),
                    ActiveCases = headerList[i].ElementAt(13)
                });

            }

            using (var _context = new ApplicationContext())
            {
                var getCurrentData = _context.CovidStatistics.Select(sl => sl);
                _context.CovidStatistics.RemoveRange(getCurrentData);
                _context.CovidStatistics.AddRange(data);
                _context.SaveChanges();
            }

            return data;
        }

        public void GenerateConsoleTable(List<CovidStatistics> data)
        {
            ConsoleTable
                .From<CovidStatistics>(data)
                .Configure(o => o.NumberAlignment = Alignment.Right)
                .Write(Format.Alternative);

            Console.ReadKey();
        }

        public void GenerateCSVFile(List<CovidStatistics> data, string continentName)
        {
            if (string.IsNullOrEmpty(continentName))
            {
                continentName = "All";
            }
            using (var writer = new StreamWriter(@"C:\Users\geomi\Desktop\blankfactor\export_" + continentName + "_(" + DateTime.Now.ToString("yy_MM_dd") + ").csv"))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<CovidStatisticsMap>();
                    csv.WriteField("sep=,", false);
                    csv.NextRecord();
                    csv.WriteRecords(data);

                }
            }
        }
    }
}

