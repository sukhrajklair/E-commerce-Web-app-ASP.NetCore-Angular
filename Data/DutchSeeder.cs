using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
  public class DutchSeeder
  {
    private DutchContext _ctx;
    private IWebHostEnvironment _hosting;
    private readonly UserManager<User> _userManager;

    public DutchSeeder(DutchContext ctx, IWebHostEnvironment hosting, UserManager<User> userManager)
    {
      _ctx = ctx;
      _hosting = hosting;
      _userManager = userManager;
    }

    public async Task SeedAsync()
    {
      _ctx.Database.EnsureCreated();

      User user = await _userManager.FindByEmailAsync("sukhrajklair@gmail.com");

      if (user == null)
      {
        user = new User()
        {
          FirstName = "Sukhraj",
          LastName = "Klair",
          Email = "sukhrajklair@gmail.com",
          UserName = "sukhrajklair@gmail.com"
        };

        var result = await _userManager.CreateAsync(user, "P@ssw0rd");
        if (result != IdentityResult.Success)
        {
          throw new InvalidOperationException("Could not create new user in seeder");
        }
      
      }
      if (!_ctx.Products.Any())
      {
        //Need to create sample data
        var filepath = Path.Combine(_hosting.ContentRootPath, "Data/art.json");
        var json = File.ReadAllText(filepath);
        var products = JsonSerializer.Deserialize<IEnumerable<Product>>(json);
        _ctx.Products.AddRange(products);

        var order = _ctx.Orders.Where(o => o.Id == 1).FirstOrDefault();
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
        _ctx.SaveChanges();
      }
    }
  }
}
