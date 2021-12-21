using System.ComponentModel.DataAnnotations;

namespace CovidScrapper.Models
{
    public  class CovidStatistics
    {
        [Key]
        public int Id { get; set; }

        public string Region { get; set; }

        public string Country { get; set; }

        public string TotalCases { get; set; }

        public string TotalTests { get; set; }

        public string ActiveCases { get; set; }
    }
}
