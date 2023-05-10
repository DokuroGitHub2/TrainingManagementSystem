﻿using Domain.Enums;

namespace Domain.Entities
{
    public class TestAssessment : BaseEntity
    {
        public float Score { get; set; }
        public TestAssessmentType TestAssessmentType { get; set; }
        public int AttendeeId { get; set; }
        public User Attendee { get; set; }
        public int SyllabusId { get; set; }
        public Syllabus Syllabus { get; set; }
    }
}
