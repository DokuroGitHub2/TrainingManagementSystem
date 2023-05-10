﻿using Application.Class.DTO;
using Application.Common.Exceptions;
using Application.ReportAttendences.DTO;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ReportAttendences.Commands.CreateReportAttendences
{
    public record CreateReportAttendencesCommand : IRequest<ReportAttendenceDTO>
    {
        public string Reason { get; set; }
        public DateTime expectedDates { get; set; }
        public string StudentId { get; set; }
    }
    public class CreateReportAttendencesHandler : IRequestHandler<CreateReportAttendencesCommand, ReportAttendenceDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateReportAttendencesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ReportAttendenceDTO> Handle(CreateReportAttendencesCommand request, CancellationToken cancellationToken)
        {
            var reportAttendance = _mapper.Map<ReportAttendence>(request);
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.ReportAttendenceRepository.AddAsync(reportAttendance);
            });
            var result = _mapper.Map<ReportAttendenceDTO>(reportAttendance);
            return result ?? throw new NotFoundException("Class not found");
        }
    }
}
