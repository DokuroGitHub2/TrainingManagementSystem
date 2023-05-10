﻿using Domain.Enums;

namespace Domain.Entities;

#pragma warning disable
public class Syllabus : BaseEntity
{
    public string Name { get; set; }
    public string Code { get; set; }
    public int AttendeeNumber { get; set; }
    public string TechnicalRequrement { get; set; }
    public string CourseObjective { get; set; }
    public string TrainingDeliveryPrinciple { get; set; }
    public SyllabusLevel SyllabusLevel { get; set; }
    public string Version { get; set; }
    public float QuizScheme { get; set; }
    public float AsignmentScheme { get; set; }
    public float FinalScheme { get; set; }
    public float FinalTheoryScheme { get; set; }
    public float FinalPraticeScheme { get; set; }
    public float GPAScheme { get; set; }
    //public User CreateByUser { get; set; }
    //public User ModificationByUser { get; set; }

    // Navigation Property
    public ICollection<Unit> Units { get; set; }
    public ICollection<ProgramSyllabus> ProgramSyllabus { get; set; }
    public ICollection<TestAssessment> TestAssessments { get; set; }
    public DateTime CreationDate { get; set; }
    public int? CreatedBy { get; set; }
    public User? CreateByUser { get; set; }
    public DateTime? ModificationDate { get; set; }
    public int? ModificationBy { get; set; }
    public User? ModificationByUser { get; set; }
}
