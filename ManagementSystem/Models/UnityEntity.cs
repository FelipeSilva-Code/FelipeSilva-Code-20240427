using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementSystem.Models
{
    public class UnityEntity : DefaultEntity
    {
        [Column("DsName")]
        public string Name { get; set; }

        [Column("DsCode")]
        public string Code { get; set; }

        public List<EmployeeEntity> Employees { get; set; }
    }
}
