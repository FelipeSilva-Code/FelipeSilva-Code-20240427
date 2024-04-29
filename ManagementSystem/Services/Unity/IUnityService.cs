using ManagementSystem.Models;

namespace ManagementSystem.Services.Unity
{
    public interface IUnityService
    {
        public Task CreateUnityAsync(UnityEntity newUnity);
        public Task UpdateUnityAsync(UnityEntity unity);
        public Task<UnityEntity> GetUnityByIdAsync(int id);
        public Task<List<UnityEntity>> GetAllUnitiesAsync();
    }
}
