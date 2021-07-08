using Domain.Common;

namespace Domain.Entities
{
    public class Department : GenGUIDKey
    {
        public Department(string Name) : base(Name)
        {
        }
    }
}
