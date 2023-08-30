﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Admin_Panel_ITI.Models
{
    public class Intake_Instructor
    {

        [ForeignKey(nameof(Intake))]
        public int IntakeID { get; set; }
        public virtual Intake? Intake { get; set; }


        [ForeignKey(nameof(Instructor))]
        public string InstructorID { get; set; }
        public virtual Instructor? Instructor { get; set; }
    }
}
