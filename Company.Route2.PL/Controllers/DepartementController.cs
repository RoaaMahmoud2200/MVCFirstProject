using AutoMapper;
using Company.Route2.BLL.Interfaces;
using Company.Route2.BLL.UnitOfWork;
using Company.Route2.DAL.Models;
using Company.Route2.PL.ModelViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.Route2.PL.Controllers
{
    [Authorize]

    public class DepartementController : Controller
    {
        // private IDepartementRepository _departementRepository;
         private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DepartementController(
           // IDepartementRepository departementRepository,
             IMapper mapper
            ,IUnitOfWork _unitOfWork)
        {
            // _departementRepository = departementRepository;
            this._mapper = mapper;
           this. _unitOfWork = _unitOfWork;
        }

        [HttpGet]
        public async Task< IActionResult> Index()
        {
            var departements = await _unitOfWork.DepartementRepository.GetAllAsync();
            // 1 viewdata
            ViewData["message"] = "Thats Our Great Departement ";
           var result= _mapper.Map<IEnumerable<DepartmentViewModel>>(departements);
            return View(result);
        }
        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Create(DepartmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Departement departement = new Departement()
                //{
                //    Id = model.Id,
                //    Name = model.Name,
                //    Code = model.Code,
                //    DateOfCreation = model.DateOfCreation,
                //    Employees = model.Employees,
                //};

                var result = _mapper.Map<Departement>(model);

                await _unitOfWork.DepartementRepository.CreateAsync(result);
                var count =  await _unitOfWork.SaveChangesAsync();


                if (count > 0)
                {
                    TempData["message4"] = "Departement created sucessful";
                }
                else
                {
                    TempData["message4"] = "Departement not created ";
                }

                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]

        public async Task<IActionResult> Detials(int? id, string viewName= "Detials")
        {
            if (id is null) return BadRequest();

            var model = await _unitOfWork.DepartementRepository.GetAsync(id);


            //DepartmentViewModel departement = new DepartmentViewModel()
            // {
            //     Id = model.Id,
            //     Name = model.Name,
            //     Code = model.Code,
            //     DateOfCreation = model.DateOfCreation,
            //     Employees = model.Employees,
            // };
            var result = _mapper.Map<DepartmentViewModel>(model);

            if (result is null) return NotFound();
            return View(result);

        }

        [HttpGet]
        public Task<IActionResult> Edit(int? id)
        {

            return Detials(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int? id ,DepartmentViewModel model)
        {

             if (id != model.Id) return BadRequest();

             if (ModelState.IsValid)

            {   
               

                //   Departement departement = new Departement()
                //   {
                //       Id = model.Id,
                //       Name = model.Name,
                //       Code = model.Code,
                //       DateOfCreation = model.DateOfCreation,
                //       Employees = model.Employees,
                //  };

                var result = _mapper.Map<Departement>(model);


                _unitOfWork.DepartementRepository.Update(result);
                var count = await _unitOfWork.SaveChangesAsync();

                 if (count > 0)
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

        public async Task<IActionResult> Delete( [FromRoute] int? id , DepartmentViewModel model)
        {
            if (id != model.Id) return BadRequest();

            //Departement departement = new Departement()
            //{
            //    Id = model.Id,
            //    Name = model.Name,
            //    Code = model.Code,
            //    DateOfCreation = model.DateOfCreation,
            //    Employees = model.Employees,
            //};

            var result = _mapper.Map<Departement>(model);

            _unitOfWork.DepartementRepository.Delete(result);
            var count= await _unitOfWork.SaveChangesAsync();   

            if (count == 0) return View();
            return RedirectToAction("Index");
        }

    }
}
