using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Sequor.Application.IRepositories;
using Sequor.Application.Services;
using Sequor.Application.Services.Interfaces;
using Sequor.Application.Utility;
using Sequor.Domain.EntitiesValidator;
using Sequor.Infrastructure.Data;
using Sequor.Infrastructure.RepositoryImp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SequorDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddScoped<IGetOrdersService, GetOrdersServiceImp>();
builder.Services.AddScoped<IGetProductionByEmailService, GetProductionByEmailService>();
builder.Services.AddScoped<ISetProductionService, SetProductionServiceImp>();

builder.Services.AddScoped<IOrderRepository, OrderRepositoryImp>();
builder.Services.AddScoped<IProductionRepository, ProductionRepositoryImp>();
builder.Services.AddScoped<IUserRepository, UserRepositoryImp>();

builder.Services.AddScoped<SetProductionValidation>();


builder.Services.AddSwaggerGen();




builder.Services.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SequorDbContext>();
    var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();

    try
    {
        Console.WriteLine("Aplicando migrações...");
        dbContext.Database.Migrate();
        Console.WriteLine("Migrações aplicadas.");

        Console.WriteLine("Executando seed de dados...");
        DataSeeder.Seed(dbContext, config);
        Console.WriteLine("Seed concluído.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao aplicar migração ou seed: {ex.Message}");
        throw;
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();   
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
