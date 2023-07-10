using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCOBL.Models;

namespace MVCOBL.Controllers
{
    public class UsuarioLoginController : Controller
    {
        private readonly MVCOBLContext _context;

        public UsuarioLoginController(MVCOBLContext context)
        {
            _context = context;
        }

        // GET: UsuarioLogin
        public async Task<IActionResult> Index()
        {
            var mVCOBLContext = _context.AspNetUserLogins.Include(a => a.User);
            return View(await mVCOBLContext.ToListAsync());
        }

        // GET: UsuarioLogin/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.AspNetUserLogins == null)
            {
                return NotFound();
            }

            var aspNetUserLogin = await _context.AspNetUserLogins
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.LoginProvider == id);
            if (aspNetUserLogin == null)
            {
                return NotFound();
            }

            return View(aspNetUserLogin);
        }

        // GET: UsuarioLogin/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: UsuarioLogin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoginProvider,ProviderKey,ProviderDisplayName,UserId")] AspNetUserLogin aspNetUserLogin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aspNetUserLogin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", aspNetUserLogin.UserId);
            return View(aspNetUserLogin);
        }

        // GET: UsuarioLogin/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.AspNetUserLogins == null)
            {
                return NotFound();
            }

            var aspNetUserLogin = await _context.AspNetUserLogins.FindAsync(id);
            if (aspNetUserLogin == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", aspNetUserLogin.UserId);
            return View(aspNetUserLogin);
        }

        // POST: UsuarioLogin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("LoginProvider,ProviderKey,ProviderDisplayName,UserId")] AspNetUserLogin aspNetUserLogin)
        {
            if (id != aspNetUserLogin.LoginProvider)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aspNetUserLogin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AspNetUserLoginExists(aspNetUserLogin.LoginProvider))
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
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", aspNetUserLogin.UserId);
            return View(aspNetUserLogin);
        }

        // GET: UsuarioLogin/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.AspNetUserLogins == null)
            {
                return NotFound();
            }

            var aspNetUserLogin = await _context.AspNetUserLogins
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.LoginProvider == id);
            if (aspNetUserLogin == null)
            {
                return NotFound();
            }

            return View(aspNetUserLogin);
        }

        // POST: UsuarioLogin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.AspNetUserLogins == null)
            {
                return Problem("Entity set 'MVCOBLContext.AspNetUserLogins'  is null.");
            }
            var aspNetUserLogin = await _context.AspNetUserLogins.FindAsync(id);
            if (aspNetUserLogin != null)
            {
                _context.AspNetUserLogins.Remove(aspNetUserLogin);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AspNetUserLoginExists(string id)
        {
          return (_context.AspNetUserLogins?.Any(e => e.LoginProvider == id)).GetValueOrDefault();
        }
    }
}
