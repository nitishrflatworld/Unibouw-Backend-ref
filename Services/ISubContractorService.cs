using Unibouw.Models;

namespace Unibouw.Services
{
    public interface ISubContractorService
    {
        SubContractor Add(SubContractor item);
        IEnumerable<SubContractor> GetAll();
        SubContractor? GetById(int id);
    }
}
