using Domain.Entities;

namespace Application.Dto
{
    public class ApplicationUserDto
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string phone { get; set; }

        public string Email { get; set; }
      
        public int type { get; set; }

        public string sap_no { get; set; }

        public string factory_location { get; set; }

        public string wc { get; set; }

        public string LastName { get; set; }
    }
}
