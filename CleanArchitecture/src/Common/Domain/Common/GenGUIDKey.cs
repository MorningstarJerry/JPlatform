using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Common
{
    public class GenGUIDKey : AuditableEntity
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public GenGUIDKey()
        {
            Id = Guid.NewGuid().ToString();
        }

        public GenGUIDKey(string code)
        {
            Id = Guid.NewGuid().ToString();
            Name = code;
        }
    }
}