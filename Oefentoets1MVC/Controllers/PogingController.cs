using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Oefentoets1MVC.Data;
using Oefentoets1MVC.Data.Entities;
using Oefentoets1MVC.Models;
using Oefentoets1MVC.Utils;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Oefentoets1MVC.Controllers
{
    public class PogingController : Controller
    {
        private readonly CijferRegistratieDBContext _context;

        public PogingController(CijferRegistratieDBContext context)
        {
            _context = context;
        }

        // GET: Poging
        public async Task<IActionResult> Index()
        {
            var cijferRegistratieDBContext = _context.Pogingen.Include(p => p.Vak);
            return View(await cijferRegistratieDBContext.ToListAsync());
        }

        // GET: Poging/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pogingen == null)
            {
                return NotFound();
            }

            var poging = await _context.Pogingen
                .Include(p => p.Vak)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (poging == null)
            {
                return NotFound();
            }

            return View(poging);
        }

        // GET: Poging/Create
        public IActionResult Create()
        {
            ViewData["VakNaam"] = new SelectList(_context.Vakken, "Naam", "Naam");
            return View();
        }

        // POST: Poging/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Jaar,Resultaat,VakNaam")] Poging poging)
        {
            if (ModelState.IsValid)
            {
                // var resp = new HttpClient().GetAsync("/StudentType");
                // Console.WriteLine("Valid" + resp);
                _context.Add(poging);
                await _context.SaveChangesAsync();
                if(poging.Resultaat > 5) 
                    HttpContext.Session.AddToLog($"Vak {poging.VakNaam} is behaald!");
                HttpContext.Session.AddToLog($"Poging toegevoegd aan: {poging.VakNaam}, met resultaat: {poging.Resultaat}");
                return RedirectToAction(nameof(Index), controllerName: "Home");
            }
            ViewData["VakNaam"] = new SelectList(_context.Vakken, "Naam", "Naam", poging.VakNaam);
            return View(poging);
        }

        public async Task<IActionResult> CreateSpecial(int id)
        {
            var vak = await _context.Vakken.FindAsync(id);
            var viewModel = new CreateSpecialViewModel()
            {
                VakNaam = vak.Naam,
                VakId = vak.Id,
                Jaar = DateTime.Now.Year
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpecial(CreateSpecialViewModel viewModel)
        {

            var previous = _context.Pogingen
                .Where(p => p.VakNaam == viewModel.VakNaam)
                .OrderByDescending(p => p.Resultaat)
                .Select(p => p.Resultaat)
                .FirstOrDefault();

            // var previous = _context.Pogingen.Where(m => m.VakNaam == viewModel.VakNaam).OrderByDescending(m => m.Resultaat).FirstOrDefault(new Poging() {Resultaat = -1})?.Resultaat ?? -1;
            if(ModelState.IsValid && previous < viewModel.Resultaat)
                return RedirectToActionPreserveMethod(actionName: nameof(Create), routeValues: viewModel);
            return View(viewModel);
        }

        // GET: Poging/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pogingen == null)
            {
                return NotFound();
            }

            var poging = await _context.Pogingen.FindAsync(id);
            if (poging == null)
            {
                return NotFound();
            }
            ViewData["VakNaam"] = new SelectList(_context.Vakken, "Naam", "Naam", poging.VakNaam);
            return View(poging);
        }

        // POST: Poging/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Jaar,Resultaat,VakNaam")] Poging poging)
        {
            if (id != poging.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(poging);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PogingExists(poging.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["VakNaam"] = new SelectList(_context.Vakken, "Naam", "Naam", poging.VakNaam);
            return View(poging);
        }

        // GET: Poging/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pogingen == null)
            {
                return NotFound();
            }

            var poging = await _context.Pogingen
                .Include(p => p.Vak)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (poging == null)
            {
                return NotFound();
            }

            return View(poging);
        }

        // POST: Poging/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pogingen == null)
            {
                return Problem("Entity set 'CijferRegistratieDBContext.Pogingen'  is null.");
            }
            var poging = await _context.Pogingen.FindAsync(id);
            if (poging != null)
            {
                _context.Pogingen.Remove(poging);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PogingExists(int id)
        {
          return (_context.Pogingen?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
