using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
   public class Permission : GenGUIDKey
    {
        public Permission(string Name) : base(Name)
        {
        }
    }
}
