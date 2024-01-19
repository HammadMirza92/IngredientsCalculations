using IngredientsCalculations.Models;
using IngredientsCalculations.Services.IRepository;
using IngredientsCalculations.Services.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IngredientsCalculations.Controllers
{
    public class ProductIngredientsController : Controller
    {
        private readonly IProductIngredientsRepository _productIngredientsRepository;
        private readonly IProductRepository _productRepository;
        private readonly IIngredientsRepository _ingredientsRepository;
        public ProductIngredientsController(IProductIngredientsRepository productIngredientsRepository, IProductRepository productRepository, IIngredientsRepository ingredientsRepository)
        {
            _productIngredientsRepository = productIngredientsRepository;
            _productRepository = productRepository;
            _ingredientsRepository = ingredientsRepository;
        }
        // GET: ProductIngredientsController
        public async Task<ActionResult<ProductIngredients>> Index(int? page)
        {
            int pageSize = 10; // adjust as needed
            int pageNumber = (page ?? 1);

            var productIngredients = await _productIngredientsRepository.GetAll();

            var paginatedProducts = productIngredients.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling((double)productIngredients.Count() / pageSize);

            return View(paginatedProducts);

        }

        // GET: ProductIngredientsController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var productIngredients = await _productIngredientsRepository.GetById(id);

            if (productIngredients == null)
            {
                return NotFound();
            }

            return View(productIngredients);
        }

        // GET: ProductIngredientsController/Create
        public async Task<IActionResult> Create()
        {
            await PopulateDropdowns();
            return View();
        }

        // GET: ProductIngredientsController/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var productIngredients = await _productIngredientsRepository.GetById(id);

            if (productIngredients == null)
            {
                return NotFound();
            }

            await PopulateDropdowns();
            return View(productIngredients);
        }




        // GET: ProductIngredientsController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var productIngredients = await _productIngredientsRepository.GetById(id);

            if (productIngredients == null)
            {
                return NotFound();
            }

            return View(productIngredients);
        }

        // POST: ProductIngredientsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, ProductIngredients productIngredients)
        {
            if (id != productIngredients.Id)
            {
                return NotFound();
            }
                       
            await _productIngredientsRepository.Update(productIngredients);
            await PopulateDropdowns();
            return RedirectToAction(nameof(Index));
            
        }

        // POST: ProductIngredientsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductIngredients productIngredients)
        {
           
             await _productIngredientsRepository.Add(productIngredients);
             await PopulateDropdowns();

            return RedirectToAction(nameof(Index));
           
        }

        // POST: ProductIngredientsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            var productIngredients = await _productIngredientsRepository.GetById(id);

            if (productIngredients == null)
            {
                return NotFound();
            }

            await _productIngredientsRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateDropdowns()
        {
            ViewBag.Products = await _productRepository.GetAll();
            ViewBag.Ingredients = await _ingredientsRepository.GetAll();
        }
    }
}
