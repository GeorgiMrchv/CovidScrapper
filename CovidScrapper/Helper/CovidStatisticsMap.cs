using CovidScrapper.Models;
using CsvHelper.Configuration;

namespace CovidScrapper
{
    public class CovidStatisticsMap : ClassMap<CovidStatistics>
    {
        public CovidStatisticsMap()
        {
            Map(m => m.Id).Name("Id");
            Map(m => m.Region).Name("Region");
            Map(m => m.Country).Name("Country");
            Map(m => m.TotalCases).Name("Total Cases");
            Map(m => m.TotalTests).Name("Total Tests");
            Map(m => m.ActiveCases).Name("Active Cases");
        }
    }
}
