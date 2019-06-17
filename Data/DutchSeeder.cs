﻿using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchSeeder
    {
        private readonly DutchContext _context;
        private readonly IHostingEnvironment _hosting;
        private readonly UserManager<StoreUser> _userManager;

        public DutchSeeder(
            DutchContext context, 
            IHostingEnvironment hosting,
            UserManager<StoreUser> userManager
        ) {
            _context = context;
            _hosting = hosting;
            _userManager = userManager;
        }
        public async Task SeedAsync()
        {
            _context.Database.EnsureCreated();

            StoreUser user = await _userManager.FindByEmailAsync("aa779@hotmail.com");

            if (user == null)
            {
                user = new StoreUser()
                {
                    FirstName = "Alvaro",
                    LastName = "Gonçalves",
                    Email = "aa779@hotmail.com",
                    UserName = "aa779@hotmail.com"
                };
                var result = await _userManager.CreateAsync(user, "Kiss&!45");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create new user in seeder");
                }
            }

            if (!_context.Products.Any())
            {
                //Need to create some sample data
                var filepath = Path.Combine(_hosting.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(filepath);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);

                _context.Products.AddRange(products);

                var order = _context.Orders.Where(o => o.Id == 1).FirstOrDefault();
                if (order != null)
                {
                    order.User = user;
                    order.Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    };
                }
                _context.SaveChanges();
            }
        }
    }
}
