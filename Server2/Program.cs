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

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddIdentity();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddControllers();

//builder.Services.AddScoped<IRequestHandler<GetAllContactsQuery, Contact>, GetAllContactsQueryHandler>();

#region to do вынести в отдельное расширение
Type entityType = typeof(AuditableEntity<Guid>); // Базовый тип
IEnumerable<Type> types = Assembly.GetAssembly(entityType).GetTypes().Where(type => type.IsSubclassOf(entityType));

Type baseQueryType = typeof(BaseGetAllQuery<>);
Type baseQueryHandlerType = typeof(BaseGetAllQueryHandler<>);
var assembly = Assembly.GetAssembly(baseQueryType);

foreach (var type in types)
{
    var queryType = baseQueryType.MakeGenericType(type);
    var queryConcreteQueryType = assembly?.GetTypes().FirstOrDefault(type => type.IsSubclassOf(queryType));

    var queryHandlerType = baseQueryHandlerType.MakeGenericType(type);
    var queryConcreteQueryHandlerType = assembly?.GetTypes().FirstOrDefault(type => type.IsSubclassOf(queryHandlerType));

    var serviceTypeForAdding = typeof(IRequestHandler<,>).MakeGenericType(queryConcreteQueryType, type);

    builder.Services.AddScoped(serviceTypeForAdding, queryConcreteQueryHandlerType);
}
#endregion

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
