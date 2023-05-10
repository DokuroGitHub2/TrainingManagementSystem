﻿namespace Domain.Entities
{
    public class ClassAdmin : BaseEntity
    {
        public int TrainingClassId { get; set; }
        public TrainingClass TrainingClass { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
