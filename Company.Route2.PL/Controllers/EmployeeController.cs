
using AutoMapper;
using Company.Route2.BLL.Interfaces;
using Company.Route2.BLL.Repositories;
using Company.Route2.BLL.UnitOfWork;
using Company.Route2.DAL.Models;
using Company.Route2.PL.ModelViews;
using Company.Route2.PL.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Authorization;

namespace Company.Route2.PL.Controllers
{
	[Authorize]
	public class EmployeeController : Controller
    {
        // private readonly IEmployeeRepository _employeeRepository;
        // private readonly IDepartementRepository _departementRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public EmployeeController(
            //IEmployeeRepository _employeeRepository,
            //IDepartementRepository _departementRepository,
            IUnitOfWork _unitOfWork,
            IMapper mapper
            )
        {
            // this._employeeRepository = _employeeRepository;
            // this._departementRepository = _departementRepository;
            this._unitOfWork = _unitOfWork;
            this.mapper = mapper;
        }


        public async Task<IActionResult> Index(string SearchWord)
        {
            var Employees = Enumerable.Empty<Employees>();
            if (string.IsNullOrEmpty(SearchWord))
            {
                 Employees = await _unitOfWork.EmployeeRepository.GetAllAsync();

            }else
            {
                Employees = await _unitOfWork.EmployeeRepository.GetByNameAsync(SearchWord);
            }

            var Result=mapper.Map<IEnumerable<EmployeeViewModel>>(Employees);  
            
            // 2 viewbag
            ViewBag.message2 = "Thats Our Employees ";

            return View(Result);

        }
        public async Task<IActionResult> Create()
        {
            var departments=  await _unitOfWork.DepartementRepository.GetAllAsync();
           // var Result=mapper.Map<IEnumerable< DepartmentViewModel>>(departments);
            ViewData["department"] = departments;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            var departments = await _unitOfWork.DepartementRepository.GetAllAsync();
            // var Result=mapper.Map<IEnumerable< DepartmentViewModel>>(departments);
            ViewData["department"] = departments;
            if (ModelState.IsValid)

            {
                if(model.Image is not null)
                {
                    model.ImageName = DocumentSetting.Upload(model.Image, "Images");
                }
                //Employees employees = new Employees()
                //{
                //    Id = model.Id,
                //    Name = model.Name,
                //    Email = model.Email,
                //    Address = model.Address,
                //    Age = model.Age,
                //    PhoneNumber = model.PhoneNumber,
                //    Salary = model.Salary,
                //    IsActive = model.IsActive,
                //    DateOfHiring = model.DateOfHiring,
                //    WorkForId = model.WorkForId,
                //    WorkFor=model.WorkFor
                //};

                var employees = mapper.Map<Employees>(model);
                await _unitOfWork.EmployeeRepository.CreateAsync(employees);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                    TempData["message3"] = "emmployee created sucessful";
                else
                    TempData["message3"] = "emmployee not created ";

                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task< IActionResult> Detials(int? id, string ViewName = "Detials")
        {
            var departments = await _unitOfWork.DepartementRepository .GetAllAsync();
            var result = mapper.Map<IEnumerable<Departement>>(departments);
            ViewData["department"] = result;
            if (id is null) return BadRequest();

            var model = await _unitOfWork.EmployeeRepository.GetAsync(id);

            //EmployeeViewModel employees = new EmployeeViewModel()
            //{
            //    Id = model.Id,
            //    Name = model.Name,
            //    Email = model.Email,
            //    Address = model.Address,
            //    Age = model.Age,
            //    PhoneNumber = model.PhoneNumber,
            //    Salary = model.Salary,
            //    IsActive = model.IsActive,
            //    DateOfHiring = model.DateOfHiring,
            //    WorkForId = model.WorkForId,
            //    WorkFor = model.WorkFor
            //};
            var employees = mapper.Map < EmployeeViewModel>(model) ;
            if (model is null) return NotFound();

            return View(employees);
        }
        public Task<IActionResult> Edit(int? id)
        {
           
            return Detials(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, EmployeeViewModel model)
        {
            var departments = await _unitOfWork.DepartementRepository.GetAllAsync();

            var Departments = mapper.Map<IEnumerable<DepartmentViewModel>>(departments);

            ViewData["department"] = Departments;

            if (id != model.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                if (model.ImageName is not null)
                {
                    DocumentSetting.Delete(model.ImageName, "Images");
                }
                if (model.Image is not null)
                {
                   model.ImageName= DocumentSetting.Upload(model.Image, "Images");

                }

                //Employees employees = new Employees()
                //{
                //    Id = model.Id,
                //    Name = model.Name,
                //    Email = model.Email,
                //    Address = model.Address,
                //    Age = model.Age,
                //    PhoneNumber = model.PhoneNumber,
                //    Salary = model.Salary,
                //    IsActive = model.IsActive,
                //    DateOfHiring = model.DateOfHiring,
                //    WorkForId = model.WorkForId,
                //    WorkFor = model.WorkFor
                //};
                var employees = mapper.Map<Employees>(model);
               _unitOfWork.EmployeeRepository.Update(employees);
                var result = await _unitOfWork.SaveChangesAsync();

                if (result > 0)
                { 
                   
                    return RedirectToAction("Index");

                }

            }
            return View(model);
        }
        [HttpGet]

        public Task<IActionResult> Delete(int? id)
        {
            return Detials(id, "Delete");
        }


        public async Task<IActionResult> Delete([FromRoute] int id, EmployeeViewModel model)
        {

            if (id != model.Id) return BadRequest();
            if (model.ImageName is not null)
                DocumentSetting.Delete(model.ImageName, "Images");

            //Employees employees = new Employees()
            //{
            //    Id = model.Id,
            //    Name = model.Name,
            //    Email = model.Email,
            //    Address = model.Address,
            //    Age = model.Age,
            //    PhoneNumber = model.PhoneNumber,
            //    Salary = model.Salary,
            //    IsActive = model.IsActive,
            //    DateOfHiring = model.DateOfHiring,
            //    WorkForId = model.WorkForId,
            //    WorkFor = model.WorkFor
            //};
            var employees=mapper.Map<Employees>(model);
           _unitOfWork.EmployeeRepository.Delete(employees);
            var result = await _unitOfWork.SaveChangesAsync();

            if (result > 0)
            {
                
                return RedirectToAction("Index");

            }
            return View();
        }
    }
}
