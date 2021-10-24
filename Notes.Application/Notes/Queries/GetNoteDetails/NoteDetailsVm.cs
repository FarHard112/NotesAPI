using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Domain;
using System;

namespace Notes.Application.Notes.Queries.GetNoteDetails
{
    public class NoteDetailsVm : IMapWIth<Note>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? EditDate { get; set; }
        public void Mapping(Profile profile)
        {
            profile
                .CreateMap<Note, NoteDetailsVm>()
                .ForMember(x => x.Title,
                opt => opt.MapFrom(opt => opt.Title))
                .ForMember(x => x.Details,
                opt => opt.MapFrom(opt => opt.Details))
                .ForMember(x => x.Id,
                opt => opt.MapFrom(opt => opt.Id))
                .ForMember(x => x.CreationDate,
                opt => opt.MapFrom(opt => opt.CreationDate))
                .ForMember(x => x.EditDate,
                opt => opt.MapFrom(opt => opt.EditDate));



        }
    }
}
