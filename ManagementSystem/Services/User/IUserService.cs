using ManagementSystem.Models;


namespace ManagementSystem.Services.User
{
    public interface IUserService
    {
        public Task CreateUserAsync(UserEntity newUser);
        public Task UpdateUserAsync(UserEntity user);
        public Task<UserEntity> GetUserByIdAsync(int id);
        public Task<List<UserEntity>> GetAllUsersAsync(bool? status = null);
    }
}

