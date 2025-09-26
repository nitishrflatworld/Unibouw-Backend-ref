using Unibouw.Models;

namespace Unibouw.Services
{
    public class SubContractorService: ISubContractorService
    {
        private readonly List<SubContractor> _items = new();
        private int _nextId = 1;

        public SubContractor Add(SubContractor item)
        {
            item.Id = _nextId++;
            _items.Add(item);
            return item;
        }

        public IEnumerable<SubContractor> GetAll() => _items;

        public SubContractor? GetById(int id) => _items.FirstOrDefault(w => w.Id == id);

        

       
    }
}
