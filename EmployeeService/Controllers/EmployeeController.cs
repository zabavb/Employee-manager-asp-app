using EmployeeLibrary.Models;
using EmployeeService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace EmployeeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDbContext _context;

        public EmployeeController(EmployeeDbContext context)
        {
            _context = context;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmployees()
        {
            if (_context.Employees == null)
                return NotFound();

            return await _context.Employees.ToListAsync();
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            if (_context.Employees == null)
                return NotFound();

            Employee? employee = await _context.Employees.FindAsync(id);

            if (employee == null)
                return NotFound();

            return employee;
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee model)
        {
            if (ModelState.IsValid)
            {
                if (_context.Employees == null)
                    return Problem("Entity set 'Employees' is null.");

                _context.Employees.Add(model);
                await _context.SaveChangesAsync();

                return Ok();
            }

            return BadRequest(ModelState);
        }

        // PUT: api/Employee/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee model)
        {
            if (id != model.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                Employee? employee = await _context.Employees.FindAsync(id);

                if (employee == null)
                    return NotFound();

                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName;
                employee.FullName = model.FirstName + " " + model.LastName;
                employee.Age = model.Age;

                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(model.Password!), Encoding.UTF8, "application/json");
                    client.BaseAddress = new Uri("https://localhost:7186/");
                    HttpResponseMessage response = await client.PostAsync("gateway/auth", content);

                    if (response.IsSuccessStatusCode)
                        employee.Password = await response.Content.ReadAsStringAsync();
                }

                employee.Role = model.Role;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(id))
                        return NotFound();
                    else
                        throw;
                }

                return Ok();
            }

            return BadRequest(ModelState);
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (_context.Employees == null)
                return NotFound();

            Employee? employee = await _context.Employees.FindAsync(id);

            if (employee == null)
                return NotFound();

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool EmployeeExists(int id)
        {
            return (_context.Employees?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
