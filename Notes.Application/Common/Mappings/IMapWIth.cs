using AutoMapper;

namespace Notes.Application.Common.Mappings
{
    public interface IMapWIth<T>
    {
        void Mapping(Profile profile) =>
            profile.CreateMap(typeof(T), GetType());

    }
}
