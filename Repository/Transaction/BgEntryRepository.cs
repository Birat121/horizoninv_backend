using backend.Data;
using backend.Models;
using System;


namespace backend.Repository.Transaction
{
    public interface IBgEntryRepository
    {
        Task<BGEntry> CreateBgEntryAsync(BGEntry entry);
        Task<IEnumerable<BGEntry>> ShowBgEntryAsync();
    }
    public class BgEntryRepository : IBgEntryRepository
    {
        private readonly ApplicationDbContext _context;

        public BgEntryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BGEntry> CreateBgEntryAsync(BGEntry entry)
        {
            _context.BGEntries.Add(entry);
            await _context.SaveChangesAsync();
            return entry;
        }


        }
    }
