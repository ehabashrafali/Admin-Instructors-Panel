using System.ComponentModel.DataAnnotations;
using Admin_Panel_ITI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using Admin_Panel_ITI.Repos;
using Admin_Panel_ITI.Repos.Interfaces;
using PartialViewResult = Microsoft.AspNetCore.Mvc.PartialViewResult;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;
//using System.Web.UI.ClientScriptManager;
//using System.Web.

namespace Admin_Panel_ITI.Areas.Identity.Pages.Account
{
    public class AddUserModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserStore<AppUser> _userStore;
        private readonly IUserEmailStore<AppUser> _emailStore;
        private readonly ILogger<AddUserModel> _logger;
        private readonly IAdminRepository adminRepository;
        private readonly IStudentRepository studentRepository;
        private readonly IInstructorRepository instructorRepository;
        private readonly IIntakeRepository intakeRepository;
        private readonly IIntake_InstructorRepository intakeInstructorRepository;
        private readonly ITrackRepository trackRepository;
        private readonly IIntake_Track_CourseRepository intake_Track_CourseRepository;
        private readonly IStudent_CourseRepository student_CourseRepository;

        public AddUserModel(
            UserManager<AppUser> userManager,
            IUserStore<AppUser> userStore,
            SignInManager<AppUser> signInManager,
            ILogger<AddUserModel> logger,
            IAdminRepository _adminRepository,
            IStudentRepository _studentRepository,
            IInstructorRepository _instructorRepository, 
            IIntakeRepository _intakeRepository,
            IIntake_InstructorRepository _intakeInstructorRepository,
            IIntake_Track_CourseRepository _intake_Track_CourseRepository,
            IStudent_CourseRepository _student_CourseRepository,
            ITrackRepository _trackRepository)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            adminRepository = _adminRepository;
            studentRepository = _studentRepository;
            instructorRepository = _instructorRepository;
            intakeRepository = _intakeRepository;
            intakeInstructorRepository = _intakeInstructorRepository;
            trackRepository = _trackRepository;
            intake_Track_CourseRepository = _intake_Track_CourseRepository;
            student_CourseRepository = _student_CourseRepository;
        }


        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public enum UserType { Admin, Instructor, Student }

        public class InputModel
        {
            [Required]
            public UserType userType { get; set; }

            [Required, MaxLength(50), MinLength(3)]
            [DisplayName("Full Name")]
            public string FullName { get; set; }


            [Required, MaxLength(50), MinLength(3)]
            public string Username { get; set; }


            [Required]
            [EmailAddress]
            [RegularExpression("^[a-zA-Z0-9._%+-]+@(gmail\\.com|yahoo\\.com|outlook\\.com)$" , ErrorMessage ="Invalid Email Structure")]
            public string Email { get; set; }


            [Required]
            [Phone, MaxLength(11)]
            [RegularExpression("^(011|012|010|015)\\d{8}$" , ErrorMessage ="Invalid Phone Number")]
            public string PhoneNumber { get; set; }


            [Required]
            [DataType(DataType.Password)]
            [RegularExpression("^(?=.*[A-Z])(?=.*\\d)(?=.*[^A-Za-z\\d]).{6,}$", ErrorMessage = "Password must have at least one digit, uppercase char, unique char, and length minimum 6")]
            public string Password { get; set; }


            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match")]
            public string ConfirmPassword { get; set; }


            public int[]? IntakesIDs { get; set; }

            public int? IntakeID { get; set; }


            public int? TrackID { get; set; }
            //public int TrackssID { get; set; }
        }



        public async Task OnGetAsync(string returnUrl = null)
        {
            TempData["showSuccessAlert"] = false; // Initialize it as false initially
            ReturnUrl = returnUrl;

            var AllIntakes = intakeRepository.GetCurrentAvIntakes();

            ViewData["AllIntakes"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(AllIntakes, "ID", "NameAndDuration");
        }


        public PartialViewResult OnGetTracks(int intakeID)
        {
            var AllTracks = trackRepository.GetTracksByIntakeID(intakeID);

            return Partial("_TracksParialView", AllTracks);
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/Identity/Account/AddUser");

            var user = CreateUser();

            user.FullName = Input.FullName;
            user.PhoneNumber = Input.PhoneNumber;
            await _userStore.SetUserNameAsync(user, Input.Username, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

            var result = await _userManager.CreateAsync(user, Input.Password); //new instructor created in AspNetUsers table.


            if (result.Succeeded)
            {
                user.LockoutEnabled = false;

                if (Input.userType == UserType.Admin)
                {
                    string NewAdminID = await _userManager.GetUserIdAsync(user); //get the new added Admin id

                    Admin newAdmin = new()
                    {
                        AspNetUserID = NewAdminID
                    };

                    adminRepository.CreateAdmin(newAdmin);
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
                else if (Input.userType == UserType.Instructor)
                {
                    //1
                    string NewInstructorID = await _userManager.GetUserIdAsync(user); //get the new added instructor id

                    Instructor newInstructor = new Instructor()
                    {
                        AspNetUserID = NewInstructorID,
                        CreationDate = DateTime.Now,
                        AdminID = _userManager.GetUserId(User),
                    };
                    instructorRepository.CreateInstructor(newInstructor); //add the instructor to the Instructor Table in db

                    //2 
                    if (Input.IntakesIDs != null)
                    {
                        foreach (int intakeID in Input.IntakesIDs)
                        {
                            intakeInstructorRepository.AddIntake_Instructor(intakeID, NewInstructorID); //add the instructor and intake to the Intake_Instructor Table in db
                        }
                    }

                    //3
                    await _userManager.AddToRoleAsync(user, "Instructor"); //assign his role
                }
                else
                {
                    string newStudentID = await _userManager.GetUserIdAsync(user); //get the new added instructor id

                    if (Input.IntakeID != null && Input.TrackID != null)
                    {
                        Student std = new Student()
                        {
                            AspNetUserID = newStudentID,
                            IsGraduated = false,
                            CreationDate = DateTime.Now,
                            IntakeID = Input.IntakeID ?? default(int),
                            TrackID = Input.TrackID ?? default(int),
                            AdminID = _userManager.GetUserId(User)
                        };
                        studentRepository.CreateStudent(std);

                        var courseIDs = intake_Track_CourseRepository.getCoursesForTrack(Input.TrackID,Input.IntakeID);

                        student_CourseRepository.CreateStudent_Course(courseIDs, newStudentID);
                    }


                    await _userManager.AddToRoleAsync(user, "Student");
                }

                #region NN
                //if (await _userManager.IsInRoleAsync(user, "Admin"))
                //{
                //    Admin newAdmin = new()
                //    {
                //        AspNetUserID = user.Id,
                //    };

                //    adminRepository.CreateAdmin(newAdmin);
                //} 
                #endregion

                //await _signInManager.SignInAsync(user, isPersistent: false); //create cookie 

                return LocalRedirect(returnUrl);
            }

            // Show an alert if the form is not valid.
            //ClientScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('The form is not valid.');", true);


            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }



        private AppUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<AppUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(AppUser)}'. " +
                    $"Ensure that '{nameof(AppUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }


        private IUserEmailStore<AppUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<AppUser>)_userStore;
        }
    }

}
