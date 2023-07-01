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
    public class RoleClaimsController : Controller
    {
        private readonly MVCOBLContext _context;

        public RoleClaimsController(MVCOBLContext context)
        {
            _context = context;
        }

        // GET: RoleClaims
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var mVCOBLContext = _context.AspNetRoleClaims.Include(a => a.Role);
            return View(await mVCOBLContext.ToListAsync());
        }

        // GET: RoleClaims/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AspNetRoleClaims == null)
            {
                return NotFound();
            }

            var aspNetRoleClaim = await _context.AspNetRoleClaims
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspNetRoleClaim == null)
            {
                return NotFound();
            }

            return View(aspNetRoleClaim);
        }

		// GET: RoleClaims/Create
		[Authorize(Roles = "Admin")]
		public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.AspNetRoles, "Id", "Id");
            return View();
        }

        // POST: RoleClaims/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Create([Bind("Id,RoleId,ClaimType,ClaimValue")] AspNetRoleClaim aspNetRoleClaim)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aspNetRoleClaim);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.AspNetRoles, "Id", "Id", aspNetRoleClaim.RoleId);
            return View(aspNetRoleClaim);
        }

		// GET: RoleClaims/Edit/5
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AspNetRoleClaims == null)
            {
                return NotFound();
            }

            var aspNetRoleClaim = await _context.AspNetRoleClaims.FindAsync(id);
            if (aspNetRoleClaim == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.AspNetRoles, "Id", "Id", aspNetRoleClaim.RoleId);
            return View(aspNetRoleClaim);
        }

        // POST: RoleClaims/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Edit(int id, [Bind("Id,RoleId,ClaimType,ClaimValue")] AspNetRoleClaim aspNetRoleClaim)
        {
            if (id != aspNetRoleClaim.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aspNetRoleClaim);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AspNetRoleClaimExists(aspNetRoleClaim.Id))
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
            ViewData["RoleId"] = new SelectList(_context.AspNetRoles, "Id", "Id", aspNetRoleClaim.RoleId);
            return View(aspNetRoleClaim);
        }

		// GET: RoleClaims/Delete/5
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AspNetRoleClaims == null)
            {
                return NotFound();
            }

            var aspNetRoleClaim = await _context.AspNetRoleClaims
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspNetRoleClaim == null)
            {
                return NotFound();
            }

            return View(aspNetRoleClaim);
        }

        // POST: RoleClaims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AspNetRoleClaims == null)
            {
                return Problem("Entity set 'MVCOBLContext.AspNetRoleClaims'  is null.");
            }
            var aspNetRoleClaim = await _context.AspNetRoleClaims.FindAsync(id);
            if (aspNetRoleClaim != null)
            {
                _context.AspNetRoleClaims.Remove(aspNetRoleClaim);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AspNetRoleClaimExists(int id)
        {
          return (_context.AspNetRoleClaims?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
