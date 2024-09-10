using Identity1.Data;
using Identity1.Models;
using Identity1.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Identity1.Controllers
{
    [Authorize(Roles ="Admin,SuperAdmin") ] // تعني ممنوع اي حدا يدخل على هاد الكلاس الا اذا كان مسجل دخوله وكان نوعهم اما ادمن او سوبر ادمن
    public class AccountsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager; // ApplicationUser جديدة هو ما بقدر يتعرف عليها, مشان هيك انا ضفت وخليته يورث من  att بورث منها بالاضافة بدي اضيف كم  ApplicationUser لكن عشان UserManager<IdentityUser> userManager; هو بالاصل 
        private readonly SignInManager<ApplicationUser> signInManager;
		private readonly RoleManager<IdentityRole> roleManager;

		public AccountsController(ApplicationDbContext context,UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
			this.roleManager = roleManager;
		}
        [AllowAnonymous] // مسموح للناس المجهولة
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task <IActionResult> Register(RegisterViewModel model)  // RegisterViewModel اذا نوع الباراميتر هو @model Identity1.Models.ViewModel.RegisterViewModel الي اله وبقرا اول سطر بشوف انو View عشان اعرف شو بستقبل : بروح على  
        {   // DB لا يستطيع التعامل مع  ViewModel لانه بس RegisterViewModel لكن هذا لا يمكن مع DB انا هون بدي اتعامل مع 
            ApplicationUser user = new ApplicationUser() // وارث منه , بكون جده ApplicationDbContext الي ApplicationUser وهذا يتوافر مع كلاس DomainModel لازم يكون من نوع  DB عشان يضيف على
            {
                Email = model.Email,
                PhoneNumber = model.Phone,
                UserName = model.Email,
                City = model.City,         // بفحص من حاله تلقائي الباسورد RegisterViewModel ما بنعطيه الباسورد, لانه في 
                Gender = model.Gender
            };

            //هاي الجملة الي تحت بتعمل اضافة يوزر
            var result = await userManager.CreateAsync(user,model.Password); // عشان يوخد الباسورد ويشوفها, عشان يعملها عملية تشفير

            if(result.Succeeded)
            {
                await userManager.AddToRoleAsync(user,"User"); // Register بمجرد ما ضفت يوزر وعملت , User قيمتها user الى Role معناها بدي اضيف 
                return RedirectToAction("Login");
            }

            return View(model);
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model) 
        {
            var result = await signInManager.PasswordSignInAsync(model.Email,model.Password,model.RememberMe,false);
            if (result.Succeeded) 
            {
                return RedirectToAction("Index","Home");
            }
            return View(model);
        }
        
        public IActionResult GetUsers() // ApplicationUser بدي يوزرز بس بحتووا على 4 اتربيوتس من بين كل الاتربيوتس الموجودة في 
        {
            var users = userManager.Users.ToList(); // data's here : (Domain Model)


            var userViewModel = users.Select(user => new UsersViewModel //Mapping: technique(1) => from DomainModel to ViewModel
            {
                Id = user.Id,
                Email = user.Email,             // UserManager<IdentityUser> هو هيك اصلا
                Phone = user.PhoneNumber,       // IdentityUser بورث من  ApplicarionUser لكن عشان احنا عاملين كلاس 
                UserName = user.Email,          // ApplicarionUser نوعه user فهو تلقائيا بعرف انو 
                City = user.City,
                Roles = userManager.GetRolesAsync(user).Result // user لكل Roles هاي معناها : اجيب
            }).ToList();
            return View(userViewModel); // data's here : (View Model)




            //List<UsersViewModel> UsersVm= new List<UsersViewModel>(); // List هون عندي اكثر من يوزر لازم اعملهم ب

            //foreach (var user in users)  // Mapping : technique (2) => from DomainModel to ViewModel
            //{
            //    UsersViewModel userModel = new UsersViewModel()
            //    {
            //        Email = user.Email,
            //        Phone=user.PhoneNumber,
            //        UserName=user.Email, // ApplicationUser في  username ما في عنا 
            //        City=user.City
            //    };

            //    UsersVm.Add(userModel); // List بضيف كل Object بعمله على ال
            //}
            //return View(UsersVm); // data's here : (View Model)


        }


        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleViewModel model)
        {
            IdentityRole role = new IdentityRole() {  // foreach أو  Select وحدة بس , اذا ما بلزم  Role بدي اضيف
                Name = model.RoleName                 // DB للقيمة الجديدة عشانه بتعامل هو مع  IdentityRole لازم اغير الاسم الموجود في
            };

            var result = await roleManager.CreateAsync(role); 
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(RolesList)); // الي عندي Roles بدي اعرض ال Role لما بنشئ 
            }

            return View(model);
        }

        public IActionResult RolesList()
        {
            var roles = roleManager.Roles.ToList(); // View Model بدي احولها الى Domain Model هاي عبارة عن 

            var userViewModel = roles.Select(role => new RoleViewModel 
            {
                RoleName = role.Name // RoleViewModel الجاي من الاكشن الي قبله ل IdentityRole بدنا نعطي الاسم الي جاي من 
            }).ToList();

            return View(userViewModel);
        }

        public IActionResult EditUserRole(string id) // id : GetUsers.cshtml رح تيجي من
		{                                            //<a asp-controller="Accounts" asp-action="EditUserRole" asp-route-id="@user.Id">Change Role</a>
            var viewModel = new EditUserRoleViewModel 
            {
                Id = id,
                RolesList = roleManager.Roles.Select(
                   role => new SelectListItem
                   {
                       Value = role.Id,
                       Text = role.Name
                   }
                    ).ToList()
            };

            return View(viewModel);
		}
        [HttpPost]
        public async Task<IActionResult> EditUserRole(EditUserRoleViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id); // بتجيب اليوزر المطابق لهاد الاي دي
            var currentRoles = await userManager.GetRolesAsync(user); // الواحد user لل Roles بتجيب جميع ال
            var result = await userManager.RemoveFromRolesAsync(user, currentRoles); // RemoveFromRolesAsync => حذف اكثر من رول , RemoveFromRoleAsync => حذف رول وحدة


            var role = await roleManager.FindByIdAsync(model.SelectedRoles); // المختار Id الجديدة حسب ال Role بتجيب ال

            await userManager.AddToRoleAsync(user, role.Name); 

            return RedirectToAction((nameof(GetUsers)));
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction((nameof(Login)));
        }

    }
}
