using IngredientsCalculations.Models;
using IngredientsCalculations.Services.IRepository;
using IngredientsCalculations.Services.Repository;
using Microsoft.AspNetCore.Mvc;
using PagedList;

namespace IngredientsCalculations.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICurrencyRepository _currencyRepository;

        public ProductController(IProductRepository productRepository, ICurrencyRepository currencyRepository)
        {
            _productRepository = productRepository;
            _currencyRepository = currencyRepository;

        }
        // GET: ProductController
        public async Task<ActionResult<Product>> Index(int? page)
        {
            int pageSize = 10; // adjust as needed
            int pageNumber = (page ?? 1);

            var products = await _productRepository.GetAll();

            var paginatedProducts = products.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling((double)products.Count() / pageSize);
            await PopulateDropdowns();
            return View(paginatedProducts);
        }

        // GET: ProductController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var product = await _productRepository.GetById(id);

            if (product == null)
            {
                return NotFound();
            }
            await PopulateDropdowns();
            return View(product);
        }

        // GET: ProductController/Create
        public async Task<IActionResult> Create()
        {
            await PopulateDropdowns();
            return View();
        }

        // GET: ProductController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var product = await _productRepository.GetById(id);

            if (product == null)
            {
                return NotFound();
            }
            await PopulateDropdowns();
            return View(product);
        }

        // GET: ProductController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var product = await _productRepository.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            await _productRepository.Update(product);
            return RedirectToAction(nameof(Index));
            

            return View(product);
        }
        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product)
        {
          
                await _productRepository.Add(product);
                return RedirectToAction(nameof(Index));
           

            return View(product);
        }
        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            await _productRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateDropdowns()
        {
            Currency baseCurrency = await _currencyRepository.GetBaseCurrency();
            ViewBag.BaseCurrency = baseCurrency;
        }
    }
}
