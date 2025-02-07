﻿using Store.Project.Core.Entities;
using Store.Project.Repository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Project.Repository.Data
{
    public static class StoreDbContextSeed
    {
        public async static Task SeedAsync(StoreDbContext _context)
        {
            // To make the seeding one time Brand

            if(_context.Brands.Count()==0)  
            {
                // Brand

                // 1. Read Data From Json File

                var brandsData = File.ReadAllText(@"..\Store.Project.Repository\Data\DataSeed\brands.json");


                // 2. convert json string to list<T>

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                // 3. Seed data to DB
                if (brands is not null && brands.Count() > 0)
                {
                    await _context.Brands.AddRangeAsync(brands);
                    await _context.SaveChangesAsync();
                }
            }


            // To make the seeding one time Type

            if (_context.Types.Count() == 0)
            {
                // Type

                // 1. Read Data From Json File

                var typesData = File.ReadAllText(@"..\Store.Project.Repository\Data\DataSeed\types.json");


                // 2. convert json string to list<T>

                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                // 3. Seed data to DB
                if (types is not null && types.Count() > 0)
                {
                    await _context.Types.AddRangeAsync(types);
                    await _context.SaveChangesAsync();
                }
            }


            // To make the seeding one time Product

            if (_context.Products.Count() == 0)
            {
                // Product

                // 1. Read Data From Json File

                var productsData = File.ReadAllText(@"..\Store.Project.Repository\Data\DataSeed\products.json");


                // 2. convert json string to list<T>

                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                // 3. Seed data to DB
                if (products is not null && products.Count() > 0)
                {
                    await _context.Products.AddRangeAsync(products);
                    await _context.SaveChangesAsync();
                }
            }



        }
    }
}
