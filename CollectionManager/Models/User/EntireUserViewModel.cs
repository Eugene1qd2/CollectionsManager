using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CollectionManager.Models.User
{
    public class EntireUserViewModel
    {
        [Display(Name = "Id")]
        public string Id { get; set; }
        [Display(Name = "User name")]
        public string UserName { get; set; }
        [Display(Name = "User email")]
        public string UserEmail { get; set; }
        [Display(Name = "User phone")]
        public string UserPhone { get; set; }
        [Display(Name = "User role")]
        public string UserRole { get; set; }
        [Display(Name = "User status")]
        public bool IsBlocked { get; set; }

        public EntireUserViewModel(IdentityUser item, string? role)
        {
            Id = item.Id;
            UserName = item.UserName;
            UserEmail = item.Email;
            UserPhone = item.PhoneNumber;
            UserRole = role != null ? role : "user";
            IsBlocked = item.LockoutEnd>DateTime.Now;
        }
        public EntireUserViewModel() 
        {
        }
    }
}
