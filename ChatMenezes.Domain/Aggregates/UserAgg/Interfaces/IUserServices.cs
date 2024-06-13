using ChatMenezes.Domain.Aggregates.UserAgg.Entities;
using ChatMenezes.Domain.Aggregates.UserAgg.Requests;
using ChatMenezes.Domain.Aggregates.UserAgg.Responses;
using ChatMenezes.Domain.Common.Dtos;

namespace ChatMenezes.Domain.Aggregates.UserAgg.Interfaces
{
    public interface IUserServices
    {
        Task<User?> GetById(string id);
        Task<ResponseCreateDto<LoginResponse>> DoLogin(LoginRequest request);
        Task<ResponseCreateDto<LoginResponse>> Create(CreateUserRequest request);
    }
}

