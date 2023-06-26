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
    public class UsuarioTokensController : Controller
    {
        private readonly MVCOBLContext _context;

        public UsuarioTokensController(MVCOBLContext context)
        {
            _context = context;
        }

        // GET: UsuarioTokens
        public async Task<IActionResult> Index()
        {
            var mVCOBLContext = _context.AspNetUserTokens.Include(a => a.User);
            return View(await mVCOBLContext.ToListAsync());
        }

        // GET: UsuarioTokens/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.AspNetUserTokens == null)
            {
                return NotFound();
            }

            var aspNetUserToken = await _context.AspNetUserTokens
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (aspNetUserToken == null)
            {
                return NotFound();
            }

            return View(aspNetUserToken);
        }

        // GET: UsuarioTokens/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: UsuarioTokens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,LoginProvider,Name,Value")] AspNetUserToken aspNetUserToken)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aspNetUserToken);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", aspNetUserToken.UserId);
            return View(aspNetUserToken);
        }

        // GET: UsuarioTokens/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.AspNetUserTokens == null)
            {
                return NotFound();
            }

            var aspNetUserToken = await _context.AspNetUserTokens.FindAsync(id);
            if (aspNetUserToken == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", aspNetUserToken.UserId);
            return View(aspNetUserToken);
        }

        // POST: UsuarioTokens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserId,LoginProvider,Name,Value")] AspNetUserToken aspNetUserToken)
        {
            if (id != aspNetUserToken.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aspNetUserToken);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AspNetUserTokenExists(aspNetUserToken.UserId))
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
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", aspNetUserToken.UserId);
            return View(aspNetUserToken);
        }

        // GET: UsuarioTokens/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.AspNetUserTokens == null)
            {
                return NotFound();
            }

            var aspNetUserToken = await _context.AspNetUserTokens
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (aspNetUserToken == null)
            {
                return NotFound();
            }

            return View(aspNetUserToken);
        }

        // POST: UsuarioTokens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.AspNetUserTokens == null)
            {
                return Problem("Entity set 'MVCOBLContext.AspNetUserTokens'  is null.");
            }
            var aspNetUserToken = await _context.AspNetUserTokens.FindAsync(id);
            if (aspNetUserToken != null)
            {
                _context.AspNetUserTokens.Remove(aspNetUserToken);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AspNetUserTokenExists(string id)
        {
          return (_context.AspNetUserTokens?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
