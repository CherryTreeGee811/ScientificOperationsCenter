using ScientificOperationsCentre.Api.Models;


namespace ScientificOperationsCentre.Api.DAL.Interfaces
{
    public interface ILoginRepository
    {
        Task<string> LoginAsync(UserLogin userLogin);
    }
}
