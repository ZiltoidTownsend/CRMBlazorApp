using Application.Features;
using Application.Features.Contacts.Queries;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Repositories;
using MediatR;
using Server.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddIdentity();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddControllers();

builder.Services.AddScoped<IRequestHandler<GetAllContactsQuery, Contact>, GetAllContactsQueryHandler>();
builder.Services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
builder.Services.AddTransient(typeof(IRepositoryAsync<,>), typeof(RepositoryAsync<,>)); // to do extentions

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.Initialize(builder.Configuration);

app.MapControllers();

app.Run();
