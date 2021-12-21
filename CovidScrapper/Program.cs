using System.Collections.Generic;

namespace CovidScrapper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Scrapper scrapper = new Scrapper();
            string continentName = scrapper.TypeRegion();
            List<List<string>> extractedData = scrapper.GetData(continentName);
            var data = scrapper.PopulateDatabse(extractedData, continentName);
            scrapper.GenerateCSVFile(data, continentName);
            scrapper.GenerateConsoleTable(data);

        }
    }
}


