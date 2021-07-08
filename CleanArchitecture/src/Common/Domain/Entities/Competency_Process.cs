using System.Collections.Generic;

namespace Domain.Entities
{
    public class Competency_Process
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public IList<Competency_Category> Categorys { get; set; }
        public IList<Competency_SmeDimension> Dimensions { get; set; }

        public Competency_Process()
        {
            this.Categorys = new List<Competency_Category>();
            this.Dimensions = new List<Competency_SmeDimension>();
        }
    }
}
