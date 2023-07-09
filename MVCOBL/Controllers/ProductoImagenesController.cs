//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using MVCOBL.Models;

//namespace MVCOBL.Controllers
//{
//    public class ProductoImagenesController : Controller
//    {
//        private readonly MVCOBLContext _context;

//        public ProductoImagenesController(MVCOBLContext context)
//        {
//            _context = context;
//        }

//        // GET: ProductoImagenes
//        [Authorize]
//        public async Task<IActionResult> Index()
//        {
//            var mVCOBLContext = _context.ProductoImagenes.Include(p => p.IdProductoNavigation);
//            return View(await mVCOBLContext.ToListAsync());
//        }

//        // GET: ProductoImagenes/Details/5
//        [Authorize]
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null || _context.ProductoImagenes == null)
//            {
//                return NotFound();
//            }

//            var productoImagene = await _context.ProductoImagenes
//                .Include(p => p.IdProductoNavigation)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (productoImagene == null)
//            {
//                return NotFound();
//            }

//            return View(productoImagene);
//        }

//		// GET: ProductoImagenes/Create
//		[Authorize(Roles = "Admin")]
//		public IActionResult Create()
//        {
//            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto");
//            return View();
//        }

//        // POST: ProductoImagenes/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//		[Authorize(Roles = "Admin")]
//		public async Task<IActionResult> Create([Bind("Id,IdProducto,LinkUrl")] ProductoImagene productoImagene)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(productoImagene);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", productoImagene.IdProducto);
//            return View(productoImagene);
//        }

//		// GET: ProductoImagenes/Edit/5
//		[Authorize(Roles = "Admin")]
//		public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null || _context.ProductoImagenes == null)
//            {
//                return NotFound();
//            }

//            var productoImagene = await _context.ProductoImagenes.FindAsync(id);
//            if (productoImagene == null)
//            {
//                return NotFound();
//            }
//            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", productoImagene.IdProducto);
//            return View(productoImagene);
//        }

//        // POST: ProductoImagenes/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//		[Authorize(Roles = "Admin")]
//		public async Task<IActionResult> Edit(int id, [Bind("Id,IdProducto,LinkUrl")] ProductoImagene productoImagene)
//        {
//            if (id != productoImagene.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(productoImagene);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!ProductoImageneExists(productoImagene.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", productoImagene.IdProducto);
//            return View(productoImagene);
//        }

//		// GET: ProductoImagenes/Delete/5
//		[Authorize(Roles = "Admin")]
//		public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null || _context.ProductoImagenes == null)
//            {
//                return NotFound();
//            }

//            var productoImagene = await _context.ProductoImagenes
//                .Include(p => p.IdProductoNavigation)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (productoImagene == null)
//            {
//                return NotFound();
//            }

//            return View(productoImagene);
//        }

//        // POST: ProductoImagenes/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//		[Authorize(Roles = "Admin")]
//		public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            if (_context.ProductoImagenes == null)
//            {
//                return Problem("Entity set 'MVCOBLContext.ProductoImagenes'  is null.");
//            }
//            var productoImagene = await _context.ProductoImagenes.FindAsync(id);
//            if (productoImagene != null)
//            {
//                _context.ProductoImagenes.Remove(productoImagene);
//            }
            
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool ProductoImageneExists(int id)
//        {
//          return (_context.ProductoImagenes?.Any(e => e.Id == id)).GetValueOrDefault();
//        }
//    }
//}
