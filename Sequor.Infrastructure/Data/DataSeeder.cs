using Sequor.Domain.Entities;
using System;
using System.Linq;

namespace Sequor.Infrastructure.Data
{
    public static class DataSeeder
    {
        public static void Seed(SequorDbContext context)
        {
            Console.WriteLine("Iniciando o seed de dados...");

            // 1️⃣ Usuários (funcionários)
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
            {
                if (!context.Users.Any(u => u.Email == user.Email))
                {
                    context.Users.Add(user);
                }
            }

            // 2️⃣ Produtos
            var products = new[]
            {
                new Product { ProductCode = "CAR001", ProductDescription = "Toyota Corolla 2024", Image = "0x000002", CycleTime = 18.5m },
                new Product { ProductCode = "CAR002", ProductDescription = "Honda Civic 2024", Image = "0x000000", CycleTime = 20.0m }
            };

            foreach (var product in products)
            {
                if (!context.Products.Any(p => p.ProductCode == product.ProductCode))
                {
                    context.Products.Add(product);
                }
            }

            // 3️⃣ Materiais
            var materials = new[]
            {
                new Material { MaterialCode = "PEC001", MaterialDescription = "Motor 2.0 Flex" },
                new Material { MaterialCode = "PEC002", MaterialDescription = "Câmbio CVT" },
                new Material { MaterialCode = "PEC003", MaterialDescription = "Rodas Liga Leve 17\"" }
            };

            foreach (var material in materials)
            {
                if (!context.Materials.Any(m => m.MaterialCode == material.MaterialCode))
                {
                    context.Materials.Add(material);
                }
            }

            context.SaveChanges(); // Salva antes de adicionar relacionamentos

            // 4️⃣ ProductMaterials
            var productMaterials = new[]
            {
                new ProductMaterial { ProductCode = "CAR001", MaterialCode = "PEC001" },
                new ProductMaterial { ProductCode = "CAR001", MaterialCode = "PEC002" },
                new ProductMaterial { ProductCode = "CAR002", MaterialCode = "PEC002" },
                new ProductMaterial { ProductCode = "CAR002", MaterialCode = "PEC003" }
            };

            foreach (var pm in productMaterials)
            {
                if (!context.ProductMaterials.Any(x => x.ProductCode == pm.ProductCode && x.MaterialCode == pm.MaterialCode))
                {
                    context.ProductMaterials.Add(pm);
                }
            }

            // 5️⃣ Orders
            var orders = new[]
            {
                new Order { OrderId = "OP001", Quantity = 50, ProductCode = "CAR001" },
                new Order { OrderId = "OP002", Quantity = 30, ProductCode = "CAR002" }
            };

            foreach (var order in orders)
            {
                if (!context.Orders.Any(o => o.OrderId == order.OrderId))
                {
                    context.Orders.Add(order);
                }
            }

            // 6️⃣ Productions
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
                {
                    context.Productions.Add(prod);
                }
            }

            context.SaveChanges();
            Console.WriteLine("Seed de dados concluído com sucesso!");
        }
    }
}
