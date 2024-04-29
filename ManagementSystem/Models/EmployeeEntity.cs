using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ManagementSystem.Models
{
    public class EmployeeEntity : DefaultEntity
    {
        [Column("DsName")]
        public string Name { get; set; }

        [Column("FkUser")]
        public int UserId { get; set; }
        
        [Column("FkUnity")]
        public int UnityId { get; set; }

        [ForeignKey("UserId")]
        public UserEntity User { get; set; }

        [ForeignKey("UnityId")]
        public UnityEntity Unity { get; set; }

    }
}
