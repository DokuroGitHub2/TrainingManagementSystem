﻿using Application.Interfaces;
using Application.ViewModels.TestAssessmentViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestAssessmentController : CustomBaseController
{
    private readonly ITestAssessmentService _testAssessmentService;
    public TestAssessmentController(ITestAssessmentService testAssessmentService)
    {
        _testAssessmentService = testAssessmentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTestAssessmentPagingsion(int pageIndex = 0, int pageSize = 10)
    {
        try
        {
            var result = await _testAssessmentService.GetTestAssessmentPagingsionAsync(pageIndex, pageSize);
            return CustomResult(result);
        }
        catch (Exception ex)
        {
            return CustomResult(ex.Message, HttpStatusCode.BadRequest);
        }
    }



    [HttpPost]
    public async Task<IActionResult?> CreateTestAssessment(CreateTestAssessmentViewModel request)
    {
        return CustomResult(await _testAssessmentService.CreateTestAssessmentAsync(request));
    }

    [HttpPut("{id:int}", Name = "UpdateTestAssessment")]
    public async Task<IActionResult> UpdateTestAssessmentAsync(int id, [FromBody] UpdateTestAssessmentViewModel updateDTO)
    {
        try
        {
            if (id == 0)
            {
                return CustomResult("Invalid product ID", HttpStatusCode.NotFound);
            }
            if (updateDTO == null)
            {
                return CustomResult("Bad Request", HttpStatusCode.BadRequest);
            }
            //var validator = new UpdateTestAssessmentDtoValidator();
            //var validationResult = await validator.ValidateAsync(updateDTO);

            //if (!validationResult.IsValid)
            //{
            //    var errorMessages = validationResult.Errors
            //        .Select(e => e.ErrorMessage)
            //        .ToList();
            //    _response.StatusCode = HttpStatusCode.BadRequest;
            //    _response.ErrorMessages = errorMessages;
            //    return BadRequest(_response);
            //}

            await _testAssessmentService.UpdateAsync(id, updateDTO);

            return CustomResult("Update success", HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            var ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            return CustomResult(ErrorMessages, HttpStatusCode.BadRequest);
        };
    }

    [HttpDelete("{id:int}", Name = "DeleteTestAssessment")]
    public async Task<IActionResult> DeleteTestAssessmentAsync(int id)
    {
        try
        {
            if (id == 0)
            {
                return CustomResult("Id must above 0", HttpStatusCode.BadRequest);
            }

            await _testAssessmentService.RemoveAsync(id);
            return CustomResult("Delete Test Assessment success", HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            var ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            return CustomResult(ErrorMessages, HttpStatusCode.BadRequest);
        };
    }

    [HttpGet("student/{id:int}/finalSyllabusScores")]
    public async Task<IActionResult> GetListSyllabusSocreOfStudentAsync(int id, int? classId, int pageIndex = 0, int pageSize = 10)
    {
        try
        {
            var result = await _testAssessmentService.GetListSyllabusScoreOfStudentAsync(id, classId, pageIndex, pageSize);
            return CustomResult(result);
        }
        catch (Exception ex)
        {
            var ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            return CustomResult(ErrorMessages, HttpStatusCode.BadRequest);
        };
    }

    [HttpGet("class/{id:int}/finalSyllabusScores")]
    public async Task<IActionResult> GetListSyllabusSocreOfClassAsync(int id, int? studentId, int pageIndex = 0, int pageSize = 10)
    {
        try
        {
            var result = await _testAssessmentService.GetListSyllabusScoreOfClassAsync(id, studentId, pageIndex, pageSize);
            return CustomResult(result);
        }
        catch (Exception ex)
        {
            var ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            return CustomResult(ErrorMessages, HttpStatusCode.BadRequest);
        };
    }

    [HttpGet("class/{id:int}/student/{studentId:int}")]
    public async Task<IActionResult> GetStudentGPAScoreOfClassAsync(int id, int studentId, int pageIndex = 0, int pageSize = 10)
    {
        try
        {
            var result = await _testAssessmentService.GetStudentGPAScoreOfClassAsync(id, studentId, pageIndex, pageSize);
            return CustomResult(result);
        }
        catch (Exception ex)
        {
            var ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            return CustomResult(ErrorMessages, HttpStatusCode.BadRequest);
        };
    }

    [HttpGet("student/{id:int}/class/{classId:int}")]
    public async Task<IActionResult> GetClassGPAScoreOfStudentAsync(int id, int classId, int pageIndex = 0, int pageSize = 10)
    {
        try
        {
            var result = await _testAssessmentService.GetClassGPAScoreOfStudentAsync(id, classId, pageIndex, pageSize);
            return CustomResult(result);
        }
        catch (Exception ex)
        {
            var ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            return CustomResult(ErrorMessages, HttpStatusCode.BadRequest);
        };
    }

}
