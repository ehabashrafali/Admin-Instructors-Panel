using Admin_Panel_ITI.Data;
using Admin_Panel_ITI.Models;
using Admin_Panel_ITI.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Admin_Panel_ITI.Repos.RepoServices
{
    public class Intake_Track_CourseRepoServices : IIntake_Track_CourseRepository
    {
        public MainDBContext Context { get; set; }

        public Intake_Track_CourseRepoServices(MainDBContext context)
        {
            Context = context;
        }
        void IIntake_Track_CourseRepository.DeleteIntake_Track_CoursebyCourseID(int courseID)
        {
            var records = Context.Intake_Track_Courses.Where(itc => itc.CourseID == courseID).ToList();
            foreach (var record in records)
            {
                Context.Intake_Track_Courses.Remove(record);
            }
            Context.SaveChanges();
        }

        void IIntake_Track_CourseRepository.DeleteIntake_Track_CoursebyTrackID(int trackID)
        {
            var records = Context.Intake_Track_Courses.Where(itc => itc.TrackID == trackID).ToList();
            Context.Intake_Track_Courses.RemoveRange(records);
        }


        void IIntake_Track_CourseRepository.DeleteIntake_Track_CoursebyIntakeID(int intakeID)
        {
            var records = Context.Intake_Track_Courses.Where(itc => itc.IntakeID == intakeID).ToList();
            Context.Intake_Track_Courses.RemoveRange(records);

        }

        List<int> IIntake_Track_CourseRepository.computeStudentsNumberForIntakes(List<Intake> intakes)
        {
            List<int> studentNumsforIntake = new List<int>();
            int c = 0;
            foreach (var intake in intakes)
            {
                var records = Context.Intake_Track_Courses.Where(itc => itc.IntakeID == intake.ID);

                foreach (var item in records)
                {
                    c = c + item.numOfStudentsInCourse;
                }
                studentNumsforIntake.Add(c);
                c = 0;
            }
            return studentNumsforIntake;
        }


        //---//
        public List<Intake_Track_Course> GetTracksByIntakeID(int intakeID)
        {
            return Context.Intake_Track_Courses
                .Where(i => i.IntakeID == intakeID)
                .Include(i => i.Intake)
                .Include(i=> i.Track)
                .ToList();
        }

    }
}
