﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SBD.Data;
using SBD.Models;

namespace SBD.Pages.Pasazerowie
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
            return Page();
        }

        [BindProperty]
        public Pasazer Pasazer { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Pasazer.Add(Pasazer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
