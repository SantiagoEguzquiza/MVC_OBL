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
    public class ProductoTiendaController : Controller
    {
        private readonly MVCOBLContext _context;

        public ProductoTiendaController(MVCOBLContext context)
        {
            _context = context;
        }

        // GET: ProductoTienda
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var mVCOBLContext = _context.ProductoTienda.Include(p => p.IdProductoNavigation).Include(p => p.IdTiendaNavigation);
            return View(await mVCOBLContext.ToListAsync());
        }

        // GET: ProductoTienda/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductoTienda == null)
            {
                return NotFound();
            }

            var productoTiendum = await _context.ProductoTienda
                .Include(p => p.IdProductoNavigation)
                .Include(p => p.IdTiendaNavigation)
                .FirstOrDefaultAsync(m => m.IdProductoTienda == id);
            if (productoTiendum == null)
            {
                return NotFound();
            }

            return View(productoTiendum);
        }

		// GET: ProductoTienda/Create
		[Authorize(Roles = "Admin")]
		public IActionResult Create()
        {
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto");
            ViewData["IdTienda"] = new SelectList(_context.Tienda, "IdTienda", "IdTienda");
            return View();
        }

        // POST: ProductoTienda/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Create([Bind("IdProductoTienda,IdProducto,IdTienda,PrecioUnidadCompra,PrecioUnidadVenta,Stock,FechaRegistro")] ProductoTiendum productoTiendum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productoTiendum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", productoTiendum.IdProducto);
            ViewData["IdTienda"] = new SelectList(_context.Tienda, "IdTienda", "IdTienda", productoTiendum.IdTienda);
            return View(productoTiendum);
        }

		// GET: ProductoTienda/Edit/5
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductoTienda == null)
            {
                return NotFound();
            }

            var productoTiendum = await _context.ProductoTienda.FindAsync(id);
            if (productoTiendum == null)
            {
                return NotFound();
            }
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", productoTiendum.IdProducto);
            ViewData["IdTienda"] = new SelectList(_context.Tienda, "IdTienda", "IdTienda", productoTiendum.IdTienda);
            return View(productoTiendum);
        }

        // POST: ProductoTienda/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Edit(int id, [Bind("IdProductoTienda,IdProducto,IdTienda,PrecioUnidadCompra,PrecioUnidadVenta,Stock,FechaRegistro")] ProductoTiendum productoTiendum)
        {
            if (id != productoTiendum.IdProductoTienda)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productoTiendum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoTiendumExists(productoTiendum.IdProductoTienda))
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
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", productoTiendum.IdProducto);
            ViewData["IdTienda"] = new SelectList(_context.Tienda, "IdTienda", "IdTienda", productoTiendum.IdTienda);
            return View(productoTiendum);
        }

		// GET: ProductoTienda/Delete/5
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductoTienda == null)
            {
                return NotFound();
            }

            var productoTiendum = await _context.ProductoTienda
                .Include(p => p.IdProductoNavigation)
                .Include(p => p.IdTiendaNavigation)
                .FirstOrDefaultAsync(m => m.IdProductoTienda == id);
            if (productoTiendum == null)
            {
                return NotFound();
            }

            return View(productoTiendum);
        }

        // POST: ProductoTienda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductoTienda == null)
            {
                return Problem("Entity set 'MVCOBLContext.ProductoTienda'  is null.");
            }
            var productoTiendum = await _context.ProductoTienda.FindAsync(id);
            if (productoTiendum != null)
            {
                _context.ProductoTienda.Remove(productoTiendum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoTiendumExists(int id)
        {
          return (_context.ProductoTienda?.Any(e => e.IdProductoTienda == id)).GetValueOrDefault();
        }
    }
}
