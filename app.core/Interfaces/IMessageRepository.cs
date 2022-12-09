using dotnet_worker.Data;

namespace dotnet_worker.Interfaces
{
    public interface IMessageRepository
    {
        Task SaveAsync(Messages message);
        Task<List<Messages>> GetNotReads();
        Task<long> Size();
    }
}