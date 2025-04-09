using backend.Models;
using backend.Repository.Transaction;

namespace backend.Services.Transaction
{

    public interface IBgEntryServices
    {
        Task<IEnumerable<BGEntry>> GetAsync();
        Task<BGEntry> CreateEntryAsync(BGEntry entry, string CustomerName);
    }
    public class BgEntryServices : IBgEntryServices
    {
        private readonly IBgEntryRepository _repository;

        public BgEntryServices(IBgEntryRepository repository)
        {
            _repository = repository;
        }

        public async Task<BGEntry> CreateEntryAsync(BGEntry entry, string CustomerName)
        {

        }
    }
}
