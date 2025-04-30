using ScientificOperationsCenter.Api.Models;


namespace ScientificOperationsCenter.Api.DAL.Interfaces
{
    public interface ILoginRepository
    {
        Task<string> LoginAsync(UserLogin userLogin);
    }
}
