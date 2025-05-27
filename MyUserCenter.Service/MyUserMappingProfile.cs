using AutoMapper;
using MyUserCenter.Domain;
using MyUserCenter.Service.Dto;

namespace MyUserCenter.Service;

public class MyUserMappingProfile : Profile {
    public MyUserMappingProfile() {
        CreateMap<MyUser, MyUserDto>();
    }
}
