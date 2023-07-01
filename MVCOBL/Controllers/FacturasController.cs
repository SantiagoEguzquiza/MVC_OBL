using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCOBL.Models;
using Microsoft.AspNetCore.Authorization;

namespace MVCOBL.Controllers
{
    public class FacturasController : Controller
    {
        private readonly MVCOBLContext _context;

        public FacturasController(MVCOBLContext context)
        {
            _context = context;
        }

        // GET: Facturas
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var mVCOBLContext = _context.Facturas.Include(f => f.CotizacionNavigation).Include(f => f.IdClienteNavigation);
            return View(await mVCOBLContext.ToListAsync());
        }

        // GET: Facturas/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Facturas == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .Include(f => f.CotizacionNavigation)
                .Include(f => f.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

		// GET: Facturas/Create
		[Authorize(Roles = "Admin, Empleado")]
		public IActionResult Create()
        {
            ViewData["Cotizacion"] = new SelectList(_context.Cotizaciones, "Id", "Id");
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente");
            return View();
        }

        // POST: Facturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin, Empleado")]
		public async Task<IActionResult> Create([Bind("Id,Fecha,TipoFactura,IdCliente,Cotizacion")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(factura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cotizacion"] = new SelectList(_context.Cotizaciones, "Id", "Id", factura.Cotizacion);
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", factura.IdCliente);
            return View(factura);
        }

		// GET: Facturas/Edit/5
		[Authorize(Roles = "Admin, Empleado")]
		public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Facturas == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas.FindAsync(id);
            if (factura == null)
            {
                return NotFound();
            }
            ViewData["Cotizacion"] = new SelectList(_context.Cotizaciones, "Id", "Id", factura.Cotizacion);
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", factura.IdCliente);
            return View(factura);
        }

        // POST: Facturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin, Empleado")]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,TipoFactura,IdCliente,Cotizacion")] Factura factura)
        {
            if (id != factura.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(factura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaExists(factura.Id))
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
            ViewData["Cotizacion"] = new SelectList(_context.Cotizaciones, "Id", "Id", factura.Cotizacion);
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", factura.IdCliente);
            return View(factura);
        }

		// GET: Facturas/Delete/5
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Facturas == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .Include(f => f.CotizacionNavigation)
                .Include(f => f.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Facturas == null)
            {
                return Problem("Entity set 'MVCOBLContext.Facturas'  is null.");
            }
            var factura = await _context.Facturas.FindAsync(id);
            if (factura != null)
            {
                _context.Facturas.Remove(factura);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturaExists(int id)
        {
          return (_context.Facturas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
