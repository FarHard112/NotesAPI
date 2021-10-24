using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Application.Notes.Commands.DeleteNote;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Application.Notes.Queries.GetNoteList;
using Notes.WebApi.Models;
using System;
using System.Threading.Tasks;

namespace Notes.WebApi.Controllers
{
    //[ApiVersion("1.0")]
    //[ApiVersion("2.0")]

    [ApiVersionNeutral]
    [Produces("application/json")]
    [Route("api/{version:apiVersion}[controller]")]
    public class NoteController : BaseController
    {
        private readonly IMapper mapper;

        public NoteController(IMapper mapper)
        {
            this.mapper = mapper;
        }
        /// <summary>
        /// Get All Notes
        /// </summary>
        /// <remarks>
        /// Sample reuqest:GET/Note
        /// </remarks>
        /// <returns>Returns NoteListVM</returns>
        /// <response code="200">Success</response>
        /// <response code="401"> If user Unauthorized</response>

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<NoteListVm>> GetAll()
        {
            var query = new GetNoteListQuery { UserId = UserId };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
        /// <summary>
        /// Get Note By Id
        /// </summary>
        /// <remarks>GET/note/C39FF293-EAFE-4DB7-9BBD-012267598BE1</remarks>
        /// <param name="Id">Note id (guid)</param>
        /// <returns>NoteDetailsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If user Unauthorized</response>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<NoteListVm>> Get(Guid Id)
        {
            var query = new GetNoteDetailsQuery { Id = Id, UserId = UserId };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
        /// <summary>
        /// Create new Note
        /// </summary>
        /// <remarks>
        /// Sample request:POST/note{title:"note title",details:"note details"}
        /// </remarks>
        /// <param name="createNoteDto">CreateNoteDto object</param>
        /// <returns>guid</returns>
        /// <response code="201">Success</response>
        ///  <response code="401">If user Unauthorized</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateNoteDto createNoteDto)
        {
            var command = mapper.Map<CreateNoteCommand>(createNoteDto);
            command.UserId = UserId;
            var noteId = await Mediator.Send(command);
            return Ok(noteId);
        }

        /// <summary>
        /// Update the note
        /// </summary>
        /// <remarks>
        /// Sample request:{title:"updated note title",details:"updated note details"}
        /// </remarks>
        /// <param name="updateNoteDto">updatedNoteDto object</param>
        /// <returns>Returns nocontent</returns>
        /// <response code="204">Success</response>
        ///  <response code="401">If the user Unauthorized </response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromBody] UpdateNoteDto updateNoteDto)
        {
            var command = mapper.Map<UpdateNoteCommand>(updateNoteDto);
            command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();

        }
        /// <summary>
        /// Delete note by id
        /// </summary>
        /// <remarks>Sample request:DELETE/note/846945ED-8038-42DE-B9ED-E21F505F12B5</remarks>
        /// <param name="Id">Id of the note (guid)</param>
        /// <returns>Nocontent</returns>
        /// <response code="204">Sucess</response>
        /// <response code="401">If user is Unauthorized</response>

        [HttpDelete]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var command = new DeleteNoteCommand
            {
                Id = Id,
                UserId = UserId
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
