﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class FSU : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<TrainingClass> TrainingClasses { get; set; }
    }
}
