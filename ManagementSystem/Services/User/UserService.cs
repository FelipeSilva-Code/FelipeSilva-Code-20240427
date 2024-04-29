using System.Linq;
using ManagementSystem.Data;
using ManagementSystem.Models;
using ManagementSystem.Services.Cryptography;
using ManagementSystem.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Services.User
{
    public class UserService : IUserService
    {
        private readonly ManagementSystemDbContext _context;

        public UserService(ManagementSystemDbContext context)
        {
            _context = context;
        }

        public async Task CreateUserAsync(UserEntity newUser)
        {

            if (_context.User.Any(x => x.Login == newUser.Login))
                throw new EqualException("This login is already registered");

            //CRIPTOGRAFA A SENHA
            byte[] salt = new byte[128 / 8];
            newUser.Salt = CryptographyService.GenerateSalt(salt);

            string encryptedPassword = CryptographyService.CryptographyPassword(newUser.Password, newUser.Salt);
            newUser.Password = encryptedPassword;

            _context.Add(newUser);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserEntity>> GetAllUsersAsync(bool? status = null)
        {
            if(status.HasValue)
                return await _context.User.OrderBy(x => x.Login).Where(x => x.Status == status).ToListAsync();
            else
                return await _context.User.OrderBy(x => x.Login).ToListAsync();
        }

        public async Task<UserEntity> GetUserByIdAsync(int id)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                throw new NotFoundException("Id not found");

            return user;
        }

        public async Task UpdateUserAsync(UserEntity user)
        {
            var userSaved = await this.GetUserByIdAsync(user.Id);

            userSaved.Status = user.Status;

            _context.Update(userSaved);
            await _context.SaveChangesAsync();
        }
    }
}
