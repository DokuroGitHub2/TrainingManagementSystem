﻿
#nullable disable warnings


using Domain.Entities.Users;

namespace Domain.Entities.Syllabuses
{
    public partial class Syllabus
    {
        public User? CreatedAdmin { get; set; }
        public User? ModifiedAdmin { get; set; }
        public ICollection<Unit> Units { get; set; }
        public ICollection<ProgramSyllabus> ProgramSyllabuses { get; set; }
        public ICollection<TestAssessment> TestAssessments { get; set; }
    }
}