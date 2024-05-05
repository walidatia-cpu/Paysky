using EmploymentSystem.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmploymentSystem.Core
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        [ForeignKey(nameof(CreationUser))]
        public string CreationBy { get; set; }
        public virtual ApplicationUser CreationUser { get; set; }

        [ForeignKey(nameof(ModificationUser))]
        public string? LastModificationBy { get; set; }
        public virtual ApplicationUser ModificationUser { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastModificationDate { get; set; }

    }
}
