using System.Collections.Generic;

namespace Domain.Entities
{
    public class Competency_Category
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public int processId { get; set; }
        public Competency_Process process { get; set; }

        public decimal weight { get; set; }
        public IList<Competency_Critira> Critiras { get; set; }

        public Competency_Category()
        {
            this.Critiras = new List<Competency_Critira>();
        }
    }
}
