
using BulkyBook.DataAcces.Repository.IRepository;
using BulkyBook.DataAccess;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll();
            return View(objCategoryList);
        }


        //GET
        public IActionResult Create()
        {
            return View();
        }



        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            //This is a custom validation
            if (category.Name == category.DisplayOrder.ToString())
            {
                //This error will peresnt on the asp-validation-summary erea
                //ModelState.AddModelError("CustomError", "The DisplayOrder cannot exactly match the Name.");

                //This error will peresnt on the asp-validation-for="Name"
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }


            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(category);
                _unitOfWork.Save();
                TempData["success"] = "Category created succefully";
                return RedirectToAction("Index");
            }

            return View(category);
        }





        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //The diffrence between the methods is the less compless script
            //becouse we are using with int we it is ok to use use find

            var categoryFromDB = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);
            //var categoryFromDB = categoryRepository.Categories.Find(id);
            if (categoryFromDB == null)
            {
                return NotFound();
            }


            return View(categoryFromDB);
        }







        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {

            //This is a custom validation
            if (category.Name == category.DisplayOrder.ToString())
            {
                //This error will peresnt on the asp-validation-summary erea
                //ModelState.AddModelError("CustomError", "The DisplayOrder cannot exactly match the Name.");

                //This error will peresnt on the asp-validation-for="Name"
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }


            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(category);
                _unitOfWork.Save();
                TempData["success"] = "Category updated succefully";
                return RedirectToAction("Index");
            }

            return View(category);
        }






        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //The diffrence between the methods is the less compless script
            //becouse we are using with int we it is ok to use use find

            var categoryFromDB = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);
            //var categoryFromDB = categoryRepository.Categories.Find(id);
            if (categoryFromDB == null)
            {
                return NotFound();
            }


            return View(categoryFromDB);
        }







        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var categoryFromDB = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);

            if (categoryFromDB == null)
            {
                return NotFound();
            }



            _unitOfWork.Category.Remove(categoryFromDB);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted succefully";
            return RedirectToAction("Index");



        }






    }
}
