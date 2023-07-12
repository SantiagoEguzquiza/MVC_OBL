using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCOBL.Models;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using API;


namespace MVCOBL.Controllers
{


    public class VentasController : Controller
    {
        private readonly MVCOBLContext _context;

        public VentasController(MVCOBLContext context)
        {
            _context = context;

        }

        // GET: Ventas
        [Authorize]
        public async Task<IActionResult> Index()
        {


            var Sucursales = _context.Tienda.ToList();
            var ventas = _context.Venta.ToList();
            var Clientes = _context.Clientes.ToList();
            var Usuarios = _context.AspNetUsers.ToList();



            //Esto es un inner join el nombre de usuario por cada venta existente, basicamente replica la lista ventas y en cada idUsuario te lo iguala al nombre.
            //Generando una lista con la misma cantidad que la de ventas pero en cada objeto solo tiene el nombre

            var nombreUsuarios = ventas
                .Join(Usuarios, venta => venta.IdUsuario, usuario => usuario.Id, (venta, usuario) => new { venta, usuario })
                .Select(x => new { x.venta.IdUsuario, x.usuario.UserName })
                .ToList();

            var nombreClientes = ventas
                .Join(Clientes, venta => venta.IdCliente, x => x.IdCliente, (venta, x) => new { venta, x })
                .Select(x => new { x.venta.IdUsuario, x.x.Nombre })
                .ToList();

            var nombreSucursales = ventas
                .Join(Sucursales, venta => venta.IdTienda, x => x.IdTienda, (venta, x) => new { venta, x })
                .Select(x => new { x.venta.IdTienda, x.x.Nombre })
                .ToList();


            var resultado = ventas.Zip(nombreUsuarios, (venta, user) => (user.UserName, venta.IdCliente, venta.IdTienda, venta.FechaRegistro, venta.IdVenta))
                                  .Zip(nombreClientes, (ventauser, cliente) => (ventauser.UserName, cliente.Nombre, ventauser.IdTienda, ventauser.FechaRegistro, ventauser.IdVenta))
                                  .Zip(nombreSucursales, (ventausercliente, sucursal) => (ventausercliente.UserName, ventausercliente.Nombre, sucursal.Nombre, ventausercliente.FechaRegistro, ventausercliente.IdVenta)) ;

            ViewBag.combinada = resultado;



            //var Usuarios = _context.AspNetUsers.Where(x => x.Id == );

            var mVCOBLContext = _context.Venta.Include(v => v.IdClienteNavigation).Include(v => v.IdTiendaNavigation).Include(v => v.IdUsuarioNavigation);
            return View(await mVCOBLContext.ToListAsync());
        }

        // GET: Ventas/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Venta == null)
            {
                return NotFound();
            }

            var ventum = await _context.Venta
                .Include(v => v.IdClienteNavigation)
                .Include(v => v.IdTiendaNavigation)
                .Include(v => v.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdVenta == id);
            if (ventum == null)
            {
                return NotFound();
            }

            return View(ventum);
        }

        // GET: Ventas/Create
        [Authorize(Roles = "Admin, Empleado")]
        public IActionResult Create()

        {

            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente");
            ViewData["IdTienda"] = new SelectList(_context.Tienda, "IdTienda", "IdTienda");
            ViewData["IdUsuario"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: Ventas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Empleado")]
        public async Task<IActionResult> Create([Bind("IdVenta,Codigo,ValorCodigo,IdTienda,IdUsuario,IdCliente,CantidadProducto,CantidadTotal,TotalCosto,FechaRegistro")] Ventum ventum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ventum);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "DetalleVentas", new { valor = ventum.IdVenta });
            }



            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", ventum.IdCliente);
            ViewData["IdTienda"] = new SelectList(_context.Tienda, "IdTienda", "IdTienda", ventum.IdTienda);
            ViewData["IdUsuario"] = new SelectList(_context.AspNetUsers, "Id", "Id", ventum.IdUsuario);
            return View(ventum);
        }

        // GET: Ventas/Edit/5
        [Authorize(Roles = "Admin, Empleado")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Venta == null)
            {
                return NotFound();
            }

            var ventum = await _context.Venta.FindAsync(id);
            if (ventum == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", ventum.IdCliente);
            ViewData["IdTienda"] = new SelectList(_context.Tienda, "IdTienda", "IdTienda", ventum.IdTienda);
            ViewData["IdUsuario"] = new SelectList(_context.AspNetUsers, "Id", "Id", ventum.IdUsuario);
            return View(ventum);
        }

        // POST: Ventas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Empleado")]
        public async Task<IActionResult> Edit(int id, [Bind("IdVenta,Codigo,ValorCodigo,IdTienda,IdUsuario,IdCliente,CantidadProducto,CantidadTotal,TotalCosto,FechaRegistro")] Ventum ventum)
        {
            if (id != ventum.IdVenta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ventum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentumExists(ventum.IdVenta))
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
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", ventum.IdCliente);
            ViewData["IdTienda"] = new SelectList(_context.Tienda, "IdTienda", "IdTienda", ventum.IdTienda);
            ViewData["IdUsuario"] = new SelectList(_context.AspNetUsers, "Id", "Id", ventum.IdUsuario);
            return View(ventum);
        }

        // GET: Ventas/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Venta == null)
            {
                return NotFound();
            }

            var ventum = await _context.Venta
                .Include(v => v.IdClienteNavigation)
                .Include(v => v.IdTiendaNavigation)
                .Include(v => v.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdVenta == id);
            if (ventum == null)
            {
                return NotFound();
            }

            return View(ventum);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Venta == null)
            {
                return Problem("Entity set 'MVCOBLContext.Venta'  is null.");
            }

            var ventum = await _context.Venta.FindAsync(id);
            var listaDetalles = _context.DetalleVenta.Where(x => x.IdVenta == id);

            if (ventum != null)
            {
                if (listaDetalles != null)
                {
                    foreach (var x in listaDetalles)
                    {
                        _context.DetalleVenta.Remove(x);
                    }
                }
                _context.Venta.Remove(ventum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentumExists(int id)
        {
            return (_context.Venta?.Any(e => e.IdVenta == id)).GetValueOrDefault();
        }

        [Authorize]
        public IActionResult VerFactura(int id)

        {
            var Venta = _context.Venta.Where(x => x.IdVenta == id).ToList();
            var ListaDetalle = _context.DetalleVenta.Where(x => x.IdVenta == id).ToList();

            decimal? aux = 0;

            foreach (var venta in ListaDetalle)
            {
                aux += venta.ImporteTotal;
            }

            ViewBag.Venta = Venta;
            ViewBag.ListaDetalle = ListaDetalle;
            ViewBag.Total = aux;


            return View();
        }
    }
}
