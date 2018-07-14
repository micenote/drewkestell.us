using DrewKestellSite.Configuration;
using DrewKestellSite.Data;
using DrewKestellSite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace DrewKestellSite.Controllers.Admin
{
    public class UserController : Controller
    {
        readonly ApplicationContext context;
        readonly IPasswordHasher<User> passwordHasher;
        readonly ApiConfiguration config;

        public UserController(ApplicationContext context,
            IPasswordHasher<User> passwordHasher,
            IOptions<ApiConfiguration> config)
        {
            this.context = context;
            this.passwordHasher = passwordHasher;
            this.config = config.Value;
        }

        [HttpGet("/Admin/User/Create")]
        public IActionResult Create() => View("~/Views/Admin/User/Create.cshtml");

        [HttpPost("/Admin/User/Create")]
        public async Task<IActionResult> Create(string username, string password, string adminKey)
        {
            if (adminKey != config.AdminKey)
                throw new AuthenticationException("Invalid Key!");

            var user = new User
            {
                Username = username
            };
            user.HashedPassword = passwordHasher.HashPassword(user, password);
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return RedirectToAction("Create", "Session");
        }
    }
}
