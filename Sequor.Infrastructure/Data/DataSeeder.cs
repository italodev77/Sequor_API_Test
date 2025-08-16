using Sequor.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Sequor.Infrastructure.Data
{
    public static class DataSeeder
    {
        public static void Seed(SequorDbContext context, IConfiguration config)
        {
            Console.WriteLine("Iniciando o seed de dados...");

            var baseUrl = config["ImageSettings:BaseUrl"];

            var users = new[]
            {
                new User
                {
                    Email = "engenheiro@fabrica.com",
                    Name = "Engenheiro de Produção",
                    InitialDate = new DateTime(2021, 01, 01),
                    EndDate = new DateTime(2025, 12, 31)
                },
                new User
                {
                    Email = "operador@fabrica.com",
                    Name = "Operador de Linha",
                    InitialDate = new DateTime(2021, 01, 01),
                    EndDate = new DateTime(2025, 12, 31)
                }
            };

            foreach (var user in users)
                if (!context.Users.Any(u => u.Email == user.Email))
                    context.Users.Add(user);

            var products = new[]
            {
                new Product { ProductCode = "CAR001", ProductDescription = "Toyota Corolla 2024", Image = baseUrl + "corolla.png", CycleTime = 18.5m },
                new Product { ProductCode = "CAR002", ProductDescription = "Honda Civic 2024", Image = baseUrl + "civic.png", CycleTime = 20.0m },
                new Product { ProductCode = "CAR003", ProductDescription = "Chevrolet Onix 2025", Image = baseUrl + "onix.png", CycleTime = 16.5m },
                new Product { ProductCode = "CAR004", ProductDescription = "Volkswagen Polo 2025", Image = baseUrl + "polo.png", CycleTime = 17.2m },
                new Product { ProductCode = "CAR005", ProductDescription = "Fiat Cronos 2025", Image = baseUrl + "cronos.png", CycleTime = 19.0m },
                new Product { ProductCode = "CAR006", ProductDescription = "Jeep Compass 2025", Image = baseUrl + "compass.png", CycleTime = 22.5m }
            };

            foreach (var product in products)
                if (!context.Products.Any(p => p.ProductCode == product.ProductCode))
                    context.Products.Add(product);

            var materials = new[]
            {
                new Material { MaterialCode = "PEC001", MaterialDescription = "Motor 2.0 Flex" },
                new Material { MaterialCode = "PEC002", MaterialDescription = "Câmbio CVT" },
                new Material { MaterialCode = "PEC003", MaterialDescription = "Rodas Liga Leve 17\"" },
                new Material { MaterialCode = "PEC004", MaterialDescription = "Painel Digital" },
                new Material { MaterialCode = "PEC005", MaterialDescription = "Bancos de Couro" },
                new Material { MaterialCode = "PEC006", MaterialDescription = "Sistema Multimídia 10\"" }
            };

            foreach (var material in materials)
                if (!context.Materials.Any(m => m.MaterialCode == material.MaterialCode))
                    context.Materials.Add(material);

            var productMaterials = new[]
            {
                new ProductMaterial { ProductCode = "CAR001", MaterialCode = "PEC001" },
                new ProductMaterial { ProductCode = "CAR001", MaterialCode = "PEC002" },
                new ProductMaterial { ProductCode = "CAR002", MaterialCode = "PEC002" },
                new ProductMaterial { ProductCode = "CAR002", MaterialCode = "PEC003" },
                new ProductMaterial { ProductCode = "CAR003", MaterialCode = "PEC001" },
                new ProductMaterial { ProductCode = "CAR003", MaterialCode = "PEC004" },
                new ProductMaterial { ProductCode = "CAR004", MaterialCode = "PEC005" },
                new ProductMaterial { ProductCode = "CAR005", MaterialCode = "PEC003" },
                new ProductMaterial { ProductCode = "CAR006", MaterialCode = "PEC006" }
            };

            foreach (var pm in productMaterials)
                if (!context.ProductMaterials.Any(x => x.ProductCode == pm.ProductCode && x.MaterialCode == pm.MaterialCode))
                    context.ProductMaterials.Add(pm);

            var orders = new[]
            {
                new Order { OrderId = "OP001", Quantity = 50, ProductCode = "CAR001" },
                new Order { OrderId = "OP002", Quantity = 30, ProductCode = "CAR002" },
                new Order { OrderId = "OP003", Quantity = 40, ProductCode = "CAR003" },
                new Order { OrderId = "OP004", Quantity = 20, ProductCode = "CAR004" },
                new Order { OrderId = "OP005", Quantity = 25, ProductCode = "CAR005" },
                new Order { OrderId = "OP006", Quantity = 15, ProductCode = "CAR006" }
            };

            foreach (var order in orders)
                if (!context.Orders.Any(o => o.OrderId == order.OrderId))
                    context.Orders.Add(order);

            var productions = new[]
            {
                new Production
                {
                    Email = "operador@fabrica.com",
                    OrderId = "OP001",
                    Date = DateTime.Now.AddDays(-1),
                    Quantity = 5,
                    MaterialCode = "PEC001",
                    CycleTime = 18.0m
                },
                new Production
                {
                    Email = "operador@fabrica.com",
                    OrderId = "OP001",
                    Date = DateTime.Now.AddDays(-2),
                    Quantity = 7,
                    MaterialCode = "PEC002",
                    CycleTime = 17.8m
                }
            };

            foreach (var prod in productions)
            {
                bool exists = context.Productions.Any(p =>
                    p.Email == prod.Email &&
                    p.OrderId == prod.OrderId &&
                    p.MaterialCode == prod.MaterialCode &&
                    p.Date.Date == prod.Date.Date);

                if (!exists)
                    context.Productions.Add(prod);
            }

            context.SaveChanges();
            Console.WriteLine("Seed de dados concluído com sucesso!");
        }
    }
}
