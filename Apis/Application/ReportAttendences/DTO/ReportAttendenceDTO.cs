﻿using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ReportAttendences.DTO
{
    public class ReportAttendenceDTO
    {
        public string Reason { get; set; }
        public StatusAttendance statusAttendance { get; set; }
        public DateTime expectedDates { get; set; }
        public string StudentId { get; set; }
        public DateTime CreationDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModificationDate { get; set; }
        public int? ModificationBy { get; set; }
        public int? DeleteBy { get; set; }
        public DateTime? DeletionDate { get; set; }
        public bool IsDeleted { get; set; } 
    }
}
