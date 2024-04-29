using ManagementSystem.Data;
using ManagementSystem.Models;
using ManagementSystem.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Services.Unity
{
    public class UnityService : IUnityService
    {
        private readonly ManagementSystemDbContext _context;

        public UnityService(ManagementSystemDbContext context)
        {
            _context = context;
        }

        public async Task CreateUnityAsync(UnityEntity newUnity)
        {
            if(await _context.Unity.AnyAsync(x => x.Code == newUnity.Code))
                throw new EqualException("This unity code is already registered");

            _context.Add(newUnity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UnityEntity>> GetAllUnitiesAsync()
        {
            return await _context.Unity.OrderBy(x => x.Code).Include(x => x.Employees).ToListAsync();
        }

        public async Task<UnityEntity> GetUnityByIdAsync(int id)
        {
            var unity = await _context.Unity.FirstOrDefaultAsync(x => x.Id == id);

            if (unity == null)
                throw new NotFoundException("Id not found");

            return unity;
        }

        public async Task UpdateUnityAsync(UnityEntity unity)
        {
            var unitySaved = await this.GetUnityByIdAsync(unity.Id);

            unitySaved.Status = unity.Status;

            _context.Update(unitySaved);
            await _context.SaveChangesAsync();
        }
    }
}
