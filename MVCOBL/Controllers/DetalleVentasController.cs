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
    public class DetalleVentasController : Controller
    {
        private readonly MVCOBLContext _context;

        public DetalleVentasController(MVCOBLContext context)
        {
            _context = context;
        }

        // GET: DetalleVentas
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var mVCOBLContext =_context.DetalleVenta.Include(d => d.IdProductoNavigation).Include(d => d.IdVentaNavigation);
            return View(await mVCOBLContext.ToListAsync());
        }

        // GET: DetalleVentas/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DetalleVenta == null)
            {
                return NotFound();
            }

            var detalleVentum = await _context.DetalleVenta
                
                .Include(d => d.IdProductoNavigation)
                .Include(d => d.IdVentaNavigation)
                .FirstOrDefaultAsync(m => m.IdDetalleVenta == id);
            if (detalleVentum == null)
            {
                return NotFound();
            }

            return View(detalleVentum);
        }

        // GET: DetalleVentas/Create
        [Authorize(Roles = "Admin, Empleado")]
        //public IActionResult Create()
        //      {
        //          ViewData["IdCotizacion"] = new SelectList(_context.Cotizaciones, "Id", "Id");
        //          ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto");
        //          ViewData["IdVenta"] = new SelectList(_context.Venta, "IdVenta", "IdVenta");
        //          return View();
        //      }

        public IActionResult Create(int valor)
        {
            int dato = valor;

            var listaLinea = _context.DetalleVenta.Where(l => l.IdVenta == valor).ToList();


            ViewBag.Lineas = listaLinea;

            ViewBag.dato = dato;


         
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto");
            ViewData["IdVenta"] = new SelectList(_context.Venta, "IdVenta", "IdVenta", dato);

            return View();
        }

        // POST: DetalleVentas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Empleado")]
        public async Task<IActionResult> Create([Bind("IdDetalleVenta,IdVenta,IdProducto,Cantidad,PrecioUnidad,ImporteTotal,FechaRegistro")] DetalleVentum detalleVentum)
        {
            if (ModelState.IsValid)
            {
                var idProducto = detalleVentum.IdProducto;
                var Prod = _context.Productos.Where(l => l.IdProducto == idProducto).ToList().FirstOrDefault();
                var precioPrd = Prod.Precio;
                detalleVentum.PrecioUnidad = precioPrd;

                var cantidad = detalleVentum.Cantidad;

                var total = cantidad * precioPrd;

                detalleVentum.ImporteTotal = total;

                _context.Add(detalleVentum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create), new { valor = detalleVentum.IdVenta });

            }


            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", detalleVentum.IdProducto);
            ViewData["IdVenta"] = new SelectList(_context.Venta, "IdVenta", "IdVenta", detalleVentum.IdVenta);
            return View(detalleVentum);
        }

        // GET: DetalleVentas/Edit/5
        [Authorize(Roles = "Admin, Empleado")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DetalleVenta == null)
            {
                return NotFound();
            }

            var detalleVentum = await _context.DetalleVenta.FindAsync(id);
            if (detalleVentum == null)
            {
                return NotFound();
            }
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", detalleVentum.IdProducto);
            ViewData["IdVenta"] = new SelectList(_context.Venta, "IdVenta", "IdVenta", detalleVentum.IdVenta);
            return View(detalleVentum);
        }

        // POST: DetalleVentas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Empleado")]
        public async Task<IActionResult> Edit(int id, [Bind("IdDetalleVenta,IdVenta,IdProducto,Cantidad,PrecioUnidad,ImporteTotal,FechaRegistro")] DetalleVentum detalleVentum)
        {
            if (id != detalleVentum.IdDetalleVenta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleVentum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleVentumExists(detalleVentum.IdDetalleVenta))
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
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", detalleVentum.IdProducto);
            ViewData["IdVenta"] = new SelectList(_context.Venta, "IdVenta", "IdVenta", detalleVentum.IdVenta);
            return View(detalleVentum);
        }

        // GET: DetalleVentas/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DetalleVenta == null)
            {
                return NotFound();
            }

            var detalleVentum = await _context.DetalleVenta               
                .Include(d => d.IdProductoNavigation)
                .Include(d => d.IdVentaNavigation)
                .FirstOrDefaultAsync(m => m.IdDetalleVenta == id);
            if (detalleVentum == null)
            {
                return NotFound();
            }

            return View(detalleVentum);
        }

        // POST: DetalleVentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DetalleVenta == null)
            {
                return Problem("Entity set 'MVCOBLContext.DetalleVenta'  is null.");
            }
            var detalleVentum = await _context.DetalleVenta.FindAsync(id);
            if (detalleVentum != null)
            {
                _context.DetalleVenta.Remove(detalleVentum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleVentumExists(int id)
        {
            return (_context.DetalleVenta?.Any(e => e.IdDetalleVenta == id)).GetValueOrDefault();
        }
    }
}
