using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementSystem.Models.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Ativo")]
        public bool Status { get; set; }

        public int? IdUser { get; set; }

        public int? IdUnity { get; set; }

        public UserViewModel? User { get; set; }

        public UnityViewModel? Unity { get; set; }

        [NotMapped]
        public List<UserViewModel> AllUsers { get; set; } = new List<UserViewModel>();

        [NotMapped]
        public List<UnityViewModel> AllUnities { get; set; } = new List<UnityViewModel>();
    }
}
