using Application.Commons;
using Application.Users.DTO;
using Application.Users.GetUser.Queries;
using Application.Users.GetUserById.Queries;
using Application.Users.Queries.ExportUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class UserController : CustomBaseController
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<Pagination<UserDTO>> GetAsync(int pageIndex = 0, int pageSize = 10)
            => await _mediator.Send(new GetUserQuery(pageIndex, pageSize));

        [HttpGet("{id}")]
        [Authorize]
        public async Task<UserDTO> GetAsync(int id)
            => await _mediator.Send(new GetUserByIdQuery(id));

        [HttpGet("export-users")]
        public async Task<FileResult> Get()
        {
            var vm = await _mediator.Send(new ExportUsersQuery());

            return File(vm.Content, vm.ContentType, vm.FileName);
        }
    }

    // TODO: Send mail when user register
    // TODO: Method: forget password,
    /// change password,
    /// filter user,
    /// validate token(fe),
    /// save token in session, 
    /// Logout 
}
