using EmployeeWeb.Helpers;
using EmployeeWeb.Models;
using EmployeeWeb.Services.Interfaces;

namespace EmployeeWeb.Services
{
    public class EmployeeService : IEmployeeService
    {
        // Bu MVC uygulaması client/server mimarisi yapısında client görevi görecek. O yüzden bazı gerekli kütüphaneleri tanımlamak gerekiyor.

        private readonly HttpClient _client; // müşteri
        private readonly string _Apibase; // https://localhost:7087/

        public EmployeeService(HttpClient client,IConfiguration configuration)
        {
            _client = client;
            _Apibase = configuration["APISection:BaseAddress"]; // appsettings.json dan gelen bilgiler
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            // burada bir weblinki oluşturuyoruz.
            string ApiPath = _Apibase + "api/EmployeeEF";

            var response = await _client.GetAsync(ApiPath);

            return await response.ReadContentAsync<List<Employee>>(); // karsı taraftan gelen bilgiyi okuyacak metot.******
        }

        public Task<Employee> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
