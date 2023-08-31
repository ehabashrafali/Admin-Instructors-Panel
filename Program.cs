using Admin_Panel_ITI.Data;
using Admin_Panel_ITI.Models;
using Admin_Panel_ITI.Repos;
using Admin_Panel_ITI.Repos.Interfaces;
using Admin_Panel_ITI.Repos.RepoServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Admin_Panel_ITI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<MainDBContext>(options =>
                options.UseSqlServer(connectionString));



            builder.Services.AddIdentity<AppUser,IdentityRole>()
                .AddEntityFrameworkStores<MainDBContext>().AddDefaultTokenProviders();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddControllersWithViews();


            builder.Services.AddRazorPages();



            builder.Services.AddScoped<ICourse_Day_MaterialRepository, Course_Day_MaterialRepoServices>();
            builder.Services.AddScoped<ICourseDayRepository, CourseDayRepoServices>();
            builder.Services.AddScoped<ICourseRepository, CourseRepoServices>();
            builder.Services.AddScoped<IExam_QuestionRepository, Exam_QuestionRepoServices>();
            builder.Services.AddScoped<IExam_Std_QuestionRepository, Exam_Std_QuestionRepoServices>();
            builder.Services.AddScoped<IExamRepository, ExamRepoServices>();
            builder.Services.AddScoped<IInstructor_CourseRepository, Instructor_CourseRepoServices>();
            builder.Services.AddScoped<IInstructorRepository, InstructorRepoServices>();
            builder.Services.AddScoped<IIntake_TrackRepository, Intake_TrackRepoServices>();
            builder.Services.AddScoped<IIntakeRepository, IntakeRepoServices>();
            builder.Services.AddScoped<IMaterialRepository, MaterialRepoServices>();
            builder.Services.AddScoped<IQuestionRepository, QuestionRepoServices>();
            builder.Services.AddScoped<IStudent_CourseRepository, Student_CourseRepoServices>();
            builder.Services.AddScoped<IStudent_SubmissionRepository, Student_SubmissionRepoServices>();
            builder.Services.AddScoped<IStudentRepository, StudentRepoServices>();
            builder.Services.AddScoped<IIntake_Track_CourseRepository, Intake_Track_CourseRepoServices>();
            builder.Services.AddScoped<IIntake_InstructorRepository, Intake_InstructorRepoServices>();
            builder.Services.AddScoped<ITrackRepository, TrackRepoServices>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            

            app.MapRazorPages();

            app.Run();
        }
    }
}