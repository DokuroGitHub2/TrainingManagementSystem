﻿namespace Domain.Entities;

#pragma warning disable
public class ClassStudent : BaseEntity
{
    public int TrainingClassId { get; set; }
    public TrainingClass TrainingClass { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public DateTime CreationDate { get; set; }
    public int? CreatedBy { get; set; }
    public User? CreateByUser { get; set; }
    public DateTime? ModificationDate { get; set; }
    public int? ModificationBy { get; set; }
    public User? ModificationByUser { get; set; }
    public string? PersonalEmail { get; set; }
    public float GPA { get; set; }
}
