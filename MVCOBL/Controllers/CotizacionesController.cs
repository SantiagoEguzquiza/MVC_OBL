using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCOBL.Models;
using Microsoft.AspNetCore.Authorization;
using API;
using Newtonsoft.Json;

namespace MVCOBL.Controllers
{
    public class CotizacionesController : Controller
    {
        private readonly MVCOBLContext _context;

        public CotizacionesController(MVCOBLContext context)
        {
            _context = context;
        }

        // GET: Cotizaciones
        [Authorize]
        public async Task<IActionResult> Index()
        {
              return _context.Cotizaciones != null ? 
                          View(await _context.Cotizaciones.Where(x => x.TipoMoneda == "USD").ToListAsync()) :
                          Problem("Entity set 'MVCOBLContext.Cotizaciones'  is null.");
        }

        // GET: Cotizaciones/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cotizaciones == null)
            {
                return NotFound();
            }

            var cotizacione = await _context.Cotizaciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cotizacione == null)
            {
                return NotFound();
            }

            return View(cotizacione);
        }

		// GET: Cotizaciones/Create
		[Authorize(Roles = "Admin")]
		public IActionResult Create()
        {
            return View();
        }

        // POST: Cotizaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Create([Bind("Id,TipoMoneda,ValorMoneda")] Cotizacione cotizacione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cotizacione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cotizacione);
        }

		// GET: Cotizaciones/Edit/5
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cotizaciones == null)
            {
                return NotFound();
            }

            var cotizacione = await _context.Cotizaciones.FindAsync(id);
            if (cotizacione == null)
            {
                return NotFound();
            }
            return View(cotizacione);
        }

        // POST: Cotizaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Edit(int id, [Bind("Id,TipoMoneda,ValorMoneda")] Cotizacione cotizacione)
        {
            if (id != cotizacione.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cotizacione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CotizacioneExists(cotizacione.Id))
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
            return View(cotizacione);
        }

		// GET: Cotizaciones/Delete/5
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cotizaciones == null)
            {
                return NotFound();
            }

            var cotizacione = await _context.Cotizaciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cotizacione == null)
            {
                return NotFound();
            }

            return View(cotizacione);
        }

        // POST: Cotizaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cotizaciones == null)
            {
                return Problem("Entity set 'MVCOBLContext.Cotizaciones'  is null.");
            }
            var cotizacione = await _context.Cotizaciones.FindAsync(id);
            if (cotizacione != null)
            {
                _context.Cotizaciones.Remove(cotizacione);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CotizacioneExists(int id)
        {
          return (_context.Cotizaciones?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult actCotizaciones()
        {

            API_COT cotizacion = new API_COT();

            DateTime fechaActual = DateTime.Now;
            DateTime fechaHoy = DateTime.Today;
           




            Cotizacione cota = new Cotizacione();

            var resultado = cotizacion.GetCotizacion();                 //Aca trae el JSON de la API
            var cotizacionActual = JsonConvert.DeserializeObject<COTIZACION>(resultado);
            var dolar = cotizacionActual.Quotes;

            var dolarDouble = Convert.ToDecimal(dolar.Usduyu);

            cota.Fecha = fechaActual;
            cota.FechaSinHora = fechaHoy;
            cota.ValorMoneda = dolarDouble;
            cota.TipoMoneda = cotizacionActual.Source;

            _context.Add(cota);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}
