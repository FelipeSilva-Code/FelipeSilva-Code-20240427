using System.ComponentModel.DataAnnotations;

namespace ManagementSystem.Models.ViewModels
{
    public class UnityViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Código")]
        [Required(ErrorMessage = "{0} required")]
        public string Code { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Ativo")]
        public bool Status { get; set; }

        [Display(Name = "Colaboradores")]
        public List<EmployeeViewModel> Employees { get; set; } = new List<EmployeeViewModel>();
    }
}
