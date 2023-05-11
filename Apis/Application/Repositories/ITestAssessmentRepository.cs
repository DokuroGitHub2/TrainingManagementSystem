﻿using Application.ViewModels.TestAssessmentViewModels;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Repositories;

public interface ITestAssessmentRepository : IGenericRepository<TestAssessment>
{
    Task<List<GetStudentTestScoreViewModel>> GetFinalScoreAsync(Expression<Func<TestAssessment, bool>> filter = null);
}
