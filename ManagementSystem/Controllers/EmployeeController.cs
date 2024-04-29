using ManagementSystem.Models;
using ManagementSystem.Models.ViewModels;
using ManagementSystem.Services.Employee;
using ManagementSystem.Services.Unity;
using ManagementSystem.Services.User;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IUserService _userService;
        private readonly IUnityService _unityService;

        public EmployeeController(IEmployeeService employeeService, IUserService userService, IUnityService unityService)
        {
            _employeeService = employeeService;
            _unityService = unityService;
            _userService = userService;
        }

        [HttpGet]
        [Route("Employees")]
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();

            List<EmployeeViewModel> listViewModel = new List<EmployeeViewModel>();
            foreach (var item in employees)
            {
                EmployeeViewModel employeeViewModel = new EmployeeViewModel()
                {
                    Id = item.Id,
                    Nome = item.Name,
                    User = new UserViewModel()
                    {
                        Id = item.User.Id,
                        Login = item.User.Login
                    },
                    Unity = new UnityViewModel()
                    {
                        Id = item.Unity.Id,
                        Code = item.Unity.Code,
                        Name = item.Unity.Name,
                    }
                };

                listViewModel.Add(employeeViewModel);
            }

            return View(listViewModel);
        }

        public async Task<IActionResult> Create()
        {
            var unities = await _unityService.GetAllUnitiesAsync();
            var unitiesViewModel = unities.Select(x => new UnityViewModel() { Id = x.Id, Name = x.Name, Code = x.Code, Status = x.Status });

            var users = await _userService.GetAllUsersAsync();
            var usersViewModel = users.Select(x => new UserViewModel() { Id = x.Id, Login = x.Login, Status = x.Status });

            EmployeeViewModel employeeViewModel = new EmployeeViewModel()
            {
                AllUnities = unitiesViewModel.ToList(),
                AllUsers = usersViewModel.ToList()
            };

            return View(employeeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            EmployeeEntity employeeEntity = new EmployeeEntity()
            {
                Name = employee.Nome,
                UnityId = employee.IdUnity.Value,
                UserId = employee.IdUser.Value,
                Status = true
            };

            try
            {
                await _employeeService.CreateEmployeeAsync(employeeEntity);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });

            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id.Value);

                EmployeeViewModel employeeViewModel = new EmployeeViewModel()
                {
                    Id = employee.Id,
                    Nome = employee.Name,
                    Unity = new UnityViewModel()
                    {
                        Name = employee.Unity.Name
                    }
                };

                return View(employeeViewModel);
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

        }

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _employeeService.RemoveEmployeeAsync(id);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

        }

        public async Task<IActionResult> Update(int? id)
        {

            try
            {
                if (id == null)
                    return RedirectToAction(nameof(Error), new { message = "Id not provided" });

                var employee = await _employeeService.GetEmployeeByIdAsync(id.Value);

                var unities = await _unityService.GetAllUnitiesAsync();
                var unitiesViewModel = unities.Select(x => new UnityViewModel() { Id = x.Id, Name = x.Name, Code = x.Code, Status = x.Status });

                EmployeeViewModel employeeViewModel = new EmployeeViewModel()
                {
                    Id = employee.Id,
                    Nome = employee.Name,
                    AllUnities = unitiesViewModel.ToList()
                };

                return View(employeeViewModel);
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, EmployeeViewModel viewModel)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });

            if (viewModel.IdUnity == null)
                return RedirectToAction(nameof(Error), new { message = "Id Unity not provided" });

            EmployeeEntity employeeEntity = new EmployeeEntity()
            {
                Id = id.Value,
                Name = viewModel.Nome,
                UnityId = viewModel.IdUnity.Value
            };

            try
            {
                await _employeeService.UpdateEmployeeAsync(employeeEntity);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            ErrorViewModel viewModel = new ErrorViewModel()
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }

    }
}
