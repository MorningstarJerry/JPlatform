using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Department : GenGUIDKey
    {
        public Department(string Name) : base(Name)
        {
        }
    }
}
