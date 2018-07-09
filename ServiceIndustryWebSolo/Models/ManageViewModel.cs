using Microsoft.AspNet.Identity;
using System;
using Models.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ServiceIndustryWebSolo.Models
{
    public class IndexViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Photo { get; set; }
        public bool EmailConfirmed { get; set; }
        
        public List<Executor> Executors { get; set; }

        public static IndexViewModel CustomerToIVM(Customer customer)
        {
            IndexViewModel model = new IndexViewModel
            {
                Name = customer.Name,
                Surname = customer.Surname,
                UserName = customer.UserName,
                Phone = customer.PhoneNumber,
                Email = customer.Email,
                EmailConfirmed = customer.EmailConfirmed,
                Photo = customer.Photo,
                Executors = customer.Executors.ToList()
            };
            return model;
        }

    }
    
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The value {0} must contain at least the following characters: {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}