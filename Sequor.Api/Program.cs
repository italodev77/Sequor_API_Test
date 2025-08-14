using Microsoft.EntityFrameworkCore;
using Sequor.Application.IRepositories;
using Sequor.Application.IService;
using Sequor.Application.Mapping;
using Sequor.Application.ServiceImp;
using Sequor.Infrastructure.Data;
using Sequor.Infrastructure.RepositoryImp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SequorDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IGetOrdersService, GetOrdersServiceImp>();
builder.Services.AddScoped<IGetProductionByEmailService, GetProductionByEmailService>();
builder.Services.AddScoped<ISetProductionService, SetProductionServiceImp>();

builder.Services.AddScoped<IOrderRepository, OrderRepositoryImp>();
builder.Services.AddScoped<IProductionRepository, ProductionRepositoryImp>();
builder.Services.AddScoped<IUserRepository, UserRepositoryImp>();

builder.Services.AddOpenApi();

builder.Services.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SequorDbContext>();

    // Garante que o banco existe e está atualizado
    dbContext.Database.Migrate();

    // Popula o banco com dados iniciais
    DataSeeder.Seed(dbContext);
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
