using EmployeeLibrary.Interfaces;
using EmployeeLibrary.Models;
using System.ComponentModel.DataAnnotations;

namespace EmployeeLibrary.ViewModels
{
    public class ManageViewModel : IEmployee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First name")]
        [StringLength(32, MinimumLength = 2, ErrorMessage = "First name must be in range between 2 and 32 characters.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "Last name")]
        [StringLength(32, MinimumLength = 2, ErrorMessage = "Last name must be in range between 2 and 32 characters.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        [Range(18, 80, ErrorMessage = "Age must be in range between 18 and 80.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password, ErrorMessage = "Incorrect password.")]
        [StringLength(32, MinimumLength = 3, ErrorMessage = "Password must be in range between 3 and 32 characters.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required.")]
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password, ErrorMessage = "Incorrect confirmation password.")]
        [Compare("Password", ErrorMessage = "Passwords do not much.")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public string? Role { get; set; } = Roles.Undefined.ToString();

        public ManageViewModel(Employee model)
        {
            Id = model.Id;
            FirstName = model.FirstName;
            LastName = model.LastName;
            Age = model.Age;
            Password = model.Password;
            Role = model.Role;
        }
        public ManageViewModel(int id, string firstName, string lastName, int age, string password, string role)
        {
            Id = id;
            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            Age = age;
            Password = password.Trim();
            Role = role;
        }
        public ManageViewModel() { }
    }
}
