﻿namespace Bloggable.Web.Models.Account.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Bloggable.Common.Constants;

    public class RegisterViewModel
    {
        [Required]
        [StringLength(
            UserValidationConstants.UserNameMaxLength,
            MinimumLength = UserValidationConstants.UserNameMinLength,
            ErrorMessage = "The {0} must be at least {2} characters long.")]
        [RegularExpression(
            UserValidationConstants.UserNameRegEx, 
            ErrorMessage = "Invalid username. Please, use only latin characters, digits and symbols: ._")]
        [Remote("IsAvailableUserName", "UserValidation", ErrorMessage = "{0} is already taken...")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Remote("IsAvailableEmail", "UserValidation", ErrorMessage = "{0} is already taken...")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(
            UserValidationConstants.PasswordMaxLength,
            MinimumLength = UserValidationConstants.PasswordMinLength,
            ErrorMessage = "The {0} must be at least {2} characters long.")]
        [DataType(DataType.Password)]
        [AllowHtml]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [AllowHtml]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}