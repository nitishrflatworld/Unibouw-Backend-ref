using Unibouw.Models;

namespace Unibouw.Services
{
    public interface IWorkItemService
    {
        WorkItems Add(WorkItems item);
        IEnumerable<WorkItems> GetAll();
        WorkItems? GetById(int id);
        WorkItems? Update(int id, WorkItems item);
        bool Delete(int id);
    }
}
