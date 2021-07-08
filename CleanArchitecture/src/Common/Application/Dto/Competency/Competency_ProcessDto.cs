using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Dto.Competency
{
    public class Competency_ProcessDto
    {

        public Competency_ProcessDto()
        {
            this.Categorys = new List<Competency_CategoryDto>();
            this.Dimensions = new List<Competency_SmeDimensionDto>();
        }

        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public IList<Competency_CategoryDto> Categorys { get; set; }
        public IList<Competency_SmeDimensionDto> Dimensions { get; set; }

        public decimal ProcessScore
        {
            get
            {
                return Math.Round(this.Categorys.Sum(x => x.CategoryScore * x.weight), 2);
            }
        }

        public decimal SmeScore
        {
            get
            {
                return Math.Round(this.Dimensions.Sum(x => x.DimensionScore), 2);
            }
        }
    }
}
