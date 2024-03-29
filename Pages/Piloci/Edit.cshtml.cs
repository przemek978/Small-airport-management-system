﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SBD.Data;
using SBD.Models;

namespace SBD.Pages.Piloci
{
    public class EditModel : PageModel
    {
        private readonly SBD.Data.AirPortContext _context;

        public EditModel(SBD.Data.AirPortContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Pilot Pilot { get; set; }
        public string pass { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Pilot = await _context.Pilot
                .Include(p => p.linia).FirstOrDefaultAsync(m => m.id_pilota == id);
            pass = Pilot.haslo;
            if (Pilot == null)
            {
                return NotFound();
            }
           ViewData["kod_linii"] = new SelectList(_context.LiniaLotnicza, "kod", "kod");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string ? pass)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Pilot.haslo = pass;
            _context.Attach(Pilot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PilotExists(Pilot.id_pilota))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PilotExists(int id)
        {
            return _context.Pilot.Any(e => e.id_pilota == id);
        }
    }
}
