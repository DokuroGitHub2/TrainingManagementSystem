using Application.Commons;
using Application.Syllabuses.Commands.CreateSyllabus;
using Application.Syllabuses.Commands.ImportSyllabusesCSV;
using Application.Syllabuses.DTO;
using Application.Syllabuses.Queries.ExportSyllabusesCSV;
using Application.Syllabuses.Queries.GetSyllabus;
using Application.Syllabuses.Queries.GetSyllabusById;
using Application.Syllabuses.Queries.GetPagedSyllabusesByDateRange;
using Domain.Aggregate.AppResult;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class SyllabusesController : BaseController
{
    private readonly IMediator _mediator;
    public SyllabusesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    // TODO: FIXE pagination user params or move the another layer
    [HttpGet]
    public async Task<ApiResult<Pagination<SyllabusDTO>>> Get(int pageIndex = 0, int pageSize = 10)
     => await _mediator.Send(new GetSyllabusQuery(pageIndex, pageSize));
    [HttpGet("{id}")]
    public async Task<SyllabusDTO> Get(int id)
     => await _mediator.Send(new GetSyllabusByIdQuery(id));
    [HttpPost]
    public async Task<SyllabusDTO> Post([FromBody] CreateSyllabusCommand request)
    => await _mediator.Send(request);

    #region CSV
    [HttpGet("export-syllabuses-csv")]
    public async Task<FileStreamResult> ExportSyllabusesCSV(string columnSeparator)
        => await _mediator.Send(new ExportSyllabusesCSVQuery(columnSeparator));

    [HttpPost("import-syllabuses-csv")]
    public async Task<List<SyllabusDTO>> ImportSyllabusesCSV(
        [FromQuery] bool IsScanCode,
        [FromQuery] bool IsScanName,
        [FromQuery] DuplicateHandle DuplicateHandle,
        [FromForm] IFormFile formFile)
        => await _mediator.Send(new ImportSyllabusesCSVCommand()
        {
            FormFile = formFile,
            IsScanCode = IsScanCode,
            IsScanName = IsScanName,
            DuplicateHandle = DuplicateHandle
        });
    #endregion CSV

    [HttpGet("get-paged-syllabuses-by-date-range")]
    public async Task<ApiResult<Pagination<SyllabusDTO>>> GetPagedSyllabusesByDateRange(
        [FromQuery] DateTime fromtDate,
        [FromQuery] DateTime toDate,
        [FromQuery] int pageIndex = 0,
        [FromQuery] int pageSize = 10)
        => await _mediator.Send(new GetPagedSyllabusesByDateRangeQuery()
        {
            FromDate = fromtDate,
            ToDate = toDate,
            PageIndex = pageIndex,
            PageSize = pageSize,
        });
}