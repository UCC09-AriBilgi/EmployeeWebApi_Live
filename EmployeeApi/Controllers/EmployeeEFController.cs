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


        // GET api/<EmployeeEFController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<EmployeeEFController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EmployeeEFController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmployeeEFController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
