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
    public class UsuarioClaimsController : Controller
    {
        private readonly MVCOBLContext _context;

        public UsuarioClaimsController(MVCOBLContext context)
        {
            _context = context;
        }

        // GET: UsuarioClaims
        public async Task<IActionResult> Index()
        {
            var mVCOBLContext = _context.AspNetUserClaims.Include(a => a.User);
            return View(await mVCOBLContext.ToListAsync());
        }

        // GET: UsuarioClaims/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AspNetUserClaims == null)
            {
                return NotFound();
            }

            var aspNetUserClaim = await _context.AspNetUserClaims
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspNetUserClaim == null)
            {
                return NotFound();
            }

            return View(aspNetUserClaim);
        }

        // GET: UsuarioClaims/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: UsuarioClaims/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,ClaimType,ClaimValue")] AspNetUserClaim aspNetUserClaim)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aspNetUserClaim);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", aspNetUserClaim.UserId);
            return View(aspNetUserClaim);
        }

        // GET: UsuarioClaims/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AspNetUserClaims == null)
            {
                return NotFound();
            }

            var aspNetUserClaim = await _context.AspNetUserClaims.FindAsync(id);
            if (aspNetUserClaim == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", aspNetUserClaim.UserId);
            return View(aspNetUserClaim);
        }

        // POST: UsuarioClaims/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,ClaimType,ClaimValue")] AspNetUserClaim aspNetUserClaim)
        {
            if (id != aspNetUserClaim.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aspNetUserClaim);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AspNetUserClaimExists(aspNetUserClaim.Id))
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
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", aspNetUserClaim.UserId);
            return View(aspNetUserClaim);
        }

        // GET: UsuarioClaims/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AspNetUserClaims == null)
            {
                return NotFound();
            }

            var aspNetUserClaim = await _context.AspNetUserClaims
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspNetUserClaim == null)
            {
                return NotFound();
            }

            return View(aspNetUserClaim);
        }

        // POST: UsuarioClaims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AspNetUserClaims == null)
            {
                return Problem("Entity set 'MVCOBLContext.AspNetUserClaims'  is null.");
            }
            var aspNetUserClaim = await _context.AspNetUserClaims.FindAsync(id);
            if (aspNetUserClaim != null)
            {
                _context.AspNetUserClaims.Remove(aspNetUserClaim);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AspNetUserClaimExists(int id)
        {
          return (_context.AspNetUserClaims?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
