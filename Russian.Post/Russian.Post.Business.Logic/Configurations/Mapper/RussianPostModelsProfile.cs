using AutoMapper;
using Russian.Post.Database.Models;
using Russian.Post.Models;

namespace Russian.Post.Business.Logic.Configurations.Mapper
{
    public class RussianPostModelsProfile : Profile
    {
        public RussianPostModelsProfile()
        {
            CreateMap<PostClientMessage, ClientMessage>()
               .IgnoreOther()
               .ForMember(u => u.Id, opt => opt.MapFrom(u => u.Id))
               .ForMember(u => u.Message, opt => opt.MapFrom(u => u.Message))
               .ForMember(u => u.IsDelivered, opt => opt.MapFrom(u => u.IsDelivered))
               .ForMember(u => u.AttemptCount, opt => opt.MapFrom(u => u.AttemptCount));

            CreateMap<PostServerMessage, ServerMessage>()
               .IgnoreOther()
               .ForMember(u => u.Message, opt => opt.MapFrom(u => u.Message))
               .ForMember(u => u.IpAddress, opt => opt.MapFrom(u => u.IpAddress))
               .ForMember(u => u.CreatedAt, opt => opt.MapFrom(u => u.CreatedAt));
        }
    }
}
