using EmployeeWeb.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using System.Text.Json;
using Employee = EmployeeWeb.Models.Employee;
using System.Text;


namespace EmployeeWeb.Controllers
{
    public class EmployeeController : Controller
    {
        // Burası bizim sayfalarımı kontrol edecek controller

        private readonly IEmployeeService _service;
        private readonly string _ApiBase;
        private readonly IWebHostEnvironment _environmet; // 2 uygulama arası geliş/gidiş old için

        public EmployeeController(IEmployeeService service, IConfiguration configuration, IWebHostEnvironment environment)
        {
            _service = service;
            _ApiBase = configuration["APISection:BaseAddress"];
            _environmet = environment;

        }

        // Tüm kayıtları listeleme kısmı
        public async Task<IActionResult> Index(int page = 1) //
        {
            var employees = await _service.GetAll(); // Servisim üzerinden API tarafına bağlanıyorum veriyi alacağım.

            return View(await employees.ToPagedListAsync(page, 10)); // Çok kayıtlı veri dönüşünü sayfalama şeklinde gösterebilmek için bir Nuget paketi yüklenmesi gerekiyor (X.PagedList.Mvc.Core)
        }

        // View ekranında seçilen bir kaydın detay bilgilerine ulaşma
        public async Task<IActionResult> Details(int id)
        {
            var employeeDetails = await _service.GetById(id);

            if (employeeDetails == null) return View("NotFound");

            return View(employeeDetails);


        }

        // Burası create için boş form getiren view
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // Post edilen bilgilerin geldiği Create metodu
        // ????????????
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FName,LName,City")] Employee employee)
        {
            // Bu veri geliş işleminde HttpClient kütüphanesi kullanılarak http responsu gibi alınacak
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7087/EmployeeEF");

                // Oluşturulan nesneyi JSON formatına çevirvek gerekmekte. Bunun için de JSonSerializer ın Serialize metodunu kullanarak yapıyoruz.
                var serializeEmployee = JsonSerializer.Serialize(employee);

                // String içeriğini düzenliyoruz JSON a göre
                StringContent stringContent = new StringContent(serializeEmployee, Encoding.UTF8, "application/json");

                var postResult = client.PostAsync("api/EmployeeEF", stringContent).Result;

                if (postResult.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");

                }

            }

            return View(employee);
        }

        // Get Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var employeeDetails = await _service.GetById(id); // var mı/ yok mu

            if (employeeDetails == null) return View("NotFound");

            return View(employeeDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FName,LName,City")] Employee employee)
        {
            // Bu veri geliş işleminde HttpClient kütüphanesi kullanılarak http responsu gibi alınacak
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7087/EmployeeEF");

                // Oluşturulan nesneyi JSON formatına çevirvek gerekmekte. Bunun için de JSonSerializer ın Serialize metodunu kullanarak yapıyoruz.
                var serializeEmployee = JsonSerializer.Serialize(employee);

                // String içeriğini düzenliyoruz JSON a göre
                StringContent stringContent = new StringContent(serializeEmployee, Encoding.UTF8, "application/json");

                var putResult = client.PutAsync("api/EmployeeEF/" + id, stringContent).Result;

                if (putResult.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");

                }
            }

            return View(employee);

        }

        // Delete
        public async Task<IActionResult> Delete(int id)
        {
            var employeeDetails = await _service.GetById(id); // var mı/ yok mu

            if (employeeDetails == null) return View("NotFound");

            return View(employeeDetails);
        }

        //Delete Post
        [HttpPost]
        public async Task<IActionResult>
    }
}
