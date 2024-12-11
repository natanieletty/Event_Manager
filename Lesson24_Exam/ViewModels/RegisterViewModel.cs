using System.ComponentModel.DataAnnotations;

namespace Lesson24_Exam.ViewModels
{
    public class RegisterViewModel
    {
		[Required]
		public string? Login { get; set; }
		[Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }
    }
}
