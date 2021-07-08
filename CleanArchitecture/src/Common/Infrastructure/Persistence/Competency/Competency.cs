using System.Collections.Generic;

namespace Infrastructure.Persistence.Competency
{
    public class Competency
    {
        public List<CompetencyCategoryJson> datas { get; set; }
        public List<CompetencySmeJson> sme { get; set; }

        public Competency()
        {
            datas = new List<CompetencyCategoryJson>();
            sme = new List<CompetencySmeJson>();
        }
    }

    public class CompetencyCategoryJson
    {
        public string category { get; set; }
        public string critira { get; set; }
        public string gradea { get; set; }
        public string gradea_url { get; set; }
        public string gradeb { get; set; }
        public string gradeb_url { get; set; }
        public string gradec { get; set; }
        public string gradec_url { get; set; }
        public string graded { get; set; }
        public string graded_url { get; set; }
        public string current { get; set; }
        public int score { get; set; }
    }

    public class CompetencySmeJson
    {
        public string Dimension { get; set; }
        public string Competency { get; set; }
        public string Code { get; set; }
        public string Definition { get; set; }
        public string Level0 { get; set; }
        public string Level1 { get; set; }
        public string Level2 { get; set; }
        public string Level3 { get; set; }
        public decimal Weight { get; set; }
        public decimal Score { get; set; }
    }
}
