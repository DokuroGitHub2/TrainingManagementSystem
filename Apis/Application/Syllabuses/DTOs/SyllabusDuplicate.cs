using Application.Users.DTO;
using Domain.Entities;
using Domain.Enums;
namespace Application.Syllabuses.DTO
{
    public class SyllabusDuplicate
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int AttendeeNumber { get; set; }
        public string CourseObjective { get; set; }

        public SyllabusLevel SyllabusLevel { get; set; }

        public float QuizScheme { get; set; }
        public float AssignmentScheme { get; set; }
        public float FinalScheme { get; set; }
        public float FinalTheoryScheme { get; set; }
        public float FinalPracticeScheme { get; set; }
        public float GPAScheme { get; set; }

        // Navigation Property

        public List<SyllabusUnitDuplicate> Units { get; set; }
        public List<ProgramSyllabus>? ProgramSyllabus { get; set; }
        public List<TestAssessment>? TestAssessments { get; set; }
        public DateTime CreationDate { get; set; }

        public int? CreatedBy { get; set; }
        public UserDTO? CreateByUser { get; set; }

        public DateTime? ModificationDate { get; set; }

        public int? ModificationBy { get; set; }
        public UserDTO? ModificationByUser { get; set; }
    }
    public record SyllabusUnitDuplicate
    {
        public string Name { get; init; }
        public int SyllabusSession { get; init; }
        public int UnitNumber { get; init; }
        public List<SyllabusLessonDuplicate> UnitLessons { get; init; }
    }
    public class SyllabusLessonDuplicate
    {
        public string Name { get; init; }
        public int Duration { get; init; }
        public LessonType LessonType { get; init; }
        public DeliveryType DeliveryType { get; init; }
        public List<TrainingMaterialDuplicate> TrainingMaterials { get; init; }
    }
    public class TrainingMaterialDuplicate
    {
        public string FileName { get; init; }
        public string FilePath { get; init; }
        public long FileSize { get; init; }
    }
}