using Admin_Panel_ITI.Data;
using Admin_Panel_ITI.Models;
using Admin_Panel_ITI.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Admin_Panel_ITI.Repos.RepoServices
{
    public class InstructorRepoServices : IInstructorRepository
    {
        private readonly ITrackRepository trackRepository;
        private readonly IExamRepository examRepository;
        private readonly IMaterialRepository materialRepository;
        private readonly IInstructor_CourseRepository instructor_CourseRepository;

        public MainDBContext Context { get; set; }


        public InstructorRepoServices(MainDBContext context, ITrackRepository trackRepository, IExamRepository examRepository, IMaterialRepository materialRepository, IInstructor_CourseRepository instructor_CourseRepository)
        {
            Context = context;
            this.trackRepository = trackRepository;
            this.examRepository = examRepository;
            this.materialRepository = materialRepository;
            this.instructor_CourseRepository = instructor_CourseRepository;
        }


        void IInstructorRepository.CreateInstructor(Instructor instructor)
        {
            Context.Instructors.Add(instructor);
            Context.SaveChanges();
        }

        void IInstructorRepository.DeleteInstructor(int instructorID)
        {

            // make manager of track na or null
            trackRepository.RemoveManager(instructorID);

            // make instructor id of exam na or null
            examRepository.RemoveInstructor(instructorID);

            // make instructor id of material na or null
            materialRepository.RemoveInstructor(instructorID);

            //delete record of instructor_course
            instructor_CourseRepository.DeleteInstructor_Course(instructorID);


            var ins = Context.Instructors.FirstOrDefault(i => i.Id == instructorID.ToString());
            Context.Instructors.Remove(ins);    
        }

        Instructor IInstructorRepository.GetInstructorbyID(int instructorID)
        {
            var ins = Context.Instructors.FirstOrDefault(i => i.Id == instructorID.ToString());
            return ins;
        }

        int IInstructorRepository.GetInstructorNumber()
        {
            return Context.Instructors.Count();
        }

        int IInstructorRepository.GetInstructorNumberbyIntakeID(int intakeID)
        {
            var tracks = Context.Intake_Instructors.Where(it => it.IntakeID == intakeID);
            return tracks.Count();

        }

        List<Instructor> IInstructorRepository.GetInstructors()
        {
            return Context.Instructors.Include(i=>i.Tracks).Include(i => i.InstructorCourses).ThenInclude(ic=>ic.Course).ToList();
        }

        void IInstructorRepository.UpdateInstructor(int instructorID, Instructor instructor)
        {
            var instructor_updated = Context.Instructors.FirstOrDefault(i=>i.Id == instructorID.ToString());
            instructor_updated.FullName = instructor.FullName;
            instructor_updated.UserName = instructor.UserName;
            instructor_updated.AdminID = instructor.AdminID;

            Context.SaveChanges();
        }
    }
}
