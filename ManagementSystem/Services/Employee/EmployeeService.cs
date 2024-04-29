using ManagementSystem.Data;
using ManagementSystem.Models;
using ManagementSystem.Services.Exceptions;
using ManagementSystem.Services.Unity;
using ManagementSystem.Services.User;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Services.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ManagementSystemDbContext _context;
        private readonly IUnityService _unityService;
        private readonly IUserService _userService;

        public EmployeeService(ManagementSystemDbContext context, IUnityService unityService, IUserService user)
        {
            _context = context;
            _unityService = unityService;
            _userService = user;
        }

        public async Task CreateEmployeeAsync(EmployeeEntity newEmployee)
        {
            var unity = await _unityService.GetUnityByIdAsync(newEmployee.UnityId);

            if(unity == null)
                throw new NotFoundException("Unity not found");

            if (!unity.Status)
                throw new BusinessException("Unity is inactive");

            if(_context.Employee.Any(x => x.UserId == newEmployee.UserId))
                throw new EqualException("This login is already registered to another employee");

            var user = await _userService.GetUserByIdAsync(newEmployee.UserId);
            
            if (unity == null)
                throw new NotFoundException("User not found");


            _context.Add(newEmployee);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveEmployeeAsync(int id)
        {
            var employee = await this.GetEmployeeByIdAsync(id);
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<List<EmployeeEntity>> GetAllEmployeesAsync()
        {
            return await _context.Employee.Include(x => x.Unity)
                                           .Include(x => x.User)
                                           .OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<EmployeeEntity> GetEmployeeByIdAsync(int id)
        {
            var employee = await _context.Employee.Include(x => x.Unity)
                                                  .Include(x => x.User)
                                                  .FirstOrDefaultAsync(x => x.Id == id);

            if(employee == null)
                throw new NotFoundException("Id not found");

            return employee;
        }

        public async Task UpdateEmployeeAsync(EmployeeEntity employee)
        {
            var unity = await _unityService.GetUnityByIdAsync(employee.UnityId);

            if (unity == null)
                throw new NotFoundException("Unity not found");

            if (!unity.Status)
                throw new BusinessException("Unity is inactive");

            var employeeSaved = await this.GetEmployeeByIdAsync(employee.Id);

            employeeSaved.Name = employee.Name;
            employeeSaved.UnityId = employee.UnityId;

            _context.Update(employeeSaved);
            await _context.SaveChangesAsync();
        }
    }
}
