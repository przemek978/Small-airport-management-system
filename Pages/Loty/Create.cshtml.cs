﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SBD.Data;
using SBD.Models;

namespace SBD.Pages.Loty
{
    public class CreateModel : PageModel
    {
        private readonly SBD.Data.AirPortContext _context;

        public CreateModel(SBD.Data.AirPortContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["id_lotniska_startowego"] = new SelectList(_context.Lotnisko, "id_lotniska", "id_lotniska");
        ViewData["id_lotniska_koncowego"] = new SelectList(_context.Lotnisko, "id_lotniska", "id_lotniska");
        ViewData["id_samolotu"] = new SelectList(_context.Samolot, "id_samolotu", "id_samolotu");
            return Page();
        }

        [BindProperty]
        public Lot Lot { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Lot.Add(Lot);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
