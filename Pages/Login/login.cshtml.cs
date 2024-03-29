using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SBD.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System;
using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Security.AccessControl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Policy;

namespace SBD.Pages.Login
{
    public class LoginModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public string Message { get; set; }
        [BindProperty]
        public User user { get; set; }
        public LoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool ValidateUser(User user)
        {
            string conn = _configuration.GetConnectionString("AirPortContext");
            SqlConnection connection = new SqlConnection(conn);
            connection.Open();
            var passwordHasher = new PasswordHasher<string>();
            string query = "SELECT nazwa_uzytkownika, haslo,stanowisko FROM pracownik";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user.stanowisko = reader.GetString(2);
                        //user.haslo = passwordHasher.HashPassword(null, user.haslo);
                        if (user.nazwa_uzytkownika == reader.GetString(0) && passwordHasher.VerifyHashedPassword(null, reader.GetString(1), user.haslo) == PasswordVerificationResult.Success)
                            return true;
                    }
                }
            }
            query = "SELECT nazwa_uzytkownika, haslo FROM pilot";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user.stanowisko = "Pilot";
                        //user.haslo = passwordHasher.HashPassword(null, user.haslo);
                        if (user.nazwa_uzytkownika == reader.GetString(0) && passwordHasher.VerifyHashedPassword(null, reader.GetString(1), user.haslo) == PasswordVerificationResult.Success)
                            return true;
                    }
                }
            }
            return false;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ValidateUser(user))
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.nazwa_uzytkownika),
                    new Claim(ClaimTypes.Role,user.stanowisko)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(1)
                };
                await HttpContext.SignInAsync("CookieAuthentication", new ClaimsPrincipal(claimsIdentity));
                //return RedirectToPage(returnUrl);
                return RedirectToPage("/Index");
            }
            return Page();
        }

        public void OnGet()
        {
        }
    }
}
