using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementSystem.Models.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required")]
        public string Login { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Ativo")]
        public bool Status { get; set; }
    }
}
