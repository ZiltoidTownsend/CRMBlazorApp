using Application.Interfaces.Services;
using Infrastructure.Contexts;
using Infrastructure.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public class DatabaseSeeder : IDatabaseSeeder
{
    private readonly MainContext _db;
    private readonly UserManager<CRMUser> _userManager;
    private readonly ILogger<DatabaseSeeder> _logger;

    public DatabaseSeeder(MainContext db,
            UserManager<CRMUser> userManager,
            ILogger<DatabaseSeeder> logger)
    {
        _db = db;
        _logger = logger;
        _userManager = userManager;
    }

    public void Initialize()
    {
        AddBasicUser();
        _db.SaveChanges();
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
}
