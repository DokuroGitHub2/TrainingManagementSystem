﻿using Application.Common.Exceptions;
using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.TestAssessmentViewModels;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    public class TestAssessmentService : ITestAssessmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TestAssessmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TestAssessmentViewModel?> CreateTestAssessmentAsync(CreateTestAssessmentViewModel request)
        {
            var obj = _mapper.Map<TestAssessment>(request);
            await _unitOfWork.TestAssessmentRepository.AddAsync(obj);
            var isSuccess = await _unitOfWork.SaveChangesAsync() > 0;
            if (isSuccess)
            {
                return _mapper.Map<TestAssessmentViewModel>(obj);
            }
            return null;
        }

        public async Task<List<TestAssessmentViewModel>> GetTestAssessmentAsync()
        {
            var testAssessments = await _unitOfWork.TestAssessmentRepository.GetAsync();
            var result = _mapper.Map<List<TestAssessmentViewModel>>(testAssessments);
            return result;
        }

        public async Task<Pagination<TestAssessmentViewModel>> GetTestAssessmentPagingsionAsync(int pageIndex = 0, int pageSize = 10)
        {
            var testAssessments = await _unitOfWork.TestAssessmentRepository.ToPagination(pageIndex, pageSize);
            var result = _mapper.Map<Pagination<TestAssessmentViewModel>>(testAssessments);
            return result;
        }

        public async Task RemoveAsync(int id)
        {
            var product = await _unitOfWork.TestAssessmentRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            await _unitOfWork.TestAssessmentRepository.Delete(id);
        }

        public async Task<TestAssessmentViewModel> UpdateAsync(int id, UpdateTestAssessmentViewModel updateDTO)
        {
            TestAssessment model = _mapper.Map<TestAssessment>(updateDTO);
            model.Id = id;
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.TestAssessmentRepository.Update(model);
            });
            var productDto = _mapper.Map<TestAssessmentViewModel>(model);
            return productDto ?? throw new NotFoundException("Can not update test assessment");
        }

        public async Task<List<GetStudentFinalSyllabusScoreViewModel>> GetFinalSyllabusScoreAsync(int id)
        {
            var scoreByTestType = await _unitOfWork.TestAssessmentRepository.GetFinalScoreAsync(id);
            var result = scoreByTestType.GroupBy(ta => new { ta.AttendeeId, ta.SyllabusId, ta.TrainingClassId}).Select(group => new GetStudentFinalSyllabusScoreViewModel
            {
                AttendeeId = group.Key.AttendeeId,
                SyllabusId = group.Key.SyllabusId,
                TrainingClassId = group.Key.TrainingClassId,
                FinalSyllabusScore = group.Sum(ta => ta.AverageScore * ta.SyllabusScheme ) / group.Sum(ta => ta.SyllabusScheme) ?? 0,
                ListAssessment = scoreByTestType.Where(x => x.SyllabusId == group.Key.SyllabusId && x.TrainingClassId == group.Key.TrainingClassId).ToList()
            }).ToList();
            return result ?? throw new NotFoundException("Can not update test assessment");
        }

    }
}