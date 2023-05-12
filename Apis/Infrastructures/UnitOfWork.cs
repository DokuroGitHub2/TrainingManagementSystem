﻿using Application;
using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Repositories;
using Infrastructures.Persistence;
using Infrastructures.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructures;

public class UnitOfWork : IUnitOfWork
{
    private IDbContextTransaction? _transaction;
    private bool _disposed;
    //
    private readonly ApplicationDbContext _context;
    private readonly ICacheService _cache;

    // repositories
    public IAttendanceRepository AttendanceRepository { get; }
    public IReportAttendanceRepository ReportAttendanceRepository { get; }
    public IApproveRequestRepository ApproveRequestRepository { get; }
    public IClassRepository ClassRepository { get; }
    public IClassStudentRepository ClassStudentRepository { get; }
    public IOutputStandardRepository OutputStandardRepository { get; }
    public ISyllabusRepository SyllabusRepository { get; }
    public ITestAssessmentRepository TestAssessmentRepository { get; }
    public IUserRepository UserRepository { get; }
    public IUnitRepository UnitRepository { get; }
    public IUnitLessonRepository UnitLessonRepository { get; }
    public IClassTrainerRepository ClassTrainerRepository { get; }
    public ITrainingProgramRepository TrainingProgramRepository { get; }
    public IFeedBackrepository FeedBackRepository { get; }
    //
    public UnitOfWork(ApplicationDbContext dbContext, ICacheService cache)
    {
        _context = dbContext;
        _cache = cache;
        // repositories
        AttendanceRepository = new AttendanceRepository(_context, _cache);
        ClassRepository = new ClassRepository(_context, _cache);
        ClassStudentRepository = new ClassStudentRepository(_context, _cache);
        UserRepository = new UserRepository(_context, _cache);
        SyllabusRepository = new SyllabusRepository(_context, _cache);
        OutputStandardRepository = new OutputStandardRepository(_context, _cache);
        TestAssessmentRepository = new TestAssessmentRepository(_context, _cache);
        AttendanceRepository = new AttendanceRepository(_context, _cache);
        ReportAttendanceRepository = new ReportAttendanceRepository(_context, _cache);
        ApproveRequestRepository = new ApproveRequestRepository(_context, _cache);
        ClassTrainerRepository = new ClassTrainerRepository(_context, _cache);
        TrainingProgramRepository = new TrainingProgramRepository(_context, _cache);
        UnitLessonRepository = new UnitLessonRepository(_context, _cache);
        UnitRepository = new UnitRepository(_context, _cache);
        FeedBackRepository = new FeedBackRepository(_context, _cache);
    }

    // save changes
    public int SaveChanges() => _context.SaveChanges();

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

    // transaction
    public void BeginTransaction()
    {
        _transaction = _context.Database.BeginTransaction();
    }

    // commit
    public void Commit()
    {
        if (_transaction == null)
            throw new TransactionException("No transaction to commit");
        try
        {
            _context.SaveChanges();
            _transaction.Commit();
        }
        finally
        {
            _transaction.Dispose();
            _transaction = null;
        }
    }

    public async Task CommitAsync()
    {
        if (_transaction == null)
            throw new TransactionException("No transaction to commit");
        try
        {
            await _context.SaveChangesAsync();
            _transaction.Commit();
        }
        finally
        {
            _transaction.Dispose();
            _transaction = null;
        }
    }

    // rollback
    public void Rollback()
    {
        if (_transaction == null)
            throw new TransactionException("No transaction to rollback");
        _transaction.Rollback();
        _transaction.Dispose();
        _transaction = null;
    }

    public async Task RollbackAsync()
    {
        if (_transaction == null)
            throw new TransactionException("No transaction to rollback");
        await _transaction.RollbackAsync();
        _transaction.Dispose();
        _transaction = null;
    }

    // dispose
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _transaction?.Dispose();
                _context.Dispose();
            }

            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    // execute transaction
    public async Task ExecuteTransactionAsync(Action action)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            action();
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new TransactionException("Can't execute transaction: "+ ex);
        }
    }
    public List<object> GetTrackedEntities()
    {
        return _context.ChangeTracker
            .Entries()
            .Where(e => e.State != EntityState.Detached && e.Entity != null)
            .Select(e => e.Entity)
            .ToList();
    }
}
