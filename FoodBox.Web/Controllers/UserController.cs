using BarkodDProject.Web.Controllers;
using FoodBox.Core.Models;
using FoodBox.Core.Services;
using FoodBox.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Management.SqlParser.Metadata;
namespace FoodBox.Web.Controllers
{
    public class UserController : BaseProcess<Employee, IEmployeeService>
	{
		private readonly UserManager<Employee> _userManager;
		private readonly SignInManager<Employee> _signInManager;
		private readonly IEmployeeService _employeeService;
		private readonly RoleManager<IdentityRole> _roleManager;

		public UserController(UserManager<Employee> userManager, SignInManager<Employee> signInManager, IEmployeeService employeeService, RoleManager<IdentityRole> roleManager)
			: base(employeeService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_employeeService = employeeService;
			_roleManager = roleManager;
		}

		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Login", "Users");
		}

		[HttpGet]
		public async Task<IActionResult> Login()
		{

			//Şifre Admin  : 123456+kV465568*

			//Şifre Uretim : U2T2v99h4YHK

			//Şifre Monzer : 9p2EKDwyv89M**
			return View();
		}

		[HttpPost]
		public async override Task<IActionResult> Create(Employee entity)
		{
			try
			{

				//Kullanıcı bilgileri set edilir.
				Employee user = new Employee();

				user.FirstName = entity.FirstName;
				user.LastName = entity.LastName;
				user.Email = entity.Email.Trim();
				user.UserName = entity.Email.Trim();
			
				//Kullanıcı oluşturulur.
				IdentityResult result = await _userManager.CreateAsync(user, entity.PasswordHash.Trim());

				//Kullanıcı oluşturuldu ise
				if (result.Succeeded)
				{
					bool roleExists = await _roleManager.RoleExistsAsync("User");

					if (!roleExists)
					{
						IdentityRole role = new IdentityRole("User");
						role.NormalizedName = "User";

						_roleManager.CreateAsync(role).Wait();
					}

					//Kullanıcıya ilgili rol ataması yapılır.
					_userManager.AddToRoleAsync(user, "User").Wait();

					//todo rol içerde yoksa oluşturulacak.

				}

				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest();
			}

			//string password = entity.PasswordHash;
			//entity.PasswordHash = null;
			//var result = await _userManager.CreateAsync(entity, password);
			//await _unitOfWork.Users.AddAsync(entity);

			//await _unitOfWork.CommitAsync();

			//return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(UserSingInUpViewModel user, string returnUrl = null)
		{
			if (ModelState.IsValid)
			{
				var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, false, false);
				if (result.Succeeded)
				{
					if (returnUrl != null)
						return LocalRedirect(returnUrl);
					else
						return RedirectToAction("Index", "Home");
				}
				else
				{
					return View();
				}
			}
			return View();
		}
	}
}
