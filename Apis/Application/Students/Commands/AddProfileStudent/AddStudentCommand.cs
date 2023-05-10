﻿using MediatR;
using AutoMapper;
using Application.Common.Exceptions;
using Application.Students.DTO;
using Domain.Entities;
// TODO: user soft removes
// TODO: add createBy, modify when, from authentication
// TODO: create all api endpoints
namespace Application.Student.Commands.AddStudent
{
    public record AddStudentCommand : IRequest<StudentDTO>
    {
        public int Id { get; set; }
        public string AvatarURL { get; set; }
    }
    public class AddStudentHandler : IRequestHandler<AddStudentCommand, StudentDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AddStudentHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<StudentDTO> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsyncAsNoTracking(request.Id);
            if (user == null)
                throw new NotFoundException("Student not found");
            user = _mapper.Map<User>(request);
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.UserRepository.Update(user);
            });
            var result = _mapper.Map<StudentDTO>(user);
            return result ?? throw new NotFoundException("Can not add student");
        }
    }
}