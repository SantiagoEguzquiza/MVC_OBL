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
    public class ComprasController : Controller
    {
        private readonly MVCOBLContext _context;

        public ComprasController(MVCOBLContext context)
        {
            _context = context;
        }

        // GET: Compras
        [Authorize]
        public async Task<IActionResult> Index()
        {

            var ventas = _context.Compras.ToList();
            var Clientes = _context.Clientes.ToList();
            var Usuarios = _context.AspNetUsers.ToList();
			var Sucursales = _context.Tienda.ToList();

			var nombreUsuarios = ventas
                .Join(Usuarios, venta => venta.IdUsuario, usuario => usuario.Id, (venta, usuario) => new { venta, usuario })
                .Select(x => new { x.venta.IdUsuario, x.usuario.UserName })
                .ToList();

			var nombreSucursales = ventas
				.Join(Sucursales, venta => venta.IdTienda, x => x.IdTienda, (venta, x) => new { venta, x })
				.Select(x => new { x.venta.IdTienda, x.x.Nombre })
				.ToList();


			ViewBag.nombreUser = nombreUsuarios;
            ViewBag.venta = ventas;

            //Esta es una lista combinada 
            var combinada = nombreUsuarios.Zip(ventas, (usuario, venta) => new { Nombre = usuario.UserName, Fecha = venta.FechaRegistro, Sucursal = venta.IdTienda, IdVenta = venta.IdCompra })
                                           .Zip(nombreSucursales, (ventauser, cliente) => (ventauser.Nombre, cliente.Nombre, ventauser.Fecha, ventauser.IdVenta, ventauser.Fecha));




			ViewBag.combinada = combinada;


            var mVCOBLContext = _context.Compras.Include(c => c.IdTiendaNavigation).Include(c => c.IdUsuarioNavigation);
            return View(await mVCOBLContext.ToListAsync());
        }

        // GET: Compras/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .Include(c => c.IdTiendaNavigation)
                .Include(c => c.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdCompra == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        // GET: Compras/Create
        [Authorize(Roles = "Admin, Empleado")]
        public IActionResult Create()
        {
            ViewData["IdTienda"] = new SelectList(_context.Tienda, "IdTienda", "IdTienda");
            ViewData["IdUsuario"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: Compras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Empleado")]
        public async Task<IActionResult> Create([Bind("IdCompra,IdUsuario,IdTienda,TotalCosto,TipoComprobante,FechaRegistro")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(compra);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "DetalleCompras", new { valor = compra.IdCompra });
            }
            ViewData["IdTienda"] = new SelectList(_context.Tienda, "IdTienda", "IdTienda", compra.IdTienda);
            ViewData["IdUsuario"] = new SelectList(_context.AspNetUsers, "Id", "Id", compra.IdUsuario);
            return View(compra);
        }

        // GET: Compras/Edit/5
        [Authorize(Roles = "Admin, Empleado")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras.FindAsync(id);
            if (compra == null)
            {
                return NotFound();
            }
            ViewData["IdTienda"] = new SelectList(_context.Tienda, "IdTienda", "IdTienda", compra.IdTienda);
            ViewData["IdUsuario"] = new SelectList(_context.AspNetUsers, "Id", "Id", compra.IdUsuario);
            return View(compra);
        }

        // POST: Compras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Empleado")]
        public async Task<IActionResult> Edit(int id, [Bind("IdCompra,IdUsuario,IdTienda,TotalCosto,TipoComprobante,FechaRegistro")] Compra compra)
        {
            if (id != compra.IdCompra)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompraExists(compra.IdCompra))
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
            ViewData["IdTienda"] = new SelectList(_context.Tienda, "IdTienda", "IdTienda", compra.IdTienda);
            ViewData["IdUsuario"] = new SelectList(_context.AspNetUsers, "Id", "Id", compra.IdUsuario);
            return View(compra);
        }

        // GET: Compras/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .Include(c => c.IdTiendaNavigation)
                .Include(c => c.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdCompra == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Compras == null)
            {
                return Problem("Entity set 'MVCOBLContext.Compras'  is null.");
            }
            var compra = await _context.Compras.FindAsync(id);
            var listaDetalles = _context.DetalleCompras.Where(x => x.IdCompra == id);

            if (compra != null)
            {
                if (listaDetalles != null)
                {
                    foreach (var x in listaDetalles)
                    {
                        _context.DetalleCompras.Remove(x);
                    }
                }
                _context.Compras.Remove(compra);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompraExists(int id)
        {
            return (_context.Compras?.Any(e => e.IdCompra == id)).GetValueOrDefault();
        }

        public IActionResult VerFactura(int id)

        {
            var Venta = _context.Compras.Where(x => x.IdCompra == id).ToList();
            var ListaDetalle = _context.DetalleCompras.Where(x => x.IdCompra == id).ToList();

            decimal? aux = 0;
            

            foreach (var venta in ListaDetalle)
            {
                aux += venta.TotalCosto;
            }

            ViewBag.Compra = Venta;
            ViewBag.ListaDetalleCompra = ListaDetalle;
            ViewBag.TotalCompra = aux;


            return View();
        }
    }
}
