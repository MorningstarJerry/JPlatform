using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Dto.Competency
{
    public class Competency_CategoryDto
    {
        public Competency_CategoryDto()
        {
            this.Critiras = new List<Competency_CritiraDto>();
        }

        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public int processId { get; set; }
        public decimal weight { get; set; }
        public IList<Competency_CritiraDto> Critiras { get; set; }

        public decimal CategoryScore
        {
            get
            {
                decimal avgScore = Critiras.Average(emp => emp.score);
                return Math.Round(avgScore, 2);
            }
        }
    }
}
