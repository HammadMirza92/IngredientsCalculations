using IngredientsCalculations.Models;
using IngredientsCalculations.Services.IRepository;
using IngredientsCalculations.Services.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IngredientsCalculations.Controllers
{
    public class IngredientsController : Controller
    {

        private readonly IIngredientsRepository _ingredientsRepository;
        public IngredientsController(IIngredientsRepository ingredientsRepository)
        {
            _ingredientsRepository = ingredientsRepository;
        }

        // GET: IngredientsController
        public async Task<ActionResult<Ingredients>> Index(int? page)
        {
            int pageSize = 10; // adjust as needed
            int pageNumber = (page ?? 1);

            var ingredients = await _ingredientsRepository.GetAll();

            var paginatedProducts = ingredients.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling((double)ingredients.Count() / pageSize);

            return View(paginatedProducts);
        }

        // GET: IngredientsController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var ingredients = await _ingredientsRepository.GetById(id);

            if (ingredients == null)
            {
                return NotFound();
            }

            return View(ingredients);
        }

        // GET: IngredientsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: IngredientsController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var ingredients = await _ingredientsRepository.GetById(id);

            if (ingredients == null)
            {
                return NotFound();
            }

            return View(ingredients);
        }

        // GET: IngredientsController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var ingredients = await _ingredientsRepository.GetById(id);

            if (ingredients == null)
            {
                return NotFound();
            }

            return View(ingredients);
        }

        // POST: IngredientsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Ingredients ingredients)
        {

            await _ingredientsRepository.Add(ingredients);
            return RedirectToAction(nameof(Index));
       

            return View(ingredients);
        }
        // POST: IngredientsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, Ingredients ingredients)
        {
            if (id != ingredients.Id)
            {
                return BadRequest();
            }

      
            await _ingredientsRepository.Update(ingredients);
            return RedirectToAction(nameof(Index));
           

            return View(ingredients);
        }
        // POST: IngredientsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            await _ingredientsRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
