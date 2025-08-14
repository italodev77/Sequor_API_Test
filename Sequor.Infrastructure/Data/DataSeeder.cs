using Sequor.Domain.Entities;
using System;

namespace Sequor.Infrastructure.Data
{
    public static class DataSeeder
    {
        public static void Seed(SequorDbContext context)
        {
            // Usuários (funcionários)
            if (!context.Users.Any())
            {
                context.Users.AddRange(
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
                );
            }

            // Modelos de carros (Products)
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        ProductCode = "CAR001",
                        ProductDescription = "Toyota Corolla 2024",
                        Image = "0x000002",
                        CycleTime = 18.5m
                    },
                    new Product
                    {
                        ProductCode = "CAR002",
                        ProductDescription = "Honda Civic 2024",
                        Image = "0x000000",
                        CycleTime = 20.0m
                    }
                );
            }

            // Peças utilizadas na montagem (Materials)
            if (!context.Materials.Any())
            {
                context.Materials.AddRange(
                    new Material { MaterialCode = "PEC001", MaterialDescription = "Motor 2.0 Flex" },
                    new Material { MaterialCode = "PEC002", MaterialDescription = "Câmbio CVT" },
                    new Material { MaterialCode = "PEC003", MaterialDescription = "Rodas Liga Leve 17\"" }
                );
            }

            // Relacionamento modelo → peças (ProductMaterials)
            if (!context.ProductMaterials.Any())
            {
                context.ProductMaterials.AddRange(
                    new ProductMaterial { ProductCode = "CAR001", MaterialCode = "PEC001" },
                    new ProductMaterial { ProductCode = "CAR001", MaterialCode = "PEC002" },
                    new ProductMaterial { ProductCode = "CAR002", MaterialCode = "PEC002" },
                    new ProductMaterial { ProductCode = "CAR002", MaterialCode = "PEC003" }
                );
            }

            // Ordens de produção (Orders)
            if (!context.Orders.Any())
            {
                context.Orders.AddRange(
                    new Order
                    {
                        OrderId = "OP001",
                        Quantity = 50, // quantidade de carros a fabricar
                        ProductCode = "CAR001"
                    },
                    new Order
                    {
                        OrderId = "OP002",
                        Quantity = 30,
                        ProductCode = "CAR002"
                    }
                );
            }

            // Produções realizadas (Productions)
            if (!context.Productions.Any())
            {
                context.Productions.AddRange(
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
                );
            }

            context.SaveChanges();
        }
    }
}
