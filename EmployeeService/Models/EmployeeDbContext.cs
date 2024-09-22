using EmployeeLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace EmployeeService.Models
{
    public class EmployeeDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string pass1 = "1234", pass2 = "4321", pass3 = "1234";

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7186/");

                {
                    var content = new StringContent(JsonConvert.SerializeObject(pass1), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync("gateway/auth", content).Result;
                    if (response.IsSuccessStatusCode)
                        pass1 = response.Content.ReadAsStringAsync().Result;
                }

                {
                    var content = new StringContent(JsonConvert.SerializeObject(pass2), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync("gateway/auth", content).Result;
                    if (response.IsSuccessStatusCode)
                        pass2 = response.Content.ReadAsStringAsync().Result;
                }
                {
                    var content = new StringContent(JsonConvert.SerializeObject(pass3), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync("gateway/auth", content).Result;
                    if (response.IsSuccessStatusCode)
                        pass3 = response.Content.ReadAsStringAsync().Result;
                }
            }

            modelBuilder.Entity<Employee>().HasData(
                new Employee(1, "John", "Smith", 18, pass1, Roles.Admin.ToString()),
                new Employee(2, "Oleg", "Olegovich", 23, pass2, Roles.Engeneer.ToString()),
                new Employee(3, "Optimus", "Prime", 45, pass3, Roles.Designer.ToString())
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}