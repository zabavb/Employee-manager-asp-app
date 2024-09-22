using EmployeeLibrary.Interfaces;
using EmployeeLibrary.ViewModels;

namespace EmployeeLibrary.Models
{
    public class Employee : IEmployee
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public int Age { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; } = Roles.Undefined.ToString();

        public Employee(LoginViewModel model)
        {
            FirstName = model.FirstName;
            LastName = model.LastName;
            FullName = model.FirstName + " " + model.LastName;
            Password = model.Password;
        }
        public Employee(ManageViewModel model)
        {
            FirstName = model.FirstName;
            LastName = model.LastName;
            FullName = model.FirstName + " " + model.LastName;
            Age = model.Age;
            Password = model.Password;
            Role = model.Role;
        }
        public Employee(int id, string firstName, string lastName, int age, string password, string role)
        {
            Id = id;
            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            FullName = firstName + " " + lastName;
            Age = age;
            Password = password.Trim();
            Role = role;
        }
        public Employee() { }
    }
}
