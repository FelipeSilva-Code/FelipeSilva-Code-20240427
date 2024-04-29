using ManagementSystem.Models;
using ManagementSystem.Models.ViewModels;
using ManagementSystem.Services.Unity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ManagementSystem.Controllers
{

    public class UnityController : Controller
    {
        private readonly IUnityService _unityService;

        public UnityController(IUnityService unityService)
        {
            _unityService = unityService;
        }

        [HttpGet]
        [Route("Unities")]
        public async Task<IActionResult> Index()
        {
            var unities = await _unityService.GetAllUnitiesAsync();

            var listViewModels = new List<UnityViewModel>();
            
            foreach(var item in unities)
            {
                var viewModel = new UnityViewModel()
                {
                    Id = item.Id,
                    Code = item.Code,
                    Name = item.Name,
                    Employees = item.Employees.Select(x => new EmployeeViewModel()
                    {
                        Id = x.Id,
                        Nome = x.Name
                    }).ToList(),
                };

                listViewModels.Add(viewModel);
            }

            return View(listViewModels);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UnityViewModel unity)
        {
            if (!ModelState.IsValid)
            {
                return View(unity);
            }

            UnityEntity unityEntity = new UnityEntity()
            {
                Name = unity.Name,
                Code = unity.Code,
                Status = unity.Status
            };

            try
            {
                await _unityService.CreateUnityAsync(unityEntity);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });

            try
            {
                var unity = await _unityService.GetUnityByIdAsync(id.Value);

                UnityViewModel unityViewModel = new UnityViewModel()
                {
                    Id = unity.Id,
                    Name = unity.Name,
                    Code = unity.Code,
                    Status = unity.Status
                };

                if (id == null)
                    return RedirectToAction(nameof(Error), new { message = "Id not provided" });

                return View(unityViewModel);
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> Update(int? id, bool? status)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });

            if (status == null)
                return RedirectToAction(nameof(Error), new { message = "Status not provided" });

            UnityEntity unityEntity = new UnityEntity()
            {
                Id = id.Value,
                Status = status.Value
            };

            try
            {
                await _unityService.UpdateUnityAsync(unityEntity);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
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
