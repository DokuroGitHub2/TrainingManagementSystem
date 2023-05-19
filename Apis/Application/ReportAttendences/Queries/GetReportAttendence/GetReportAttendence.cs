﻿using Application.Commons;
using Application.ReportAttendances.DTO;
using AutoMapper;
using MediatR;

namespace Application.ReportAttendances.Queries.GetreportAttendance
{
    public record GetreportAttendanceQuery(int PageIndex = 0, int PageSize = 10) : IRequest<Pagination<AttendanceDTO>>;
    public class GetreportAttendanceHandler : IRequestHandler<GetreportAttendanceQuery, Pagination<AttendanceDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetreportAttendanceHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Pagination<AttendanceDTO>> Handle(GetreportAttendanceQuery request, CancellationToken cancellationToken)
        {
            var reportAttendance = await _unitOfWork.ReportAttendanceRepository.ToPagination(request.PageIndex, request.PageSize);

            var result = _mapper.Map<Pagination<AttendanceDTO>>(reportAttendance);

            return result;
        }
    }
}
