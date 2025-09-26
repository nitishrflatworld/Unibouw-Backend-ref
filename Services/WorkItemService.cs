using Unibouw.Models;

namespace Unibouw.Services
{
    public class WorkItemService : IWorkItemService
    {
        private readonly List<WorkItems> _items = new();
        private int _nextId = 1;

        public WorkItems Add(WorkItems item)
        {
            item.Id = _nextId++;
            _items.Add(item);
            return item;
        }

        public IEnumerable<WorkItems> GetAll() => _items;

        public WorkItems? GetById(int id) => _items.FirstOrDefault(w => w.Id == id);

        public WorkItems? Update(int id, WorkItems item)
        {
            var existing = _items.FirstOrDefault(w => w.Id == id);
            if (existing == null) return null;

            existing.WorkItemCode = item.WorkItemCode;
            existing.WorkItem = item.WorkItem;
            existing.Description = item.Description;

            return existing;
        }

        public bool Delete(int id)
        {
            var existing = _items.FirstOrDefault(w => w.Id == id);
            if (existing == null) return false;

            _items.Remove(existing);
            return true;
        }
    }
}