using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Competency_SmeDimension : AuditableEntity
    {
        public Competency_SmeDimension()
        {
            smeDetails = new List<Competency_SmeDetails>();
        }

        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }

        public int processId { get; set; }
        public Competency_Process process { get; set; }

        public IList<Competency_SmeDetails> smeDetails { get; set; }

    }
}
