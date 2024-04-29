using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementSystem.Models
{
    public abstract class DefaultEntity
    {
        [Column("PkId")]
        public int Id { get; set; }

        [Column("TgActive")]
        public bool Status { get; set; }
    }
}
