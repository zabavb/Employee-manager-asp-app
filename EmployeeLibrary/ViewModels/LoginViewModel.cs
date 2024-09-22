using EmployeeLibrary.Interfaces;
using EmployeeLibrary.Models;
using System.ComponentModel.DataAnnotations;

namespace EmployeeLibrary.ViewModels
{
    public class LoginViewModel : IEmployee
    {
        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First name")]
        [StringLength(32, MinimumLength = 2, ErrorMessage = "First name must be in range between 2 and 32 characters")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last name")]
        [StringLength(32, MinimumLength = 2, ErrorMessage = "Last name must be in range between 2 and 32 characters")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password, ErrorMessage = "Incorrect password")]
        [StringLength(32, MinimumLength = 3, ErrorMessage = "Password must be in range between 3 and 32 characters")]
        public string? Password { get; set; }

        public LoginViewModel(Employee model)
        {
            FirstName = model.FirstName;
            LastName = model.LastName;
            Password = model.Password;
        }
        public LoginViewModel(string firstName, string lastName, string password)
        {
            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            Password = password.Trim();
        }
        public LoginViewModel() { }
    }
}
