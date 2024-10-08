using AutoMapper;
using Company.Route2.DAL.Models;
using Company.Route2.PL.Helper;
using Company.Route2.PL.ModelViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace Company.Route2.PL.Controllers
{
    [Authorize]

    public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public UserController( UserManager<ApplicationUser> _userManager,SignInManager<ApplicationUser> _signInManager) 
		{
			this._userManager = _userManager;
			this._signInManager = _signInManager;
		}	

		public async Task< IActionResult> Index(string? searchKeyWord)
		{
			var user = Enumerable.Empty<UserViewModel>();

			if(string.IsNullOrEmpty(searchKeyWord))
			{
				user = await _userManager.Users.Select(u => new UserViewModel()
				{
					Id=u.Id,	
					Fname = u.Fname,
					Lname = u.Lname,
					Email = u.Email,
					Roles =  _userManager.GetRolesAsync(u).Result
				}).ToListAsync();
			}
			else
			{
				user= await _userManager.Users.Where(u=>u.Email
			                                               .ToLower()
			                                               .Contains(searchKeyWord.ToLower()))
			                                               .Select(u => new UserViewModel()
			                                               {
			                                               		Fname = u.Fname,
			                                               		Lname = u.Lname,
			                                               		Email = u.Email,
			                                               		Roles = _userManager.GetRolesAsync(u).Result
			                                               }).ToListAsync();


			}
			return View(user);	
		}


		// Details
		public async Task< IActionResult> Detials(string? id, string ViewName= "Detials")
		{
			if (string.IsNullOrEmpty(id))
				return BadRequest();
			var userFromDB = await _userManager.FindByIdAsync(id);
			if (userFromDB is null)
				return NotFound();
			var user = new UserViewModel()
			{
				Id = userFromDB.Id,
				Fname = userFromDB.Fname,
				Lname = userFromDB.Lname,
				Email = userFromDB.Email,
				Roles = _userManager.GetRolesAsync(userFromDB).Result

			};

            return View(user);
		}

        public Task<IActionResult> Edit(string? id)
        {

            return Detials(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, UserViewModel model)
        {

            if (id != model.Id) return BadRequest();
            if (ModelState.IsValid)
            {


                var userFromDB = await _userManager.FindByIdAsync(id);
                if (userFromDB is null)
                    return NotFound();

                    userFromDB.Fname = model.Fname;
                    userFromDB.Lname = model.Lname;
                    userFromDB.Email = model.Email;


                var result= await _userManager.UpdateAsync(userFromDB);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

            }
            return View(model);
        }
        [HttpGet]

        public Task<IActionResult> Delete(string? id)
        {
            return Detials(id, "Delete");
        }


        public async Task<IActionResult> Delete([FromRoute] string? id, UserViewModel model)
        {

            if (id != model.Id) return BadRequest();
            var userFromDB = await _userManager.FindByIdAsync(id);
            if (userFromDB is null)
                return NotFound();

            var result = await _userManager.DeleteAsync(userFromDB);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
