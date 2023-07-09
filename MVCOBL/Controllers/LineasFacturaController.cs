//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using MVCOBL.Models;
//using Microsoft.AspNetCore.Authorization;

//namespace MVCOBL.Controllers
//{
//    public class LineasFacturaController : Controller
//    {
//        private readonly MVCOBLContext _context;

//        public LineasFacturaController(MVCOBLContext context)
//        {
//            _context = context;
//        }

//        // GET: LineasFactura
//        [Authorize]
//        public async Task<IActionResult> Index()
//        {
//            var mVCOBLContext = _context.LineaFacturas.Include(l => l.IdFacturaNavigation).Include(l => l.IdProductoNavigation);
//            return View(await mVCOBLContext.ToListAsync());
//        }

//        // GET: LineasFactura/Details/5
//        [Authorize]
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null || _context.LineaFacturas == null)
//            {
//                return NotFound();
//            }

//            var lineaFactura = await _context.LineaFacturas
//                .Include(l => l.IdFacturaNavigation)
//                .Include(l => l.IdProductoNavigation)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (lineaFactura == null)
//            {
//                return NotFound();
//            }

//            return View(lineaFactura);
//        }

//		// GET: LineasFactura/Create
//		[Authorize(Roles = "Admin, Empleado")]
//		public IActionResult Create()
//        {
//            ViewData["IdFactura"] = new SelectList(_context.Facturas, "Id", "Id");
//            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto");
//            return View();
//        }

//        // POST: LineasFactura/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//		[Authorize(Roles = "Admin, Empleado")]
//		public async Task<IActionResult> Create([Bind("Id,Cantidad,Precio,IdFactura,IdProducto")] LineaFactura lineaFactura)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(lineaFactura);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["IdFactura"] = new SelectList(_context.Facturas, "Id", "Id", lineaFactura.IdFactura);
//            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", lineaFactura.IdProducto);
//            return View(lineaFactura);
//        }

//		// GET: LineasFactura/Edit/5
//		[Authorize(Roles = "Admin, Empleado")]
//		public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null || _context.LineaFacturas == null)
//            {
//                return NotFound();
//            }

//            var lineaFactura = await _context.LineaFacturas.FindAsync(id);
//            if (lineaFactura == null)
//            {
//                return NotFound();
//            }
//            ViewData["IdFactura"] = new SelectList(_context.Facturas, "Id", "Id", lineaFactura.IdFactura);
//            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", lineaFactura.IdProducto);
//            return View(lineaFactura);
//        }

//        // POST: LineasFactura/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//		[Authorize(Roles = "Admin, Empleado")]
//		public async Task<IActionResult> Edit(int id, [Bind("Id,Cantidad,Precio,IdFactura,IdProducto")] LineaFactura lineaFactura)
//        {
//            if (id != lineaFactura.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(lineaFactura);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!LineaFacturaExists(lineaFactura.Id))
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
//            ViewData["IdFactura"] = new SelectList(_context.Facturas, "Id", "Id", lineaFactura.IdFactura);
//            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", lineaFactura.IdProducto);
//            return View(lineaFactura);
//        }

//		// GET: LineasFactura/Delete/5
//		[Authorize(Roles = "Admin")]
//		public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null || _context.LineaFacturas == null)
//            {
//                return NotFound();
//            }

//            var lineaFactura = await _context.LineaFacturas
//                .Include(l => l.IdFacturaNavigation)
//                .Include(l => l.IdProductoNavigation)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (lineaFactura == null)
//            {
//                return NotFound();
//            }

//            return View(lineaFactura);
//        }

//        // POST: LineasFactura/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//		[Authorize(Roles = "Admin")]
//		public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            if (_context.LineaFacturas == null)
//            {
//                return Problem("Entity set 'MVCOBLContext.LineaFacturas'  is null.");
//            }
//            var lineaFactura = await _context.LineaFacturas.FindAsync(id);
//            if (lineaFactura != null)
//            {
//                _context.LineaFacturas.Remove(lineaFactura);
//            }
            
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool LineaFacturaExists(int id)
//        {
//          return (_context.LineaFacturas?.Any(e => e.Id == id)).GetValueOrDefault();
//        }
//    }
//}
