using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Application.Notes.Commands.UpdateNote;
using System;

namespace Notes.WebApi.Models
{
    public class UpdateNoteDto : IMapWIth<UpdateNoteCommand>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateNoteCommand, UpdateNoteDto>()
                .ForMember(noteDto => noteDto.Id, opt => opt
                 .MapFrom(noteCommand => noteCommand.Id))
                    .ForMember(noteDto => noteDto.Title, opt => opt
                 .MapFrom(noteCommand => noteCommand.Title))
                     .ForMember(noteDto => noteDto.Details, opt => opt
                 .MapFrom(noteCommand => noteCommand.Details));
        }
    }
}
