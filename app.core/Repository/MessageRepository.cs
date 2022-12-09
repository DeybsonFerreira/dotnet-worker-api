using dotnet_worker.Data;
using dotnet_worker.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dotnet_worker.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly MyAppContext _context;
        public MessageRepository(MyAppContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(Messages message)
        {
            await _context.AddAsync(message);
            await _context.SaveChangesAsync();
        }

        public async Task<long> Size() => await _context.Messages.LongCountAsync();

        public async Task<List<Messages>> GetNotReads()
        {
            return await _context.Messages.Where(x => !x.Read).ToListAsync();
        }
    }
}