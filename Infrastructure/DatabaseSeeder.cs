using Application.Interfaces.Services;
using BlazorHero.Shared.Constants.Permission;
using CRMBlazorApp.Shared.Constants;
using CRMBlazorApp.Shared.Constants.Roles;
using CRMBlazorApp.Shared.Constants.User;
using Domain.Entities;
using Infrastructure.Contexts;
using Infrastructure.Helpers;
using Infrastructure.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Infrastructure;

public class DatabaseSeeder : IDatabaseSeeder
{
    private readonly MainContext _db;
    private readonly UserManager<CRMUser> _userManager;
    private readonly ILogger<DatabaseSeeder> _logger;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DatabaseSeeder(MainContext db,
            UserManager<CRMUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<DatabaseSeeder> logger)
    {
        _db = db;
        _logger = logger;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public void Initialize()
    {
        AddAdministrator();
        AddContacts();

        _db.SaveChanges();
    }
    private void AddContacts()
    {
        var contact = new Contact
        {
            Id = Guid.NewGuid(),
            FirstName = "Test",
            LastName = "Testng",
            CreatedBy = "asd",
            LastModifiedBy = "asd",
            CreatedOn = DateTime.UtcNow,
            LastModifiedOn = DateTime.UtcNow,
            FullName = "Test",
            Patronymic = "Test",
        };

        _db.Contacts.Add(contact);
    }
    private void AddBasicUser()
    {
        Task.Run(async () =>
        {
            //Check if User Exists
            var basicUser = new CRMUser
            {
                FirstName = "Егор",
                LastName = "Муравьев",
                Email = "egormuravev@yandex.ru",
                UserName = "Ziltoid",              
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };
            var basicUserInDb = await _userManager.FindByEmailAsync(basicUser.Email);
            if (basicUserInDb == null)
            {
                await _userManager.CreateAsync(basicUser, "123Qwerty!");
                // to do role await _userManager.AddToRoleAsync(basicUser, RoleConstants.BasicRole);
                _logger.LogInformation("Пользователь добавлен"); // to do localizer _localizer["Seeded User with Basic Role."]
            }
        }).GetAwaiter().GetResult();
    }

    private void AddAdministrator()
    {
        Task.Run(async () =>
        {
            //Check if Role Exists
            var adminRole = new IdentityRole(RoleConstants.AdministratorRole);
            var adminRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.AdministratorRole);
            if (adminRoleInDb == null)
            {
                await _roleManager.CreateAsync(adminRole);
                adminRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.AdministratorRole);
                _logger.LogInformation("Seeded Administrator Role.");
            }
            //Check if User Exists
            var superUser = new CRMUser
            {
                FirstName = "Egor",
                LastName = "Muravev",
                Email = "egormuravev@yandex.ru",
                UserName = "Ziltoid",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                CreatedOn = DateTime.Now,
                IsActive = true
            };
            var superUserInDb = await _userManager.FindByEmailAsync(superUser.Email);
            if (superUserInDb == null)
            {
                await _userManager.CreateAsync(superUser, UserConstants.DefaultPassword);
                var result = await _userManager.AddToRoleAsync(superUser, RoleConstants.AdministratorRole);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Seeded Default SuperAdmin User.");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        _logger.LogError(error.Description);
                    }
                }
            }

            foreach (var permission in Permissions.GetRegisteredPermissions())
            {
                await _roleManager.AddPermissionClaim(adminRoleInDb, permission);
            }
        }).GetAwaiter().GetResult();
    }
}
