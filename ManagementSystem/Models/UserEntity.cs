using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ManagementSystem.Models
{
    public class UserEntity : DefaultEntity
    {
        [Column("DsLogin")]
        public string Login { get; set; }

        [Column("DsPassword")]
        public string Password { get; set; }

        public byte[] Salt { get; set; }

        public EmployeeEntity Employee { get; set; }
    }
}
