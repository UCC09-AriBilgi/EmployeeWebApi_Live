using EmployeeWeb.Models;
using System.Collections.Generic;

namespace EmployeeWeb.Services.Interfaces
{
    public interface IEmployeeService
    {
        // Db üzerinden tüm kayıtları getirecek olan metot/task
        Task<IEnumerable<Employee>> GetAll();

        // Db üzerinden istenen bir kayıdı olan metot/task
        Task<Employee> GetById(int id);

    }
}
