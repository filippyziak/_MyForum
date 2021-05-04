using System.Linq;
using AutoMapper;
using MyForum.Models.Domain.Auth;
using MyForum.Models.Domain.Post;
using MyForum.ViewModels;

namespace MyForum.Core.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, ProfileViewModel>();
            CreateMap<Category, CategoryViewModel>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(c => c.Name))
            .ForMember(dest => dest.Posts, opt => opt.Ignore());
            CreateMap<Post, PostCardViewModel>()
            .ForMember(dest => dest.PostTitle, opt => opt.MapFrom(p => p.Title))
            .ForMember(dest => dest.PostId, opt => opt.MapFrom(p => p.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(p => p.Title.Length > 11 ? p.Title.Substring(0, 8) + "..." : p.Title))
            .ForMember(dest => dest.Answers, opt => opt.MapFrom(p => p.Answers.OrderBy(a => a.Created)));
        }
    }
}