﻿using Application.Class.DTO;
using Application.Common.Exceptions;
using Application.Interfaces;
using Application.TrainingPrograms.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Class.Commands.DuplicateTrainProgram
{
    public record DuplicateTrainProgramCommand : IRequest<TrainingProgramDTO>
    {
        public int id { get; init; }
    }
    public class DuplicateTrainProgramHandler : IRequestHandler<DuplicateTrainProgramCommand, TrainingProgramDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimService _claimService;
        private readonly ICurrentTime _currentTime;
        public DuplicateTrainProgramHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IClaimService claimService,
            ICurrentTime currentTime)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _claimService = claimService;
            _currentTime = currentTime;
        }
        public async Task<TrainingProgramDTO> Handle(DuplicateTrainProgramCommand request, CancellationToken cancellationToken)
        {

            var isExistTrainProgram = await _unitOfWork.TrainingProgramRepository.AnyAsync(x => x.Id == request.id);
            if (isExistTrainProgram is false)
            {
                throw new NotFoundException("Training program not found");
            }
            var trainingProgram = await _unitOfWork.TrainingProgramRepository.FirstOrdDefaultAsync(
                filter: x => x.Id == request.id,
                include: x => x.Include(x => x.ProgramSyllabus)
                               .ThenInclude(x => x.Syllabus)
                               .ThenInclude(x => x.Units)
                               .ThenInclude(x => x.UnitLessons)
                               .ThenInclude(x => x.TrainingMaterials)
            );
            var duplicate = _mapper.Map<TrainingProgramDuplicate>(trainingProgram);
            var trainingProgramUpdate = _mapper.Map<TrainingProgram>(duplicate);
            trainingProgramUpdate.CreatedBy = _claimService.CurrentUserId;
            trainingProgramUpdate.CreationDate = _currentTime.GetCurrentTime();
            trainingProgramUpdate.ParentId = request.id;
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.TrainingProgramRepository.AddAsync(trainingProgramUpdate);
            });
            var result = _mapper.Map<TrainingProgramDTO>(trainingProgramUpdate);
            return result;
        }
    }
}
