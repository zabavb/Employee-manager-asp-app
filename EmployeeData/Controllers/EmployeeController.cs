using EmployeeLibrary.Models;
using EmployeeLibrary.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace EmployeeData.Controllers
{
    public class EmployeeController : Controller
    {
        [HttpGet]
        public IActionResult Login(Status? status)
        {
            if (!string.IsNullOrEmpty(status!.Message))
                ViewBag.Status = status;

            return View(new LoginViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            Employee? employee = (await GetEmployeesAsync()).FirstOrDefault(e => e.FullName!.Equals(new Employee(model).FullName));

            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(model.Password!), Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri("https://localhost:7186/");
                HttpResponseMessage response = await client.PostAsync("gateway/auth", content);

                if (response.IsSuccessStatusCode)
                    model.Password = await response.Content.ReadAsStringAsync();
            }

            if (employee == null || !employee.Password!.Equals(model.Password))
            {
                ViewBag.Status = new Status(false, "Incorrect full name or password.");
                return View(model);
            }


            using (HttpClient client = new HttpClient())
            {
                List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, employee.FullName!),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, employee.Role!),
                    };

                var claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(2)
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                string? fullName = claimsIdentity.FindFirst(ClaimsIdentity.DefaultNameClaimType)?.Value;

                return RedirectToAction("MyProfile", new { fullName });
            }
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await AuthenticationHttpContextExtensions.SignOutAsync(HttpContext, CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", new Status(true, "Logout successful."));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MyProfile(Status status, string fullName)
        {
            if (!string.IsNullOrEmpty(status.Message))
            {
                ViewBag.Status = status;
                return View(new Employee());
            }

            Employee? employee = (await GetEmployeesAsync()).FirstOrDefault(e => e.FullName!.Equals(fullName));
            if (employee == null)
            {
                ViewBag.Status = new Status(false, "Employee data is missing.");
                return View(new Employee());
            }

            return View(employee);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Employees(Status status)
        {
            if (!string.IsNullOrEmpty(status.Message))
                ViewBag.Status = status;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7186/");
                HttpResponseMessage response = await client.GetAsync("gateway/employee");

                if (response.IsSuccessStatusCode)
                    return View(JsonConvert.DeserializeObject<IEnumerable<Employee>>(await response.Content.ReadAsStringAsync()));
                else
                    return View();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Post()
        {
            ViewBag.IsPost = true;
            return View("Manage", new ManageViewModel());
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(ManageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.IsPost = true;
                return View("Manage", model);
            }

            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(model.Password!), Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri("https://localhost:7186/");
                HttpResponseMessage response = await client.PostAsync("gateway/auth", content);

                if (response.IsSuccessStatusCode)
                    model.Password = await response.Content.ReadAsStringAsync();
            }

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7186/");
                var content = new StringContent(JsonConvert.SerializeObject(new Employee(model)), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("gateway/employee", content);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Employees", new Status(true, $"The employee '{model.LastName}' has been successfully registered."));
                else
                {
                    ViewBag.IsPost = true;
                    return View("Manage");
                }
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Put(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7186/");
                HttpResponseMessage response = await client.GetAsync($"gateway/employee/{id}");

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.IsPost = false;
                    Employee? employee = JsonConvert.DeserializeObject<Employee>(await response.Content.ReadAsStringAsync());
                    return View("Manage", new ManageViewModel(employee));
                }
                else
                    return View("Employees", new Status(false, "Employee data is missing."));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Put(ManageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.IsPost = false;
                return View("Manage", model);
            }

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7186/");
                StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"gateway/employee/{model.Id}", content);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Employees", new Status(true, $"The employee '{model.LastName}' has been successfully updated."));
                else
                {
                    ViewBag.IsPost = false;
                    return View("Manage", model);
                }
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7186/");
                HttpResponseMessage response = await client.DeleteAsync($"gateway/employee/{id}");

                return RedirectToAction("Employees", response.IsSuccessStatusCode ?
                    ViewBag.Status = new Status(true, "Employee has been successfully deleted.") :
                    ViewBag.Status = new Status(false, "An issue occurred during the deletion of the employee."));
            }
        }

        [NonAction]
        public async Task<List<Employee>> GetEmployeesAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7186/");
                HttpResponseMessage response = await client.GetAsync("gateway/employee");

                return response.IsSuccessStatusCode ?
                    JsonConvert.DeserializeObject<List<Employee>>(await response.Content.ReadAsStringAsync())! :
                    new List<Employee>();
            }
        }
    }
}
