using ChatMenezes.Domain.Aggregates.RoomAgg.Entities;
using ChatMenezes.Domain.Common.Dtos;

namespace ChatMenezes.Domain.Aggregates.RoomAgg.Interfaces
{
    public interface IRoomServices
    {
        Task<List<Room>> GetList();
        Task<Room?> GetById(int id);
        Task<ResponseCreateDto<Room>> Add(string name);
    }
}

