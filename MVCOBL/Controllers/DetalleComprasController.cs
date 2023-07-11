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
    public class DetalleComprasController : Controller
    {
        private readonly MVCOBLContext _context;

        public DetalleComprasController(MVCOBLContext context)
        {
            _context = context;
        }

        // GET: DetalleCompras
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var mVCOBLContext = _context.DetalleCompras.Include(d => d.IdCompraNavigation).Include(d => d.IdProductoNavigation);
            return View(await mVCOBLContext.ToListAsync());
        }

        // GET: DetalleCompras/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DetalleCompras == null)
            {
                return NotFound();
            }

            var detalleCompra = await _context.DetalleCompras
                .Include(d => d.IdCompraNavigation)               
                .Include(d => d.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdDetalleCompra == id);
            if (detalleCompra == null)
            {
                return NotFound();
            }

            return View(detalleCompra);
        }

		// GET: DetalleCompras/Create
		[Authorize(Roles = "Admin, Empleado")]
		//public IActionResult Create()
  //      {
  //          ViewData["IdCompra"] = new SelectList(_context.Compras, "IdCompra", "IdCompra");
  //          ViewData["IdCotizacion"] = new SelectList(_context.Cotizaciones, "Id", "Id");
  //          ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto");
  //          return View();
  //      }

		public IActionResult Create(int valor)
		{
			int dato = valor;

			var listaLinea = _context.DetalleCompras.Where(l => l.IdCompra == valor).ToList();


			ViewBag.Lineas = listaLinea;

			ViewBag.dato = dato;

			//ViewData["IdCompra"] = new SelectList(_context.Productos, "IdFactura", "IdFactura", dato);
			//ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto");

			ViewData["IdCompra"] = new SelectList(_context.Compras, "IdCompra", "IdCompra", dato);		
			ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto");

			return View();
		}

		// POST: DetalleCompras/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin, Empleado")]
		public async Task<IActionResult> Create([Bind("IdDetalleCompra,IdCompra,IdProducto,Cantidad,PrecioUnitarioCompra,TotalCosto,FechaRegistro")] DetalleCompra detalleCompra)
        {
            if (ModelState.IsValid)
            {
                var idProducto = detalleCompra.IdProducto;
                var Prod = _context.Productos.Where(l => l.IdProducto == idProducto).ToList().FirstOrDefault();
                var precioPrd = Prod.Precio;

                detalleCompra.PrecioUnitarioCompra = precioPrd;

                var cantidad = detalleCompra.Cantidad;

                var total = cantidad * precioPrd;

                detalleCompra.TotalCosto = total;

                Prod.Stock += cantidad;

                _context.Add(detalleCompra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create), new {valor = detalleCompra.IdCompra});
            }
            ViewData["IdCompra"] = new SelectList(_context.Compras, "IdCompra", "IdCompra", detalleCompra.IdCompra);          
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", detalleCompra.IdProducto);
            return View(detalleCompra);
        }

		// GET: DetalleCompras/Edit/5
		[Authorize(Roles = "Admin, Empleado")]
		public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DetalleCompras == null)
            {
                return NotFound();
            }

            var detalleCompra = await _context.DetalleCompras.FindAsync(id);
            if (detalleCompra == null)
            {
                return NotFound();
            }
            ViewData["IdCompra"] = new SelectList(_context.Compras, "IdCompra", "IdCompra", detalleCompra.IdCompra);          
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", detalleCompra.IdProducto);
            return View(detalleCompra);
        }

        // POST: DetalleCompras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin, Empleado")]
		public async Task<IActionResult> Edit(int id, [Bind("IdDetalleCompra,IdCompra,IdProducto,Cantidad,PrecioUnitarioCompra,TotalCosto,FechaRegistro")] DetalleCompra detalleCompra)
        {
            if (id != detalleCompra.IdDetalleCompra)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleCompra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleCompraExists(detalleCompra.IdDetalleCompra))
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
            ViewData["IdCompra"] = new SelectList(_context.Compras, "IdCompra", "IdCompra", detalleCompra.IdCompra);        
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", detalleCompra.IdProducto);
            return View(detalleCompra);
        }

		// GET: DetalleCompras/Delete/5
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DetalleCompras == null)
            {
                return NotFound();
            }

            var detalleCompra = await _context.DetalleCompras
                .Include(d => d.IdCompraNavigation)              
                .Include(d => d.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdDetalleCompra == id);
            if (detalleCompra == null)
            {
                return NotFound();
            }

            return View(detalleCompra);
        }

        // POST: DetalleCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DetalleCompras == null)
            {
                return Problem("Entity set 'MVCOBLContext.DetalleCompras'  is null.");
            }
            var detalleCompra = await _context.DetalleCompras.FindAsync(id);
            if (detalleCompra != null)
            {
                _context.DetalleCompras.Remove(detalleCompra);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleCompraExists(int id)
        {
          return (_context.DetalleCompras?.Any(e => e.IdDetalleCompra == id)).GetValueOrDefault();
        }
    }
}
