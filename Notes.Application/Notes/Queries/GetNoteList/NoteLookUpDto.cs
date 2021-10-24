using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Domain;
using System;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    public class NoteLookUpDto : IMapWIth<Note>
    {

        public Guid Id { get; set; }
        public string Title { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Note, NoteLookUpDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(opt => opt.Id))
                .ForMember(x => x.Title, opt => opt.MapFrom(opt => opt.Title));
        }
    }
}
