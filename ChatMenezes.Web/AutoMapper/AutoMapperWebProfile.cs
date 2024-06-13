using AutoMapper;
using ChatMenezes.Domain.Aggregates.UserAgg.Dtos;
using ChatMenezes.Domain.Aggregates.UserAgg.Entities;

namespace ChatMenezes.Web.AutoMapper
{
    public class AutoMapperWebProfile : Profile
    {
        public AutoMapperWebProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
