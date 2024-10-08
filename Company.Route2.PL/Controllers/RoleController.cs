using Company.Route2.DAL.Models;
using Company.Route2.PL.ModelViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Company.Route2.PL.Controllers
{
    [Authorize(Roles="admin")]

    public class RoleController  : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager)
        {
            this._userManager = _userManager;
            this._roleManager = _roleManager;
        }

        public async Task<IActionResult> Index(string? searchKeyWord)
        {
            var role = Enumerable.Empty<RoleViewModel>();

            if (string.IsNullOrEmpty(searchKeyWord))
            {
                role =  _roleManager.Roles.Select(u => new RoleViewModel()
                {
                    Id = u.Id,
                    Name = u.Name
                }).ToList();
            }
            else
            {
                role =  _roleManager.Roles.Where(u => u.Name
                                                           .ToLower()
                                                           .Contains(searchKeyWord.ToLower()))
                                                           .Select(u => new RoleViewModel()
                                                           {
                                                               Id = u.Id,
                                                               Name = u.Name
                                                           }).ToList();


            }
            return View(role);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();

    }
        [HttpGet]
        public async Task< IActionResult> AddOrRemoveRole(string? Id)
        {
            if (Id is null) return BadRequest();
            var RoleFromDB = await _roleManager.FindByIdAsync(Id);
            if (RoleFromDB is null)
                return NotFound();
            TempData["RoleId"] = RoleFromDB.Id;

            var Users = await _userManager.Users.ToListAsync();
            var Result = new List<UserInRoleViewModel>();
            foreach(var User in Users)
            {
                var userRoleViewModel = new UserInRoleViewModel()
                {
                    Id = User.Id,
                    Name = User.UserName,
                };
                if(await _userManager.IsInRoleAsync(User, RoleFromDB.Name))
                {
                    userRoleViewModel.IsSelected=true;  
                }else if(! await _userManager.IsInRoleAsync(User, RoleFromDB.Name))
                {
                    userRoleViewModel.IsSelected = false;
                }

                Result.Add(userRoleViewModel);
            }
            return View(Result);

        }
        [HttpPost]
        public async Task<IActionResult> AddOrRemoveRole(string? Id, List< UserInRoleViewModel> Users)
        {
            if (Id is null) return BadRequest();
            var RoleFromDB = await _roleManager.FindByIdAsync(Id);
            if (RoleFromDB is null)
                return NotFound();
            var result = new IdentityResult();
            if (ModelState.IsValid)
            {
                foreach(var user in Users)
                {
                    var userFromDb= await _userManager.FindByIdAsync(user.Id);
                    if(userFromDb is null)
                        return NotFound();
                    if (user.IsSelected && ! await _userManager.IsInRoleAsync(userFromDb,RoleFromDB.Name))
                    {
                       result=  await _userManager.AddToRoleAsync(userFromDb, RoleFromDB.Name);
                        
                    }else if (!user.IsSelected && await _userManager.IsInRoleAsync(userFromDb, RoleFromDB.Name))
                    {
                        result = await _userManager.RemoveFromRoleAsync(userFromDb, RoleFromDB.Name);
                    }
                }
                return RedirectToAction("Edit", new { Id = Id });

            }
            return View(Users);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( RoleViewModel model)
        {

            if (ModelState.IsValid)
            {
                var role = new IdentityRole()
                {
                   Name = model.Name
                };
               
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

            }
            return View(model);
        }
        // Details
        
        public async Task<IActionResult> Detials(string? id, string ViewName = "Detials")
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();
            var RoleFromDB = await _roleManager.FindByIdAsync(id);
            if (RoleFromDB is null)
                return NotFound();
            var role = new RoleViewModel()
            {
                Id = RoleFromDB.Id,
                Name = RoleFromDB.Name

            };
            return View(role);
        }

        public Task<IActionResult> Edit(string? id)
        {

            return Detials(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleViewModel model)
        {

            if (id != model.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                var RoleFromDB = await _roleManager.FindByIdAsync(id);
                if (RoleFromDB is null)
                    return NotFound();

                RoleFromDB.Name = model.Name;

                var result = await _roleManager.UpdateAsync(RoleFromDB);
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


        public async Task<IActionResult> Delete([FromRoute] string? id, RoleViewModel model)
        {

            if (id != model.Id) return BadRequest();
            var RoleViewModel = await _roleManager.FindByIdAsync(id);
            if (RoleViewModel is null)
                return NotFound();

            var result = await _roleManager.DeleteAsync(RoleViewModel);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }


    }
}
