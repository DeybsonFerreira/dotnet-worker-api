using dotnet_worker.Data;

namespace dotnet_worker.Interfaces
{
    public interface IMessageRepository
    {
        Task<List<Messages>> GetNotReads();
    }
}