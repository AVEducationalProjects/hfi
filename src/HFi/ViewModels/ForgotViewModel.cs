using System.ComponentModel.DataAnnotations;

namespace HFi.Models
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}