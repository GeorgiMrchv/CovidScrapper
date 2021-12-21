using System.Collections.Generic;

namespace CovidScrapper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Scrapper scrapper = new Scrapper();
            string continentName = scrapper.TypeRegion();
            List<List<string>> headerList = scrapper.GetData(continentName);
            var data = scrapper.PopulateDatabse(headerList, continentName);
            scrapper.GenerateCSVFile(data, continentName);
            scrapper.GenerateConsoleTable(data);

        }
    }
}


