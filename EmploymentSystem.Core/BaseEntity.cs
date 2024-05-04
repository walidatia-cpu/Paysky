using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Core
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public string? CreationBy { get; set; }
        public DateTime CreationDate { get; set; }
        public string? LastModificationBy { get; set; }
        public DateTime? LastModificationDate { get; set; }

    }
}
