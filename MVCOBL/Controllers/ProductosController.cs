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
    public class ProductosController : Controller
    {
        private readonly MVCOBLContext _context;

        public ProductosController(MVCOBLContext context)
        {
            _context = context;
        }

        // GET: Productos
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var mVCOBLContext = _context.Productos.Include(p => p.IdCategoriaNavigation).Include(p => p.IdCotizacionNavigation);
            return View(await mVCOBLContext.ToListAsync());

        }
        public IActionResult Catalogo()
        {

            //------------------------------------* API Obtener Cotizacion del dia *-------------------------------------------------------  

            API_COT cotizacion = new API_COT();
            
            var ultimaCotizacion = _context.Cotizaciones.OrderBy(x => x).LastOrDefault();        //Consultamos la ultima cotizacion que tengamos en la base
            DateTime fechaActual = DateTime.Today;


            if (ultimaCotizacion.Fecha != fechaActual)
            {
                
                Cotizacione cota = new Cotizacione();

                var resultado = cotizacion.GetCotizacion();                 //Aca trae el JSON de la API
                var cotizacionActual = JsonConvert.DeserializeObject<COTIZACION>(resultado);
                var dolar = cotizacionActual.Quotes;

                var dolarDouble = Convert.ToDecimal(dolar.Usduyu);

                cota.Fecha = fechaActual;
                cota.ValorMoneda = dolarDouble;
                cota.TipoMoneda = cotizacionActual.Source;

                _context.Add(cota);
                _context.SaveChanges();

            }


            //------------------------------------* API *-------------------------------------------------------

           

            var categorias = _context.Categoria.ToList();
            var productos = _context.Productos.ToList();

            var nombreCategorias = productos
                .Join(categorias, categorias => categorias.IdCategoria, usuario => usuario.IdCategoria, (categorias, usuario) => new { categorias, usuario })
                .Select(x => new { x.categorias.IdCategoria, x.usuario.Descripcion })
                .ToList();

            var combinada =  nombreCategorias.Zip(productos, (cate, prod) => (prod.IdProducto, prod.Codigo, prod.Nombre, prod.Descripcion, cate.Descripcion, prod.FechaRegistro, prod.Stock, prod.Precio, prod.ImagenUrl, prod.IdCotizacion, prod.Moneda));

            ViewBag.combinada = combinada;

            return View();
        }

        // GET: Productos/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdCotizacionNavigation)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }


        // GET: Productos/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "IdCategoria");
            ViewData["IdCotizacion"] = new SelectList(_context.Cotizaciones, "Id", "Id");
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("IdProducto,Codigo,Nombre,Descripcion,Precio,Stock,IdCategoria,IdCotizacion,Moneda,ImagenUrl,FechaRegistro")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "IdCategoria", producto.IdCategoria);
            ViewData["IdCotizacion"] = new SelectList(_context.Cotizaciones, "Id", "Id", producto.IdCotizacion);
            return View(producto);
        }

        // GET: Productos/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "IdCategoria", producto.IdCategoria);
            ViewData["IdCotizacion"] = new SelectList(_context.Cotizaciones, "Id", "Id", producto.IdCotizacion);
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("IdProducto,Codigo,Nombre,Descripcion,Precio,Stock,IdCategoria,Moneda,ImagenUrl,FechaRegistro,IdCotizacion")] Producto producto)
        {
            if (id != producto.IdProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.IdProducto))
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
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "IdCategoria", producto.IdCategoria);
            ViewData["IdCotizacion"] = new SelectList(_context.Cotizaciones, "Id", "Id", producto.IdCotizacion);
            return View(producto);
        }

        // GET: Productos/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdCotizacionNavigation)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Productos == null)
            {
                return Problem("Entity set 'MVCOBLContext.Productos'  is null.");
            }
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return (_context.Productos?.Any(e => e.IdProducto == id)).GetValueOrDefault();
        }
    }
}
