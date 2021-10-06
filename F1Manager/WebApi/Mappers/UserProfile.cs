using AutoMapper;
using Domain.Users;
using System;
using WebApi.DTOs;
using WebApi.Models;

namespace WebApi.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, User>().ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId == 0 ? 4 : src.RoleId))
                        .ForMember(dest =>dest.CreatedDate,opt=>opt.MapFrom(src=>DateTime.Now));
            CreateMap<ContentWriterDto, User>().ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId == 0 ? 3 : src.RoleId))
                        .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
                        .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.AdminName));
            CreateMap<ContentWriterUpdateDto, User>()
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId == 0 ? 3 : src.RoleId))
                        .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.Now))
                        .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.AdminName));

            CreateMap<User, SingleAcountView>().ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.RoleName))
                                    .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));
            CreateMap<User, AdminRegistrationView>().ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.RoleName))
                                    .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));
            CreateMap<User, LoginView>().ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));
            CreateMap<User, RegistrationView>().ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => $"You need to comfirm your account using link which we are sent to your mail {src.Email}"));

        }
    }
}