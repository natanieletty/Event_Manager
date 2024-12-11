﻿using System.ComponentModel.DataAnnotations;

namespace Lesson24_Exam.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string?Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
