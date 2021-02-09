using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Site : GenGUIDKey
    {
        public Site(string Name) : base(Name)
        {
        }
    }
}