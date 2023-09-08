using Application.Features;
using Application.Features.Contacts.Queries;
using Application.Interfaces.Repositories;
using Domain.Contracts;
using Domain.Entities;
using Infrastructure.Repositories;
using MediatR;
using Server.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSwaggerGen();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddIdentity();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddControllers();
builder.Services.AddRazorPages();


builder.Services.AddScoped<IRequestHandler<GetAllContactsQuery, IEnumerable<Contact>>, GetAllContactsQueryHandler>();

builder.Services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
builder.Services.AddTransient(typeof(IRepositoryAsync<,>), typeof(RepositoryAsync<,>)); // to do extentions

//builder.Services.AddComandsAndQueries(); to do

builder.Services.AddJwtAuthentication(builder.Services.GetApplicationSettings(builder.Configuration));
builder.Services.AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
//app.UseAuthentication();
//app.UseAuthorization();

app.Initialize(builder.Configuration);

app.MapRazorPages();
app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();
