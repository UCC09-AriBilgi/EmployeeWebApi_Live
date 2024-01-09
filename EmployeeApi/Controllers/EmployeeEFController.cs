using EmployeeApi.Data;
using EmployeeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // Buranın bir API giriş noktası olduğunu belirtiyor
    public class EmployeeEFController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeeEFController(AppDbContext context)
        {
                _context = context;
        }

        // 1. Employee kayıtlarını JSON formatı şeklinde DB tarafından getirme
        // GET: api/<EmployeeEFController>
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmployees()
        {
            return Ok(await _context.Employees.ToListAsync());
        }

        // 2. Employee kayıtlarından Id si belirlenmiş kayıdı getirme
        // GET api/<EmployeeEFController>/5
        [HttpGet("{id}")] // Yine HttpGet annotate i kullanarak ve buna bir parametre (id) geçişi yapılarak sadece istenen kaydın getirilmesi
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var employee = await _context.Employees.FindAsync(id); // üzerine gelen id bilgisine göre Find işlemi

            if (employee == null) // eğer employee içeriği oluşmamışsa
            {
                return BadRequest("Çalışan kaydı bulunamadı....");
            }

            return Ok(employee); // 200 kodu-başarılı 

        }

        // 3. Employee tablosu üzerinde yeni bir kayıt işlemi
        // POST api/<EmployeeEFController>
        [HttpPost]
        public async Task<ActionResult<List<Employee>>> AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);

            await _context.SaveChangesAsync();

            return Ok(await _context.Employees.ToListAsync());

        }
        // 4. Employe tablosu üzerinde bir kayıdın güncelleme işlemi
        // PUT api/<EmployeeEFController>/5
        [HttpPut("{id}")] // HttpPut annonate i ve id parameresi
        public async Task<ActionResult<List<Employee>>> UpdateEmployee(Employee request)
        {
            // üzerine gelen bilgi setinden ilgili id yi ara bul
            var dbemployee = await _context.Employees.FindAsync(request.Id);

            if (dbemployee == null) // Oluşmamışsa - bulamamışsa
            {
                return BadRequest("Böyle bir çalışan bulunamadı....");

            }

            dbemployee.LName = request.LName;
            dbemployee.FName= request.FName;
            dbemployee.City= request.City;

            await _context.SaveChangesAsync();

            return Ok(await _context.Employees.ToListAsync());

        }

        // 5. Employee tablosu üzerinde delete işlemi
        // DELETE api/<EmployeeEFController>/5
        [HttpDelete("{id}")] // HttpDelete annotate ve bir id bilgisine ihtiyaç var..
        public async Task<ActionResult<List<Employee>>> DeleteEmployee(int id) 
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return BadRequest("Böyle bir çalışanınız yoktur...Atma...");

            }

            _context.Employees.Remove(employee);

            await _context.SaveChangesAsync();

            return Ok(await _context.Employees.ToListAsync());

        }
    }
}
