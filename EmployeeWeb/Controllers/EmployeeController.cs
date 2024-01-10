using EmployeeWeb.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using System.Text.Json;
using Employee = EmployeeWeb.Models.Employee;


namespace EmployeeWeb.Controllers
{
    public class EmployeeController : Controller
    {
        // Burası bizim sayfalarımı kontrol edecek controller

        private readonly IEmployeeService _service;
        private readonly string _ApiBase;
        private readonly IWebHostEnvironment _environmet; // 2 uygulama arası geliş/gidiş old için

        public EmployeeController(IEmployeeService service , IConfiguration configuration, IWebHostEnvironment environment)
        {
            _service = service;
            _ApiBase = configuration["APISection:BaseAddress"];
            _environmet = environment;
                
        }


        public async Task<IActionResult> Index(int page=1) //
        {
            var employees=await _service.GetAll(); // Servisim üzerinden API tarafına bağlanıyorum veriyi alacağım.

            return View(await employees.ToPagedListAsync(page,10)); // Çok kayıtlı veri dönüşünü sayfalama şeklinde gösterebilmek için bir Nuget paketi yüklenmesi gerekiyor (X.PagedList.Mvc.Core)
        }
    }
}
