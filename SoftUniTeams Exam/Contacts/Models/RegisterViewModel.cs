﻿using System.ComponentModel.DataAnnotations;

namespace Contacts.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string UserName { get; set; } = null!;

        [Required]
        [StringLength(60, MinimumLength = 10)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
