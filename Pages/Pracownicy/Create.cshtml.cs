﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SBD.Data;
using SBD.Models;

namespace SBD.Pages.Pracownicy
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
         
            ViewData["id_lotniska"] = new SelectList(_context.Set<Lotnisko>(), "id_lotniska", "nazwa");
            return Page();
        }

        [BindProperty]
        public Pracownik Pracownik { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var passwordHasher = new PasswordHasher<string>();
            Pracownik.haslo = passwordHasher.HashPassword(null, Pracownik.haslo);
            
            _context.Pracownik.Add(Pracownik);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
