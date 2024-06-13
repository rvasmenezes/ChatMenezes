using ChatMenezes.Domain.Aggregates.RoomAgg.Entities;
using ChatMenezes.Domain.Aggregates.RoomAgg.Interfaces;
using ChatMenezes.Domain.Common.Dtos;
using ChatMenezes.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatMenezes.Domain.Aggregates.RoomAgg.Services
{
    public class RoomServices : IRoomServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoomServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Room>> GetList()
            => await _unitOfWork.RoomRepository.GetAll()
                .OrderByDescending(x => x.Id)
                .ToListAsync();

        public async Task<Room?> GetById(int id)
            => await _unitOfWork.RoomRepository.GetEntityByIdAsync(id);

        public async Task<ResponseCreateDto<Room>> Add(string name)
        {
            var response = new ResponseCreateDto<Room>
            {
                Entity = await _unitOfWork.RoomRepository.AddAsync(new Room(name))
            };

            await _unitOfWork.Commit();

            return response;
        }
    }
}
