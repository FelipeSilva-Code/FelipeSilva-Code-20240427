using ManagementSystem.Models;
using ManagementSystem.Models.ViewModels;
using ManagementSystem.Services.User;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace ManagementSystem.Controllers
{
    
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("User")]
        public async Task<IActionResult> Index([FromQuery] bool? status)
        {
            var users = await _userService.GetAllUsersAsync(status);

            var usersViewModel = users.Select(x => new UserViewModel() { Id = x.Id, Login = x.Login, Status = x.Status });
            
            return View(usersViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            UserEntity userEntity = new UserEntity()
            {
                Login = user.Login,
                Password = user.Password,
                Status = user.Status
            };

            try
            {
                await _userService.CreateUserAsync(userEntity);

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
                var user = await _userService.GetUserByIdAsync(id.Value);

                UserViewModel userViewModel = new UserViewModel()
                {
                    Id = user.Id,
                    Login = user.Login,
                    Status = user.Status
                };

                if (id == null)
                    return RedirectToAction(nameof(Error), new { message = "Id not provided" });

                return View(userViewModel);
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

            UserEntity userEntity = new UserEntity()
            {
                Id = id.Value,
                Status = status.Value
            };

            try
            {
                await _userService.UpdateUserAsync(userEntity);
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
