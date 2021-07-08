using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Dto.Competency
{
    public class Competency_SmeDimensionDto
    {
        public Competency_SmeDimensionDto()
        {
            smeDetails = new List<Competency_SmeDetailsDto>();
        }

        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public int processId { get; set; }
        public IList<Competency_SmeDetailsDto> smeDetails { get; set; }

        public decimal DimensionScore
        {
            get
            {
                decimal sumScore = smeDetails.Sum(emp => emp.score * emp.weight);
                return Math.Round(sumScore, 2);
            }
        }
    }
}
