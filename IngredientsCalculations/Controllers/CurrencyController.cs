using IngredientsCalculations.Models;
using IngredientsCalculations.Services.IRepository;
using IngredientsCalculations.Services.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IngredientsCalculations.Controllers
{
    public class CurrencyController : Controller
    {
        private readonly ICurrencyRepository _currencyRepository;
        public CurrencyController(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }
        // GET: CurrencyController
        public async Task<ActionResult<Currency>> Index()
        {
            var allCurrencies = await _currencyRepository.GetAll();
            return View(allCurrencies);
        }

        // GET: CurrencyController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var currency = await _currencyRepository.GetById(id);

            if (currency == null)
            {
                return NotFound();
            }

            return View(currency);
        }

        // GET: CurrencyController/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: CurrencyController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var currency = await _currencyRepository.GetById(id);

            if (currency == null)
            {
                return NotFound();
            }
            var allCurrencies = await _currencyRepository.GetAll();
            currency.SetAsBaseCurrency(allCurrencies.ToList()); // Convert IEnumerable to List

            await _currencyRepository.Update(currency);

            return View(currency);
        }

        // GET: CurrencyController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var currency = await _currencyRepository.GetById(id);

            if (currency == null)
            {
                return NotFound();
            }

            return View(currency);
        }

        // POST: CurrencyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, Currency currency)
        {
            if (id != currency.Id)
            {
                return BadRequest();
            }
            var allCurrencies = await _currencyRepository.GetAll();
            currency.SetAsBaseCurrency(allCurrencies.ToList()); // Convert IEnumerable to List

            await _currencyRepository.Update(currency);
            return RedirectToAction(nameof(Index));
            

            return View(currency);
        }

        // POST: CurrencyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Currency currency)
        {

            await _currencyRepository.Add(currency);
            return RedirectToAction(nameof(Index));
       

            return View(currency);
        }

        // POST: CurrencyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            await _currencyRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
