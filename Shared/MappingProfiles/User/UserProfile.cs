using AutoMapper;


namespace Shared.MappingProfiles.User;

public sealed class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<Core.User.User, Boundaries.Persistance.Models.User.User>()
            .ReverseMap();
    }
}