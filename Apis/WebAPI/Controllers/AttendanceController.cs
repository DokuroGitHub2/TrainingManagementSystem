﻿using Application.Commons;
using Application.Attendances.Commands.CreateAttendances;
using Application.Attendances.Commands.UpdateAttendances;
using Application.Attendances.DTO;
using Application.Attendances.Queries.GetAttendanceById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Application.Attendances.Queries.GetAttendanceRequest;
using Application.Attendances.Queries.GetAttendancePendingRequest;
using Domain.Enums;
using Application.Attendances.Queries.SearchAttendanceRequest;
using Application.Attendances.Commands.ApproveAbsent;
using Application.Attendances.Queries.GetAttendanceEachClass;
using Application.Attendances.Commands.SendMailAttendance;

namespace WebAPI.Controllers
{
    public class AttendanceController : CustomBaseController
    {
        private readonly IMediator _mediator;
        public AttendanceController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<Pagination<AttendanceDTO>> Get(int pageIndex = 0, int pageSize = 10)
        {
            return await _mediator.Send(new GetAttendanceRequestQuery(pageIndex, pageSize));
        }
        [HttpGet("Search")]
        public async Task<Pagination<AttendanceRelatedDTO>> Search(
            string? searchString,
            SortType sortType = SortType.Ascending,
            int pageIndex = 0,
            int pageSize = 10)
        {
            return await _mediator.Send(new SearchAttendanceRequestQuery(
                searchString,
                sortType,
                pageIndex,
                pageSize));
        }
        [HttpGet("Pending")]
        public async Task<Pagination<AttendanceRelatedDTO>> GetPending(int pageIndex = 0, int pageSize = 10)
        {
            return await _mediator.Send(new GetAttendancePendingRequestQuery(pageIndex, pageSize));
        }
        [HttpGet("attendance-class")]
        public async Task<Pagination<AttendanceRelatedDTO>> GetAttendanceClass(int pageIndex = 0, int pageSize = 10)
        {
            return await _mediator.Send(new GetAttendanceOfClassQuery(pageIndex, pageSize));
        }
        [HttpGet("{id}")]
        public async Task<AttendanceRelatedDTO> Get(int id)
        {

            return await _mediator.Send(new GetAttendanceByIdQuery(id));
        }
        [HttpGet("test")]
        public async Task Get()
        {

            await _mediator.Send(new SendMailAttendanceCommand());
        }

        [HttpPost]
        public async Task<AttendanceDTO> Post([FromBody] CreateAttendancesCommand request)
        {

            return await _mediator.Send(request);
        }
        [HttpPut]
        public async Task<AttendanceDTO> Put([FromBody] UpdateAttendancesCommand request)
        {

            return await _mediator.Send(request);
        }
        [HttpPut("ApproveAbsent")]
        [Authorize(Roles = "ClassAdmin")]
        public async Task<AttendanceDTO> ApproveAbsent(ApproveAbsentCommand request)
        {
            return await _mediator.Send(request);
        }
    }
}
