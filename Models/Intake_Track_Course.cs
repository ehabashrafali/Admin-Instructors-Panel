﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Admin_Panel_ITI.Models
{
    public class Intake_Track_Course
    {
        [ForeignKey(nameof(Intake))]
        public int IntakeID { get; set; }
        public virtual Intake? Intake { get; set; }



        [ForeignKey(nameof(Track))]
        public int TrackID { get; set; }
        public virtual Track? Track { get; set; }



        [ForeignKey(nameof(Course))]
        public int CourseID { get; set; }
        public virtual Course? Course { get; set; }

    }
}