using ManagementSystem.Models;

namespace ManagementSystem.Services.Employee
{
    public interface IEmployeeService
    {
        public Task CreateEmployeeAsync(EmployeeEntity newEmployee);
        public Task UpdateEmployeeAsync(EmployeeEntity employee);
        public Task RemoveEmployeeAsync(int id);
        public Task<EmployeeEntity?> GetEmployeeByIdAsync(int id);
        public Task<List<EmployeeEntity>> GetAllEmployeesAsync();
    }
}
