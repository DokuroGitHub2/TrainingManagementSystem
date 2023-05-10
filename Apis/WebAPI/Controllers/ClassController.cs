﻿// using Application.Class.DTO;
// using Application.Class.Queries.GetClass;
using Application.Commons;
using Domain.Aggregate.AppResult;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class ClassController : BaseController
    {
        private readonly IMediator _mediator;
        public ClassController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // [HttpGet]
        // public async Task<ApiResult<Pagination<ClassDTO>>> Get(int pageIndex = 0, int pageSize = 10)
        //     => await _mediator.Send(new GetClassQuery(pageIndex, pageSize));
        // [HttpGet("{id}")]
        // public async Task<ClassDTO> Get(int id)
        //     => await _mediator.Send(new GetClassByIdQuery(id));
        // [HttpGet("Program")]
        // public async Task<ApiResult<Pagination<ClassProgram>>> GetProgram(int pageIndex = 0, int pageSize = 10)
        //     => await _mediator.Send(new GetClassProgramQuery(pageIndex, pageSize));
        // [HttpPost]
        // public async Task<ClassDTO> Post([FromBody] CreateClassCommand request)
        //     => await _mediator.Send(request);
        // [HttpPut]
        // public async Task<ClassDTO> Put([FromBody] UpdateClassCommand request)
        //     => await _mediator.Send(request);
        // [HttpDelete("{id}")]
        // public async Task<ClassDTO> Delete(int id)
        //     => await _mediator.Send(new DeleteClassCommand(id));
    }
}
