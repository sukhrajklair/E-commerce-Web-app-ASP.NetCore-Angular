using DutchTreat.Data.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
  public class DutchRepository : IDutchRepository
  {
    private DutchContext _ctx;
    private ILogger<DutchRepository> _logger;

    public DutchRepository(DutchContext ctx, ILogger<DutchRepository> logger)
    {
      _ctx = ctx;
      _logger = logger;
    }

    public IEnumerable<Product> GetAllProducts()
    {
      try
      {
        _logger.LogInformation("All products were queried");
        return _ctx.Products
                    .OrderBy(p => p.Title)
                    .ToList();
      }
      catch (Exception ex)
      {
        _logger.LogError($"Failed to get all products: {ex}");
        return null;
      }
      
    }
    public IEnumerable<Product> GetProductsByCategory(string category)
    {
      try
      {
        return _ctx.Products
                  .OrderBy(p => p.Category == category)
                  .ToList();
      }
      catch (Exception ex)
      {
        _logger.LogError($"Failed to get all products: {ex}");
        return null;
      }
      
    }

    public bool SaveAll()
    {
      try
      {
        return _ctx.SaveChanges() > 0;
      }
      catch (Exception ex)
      {
        _logger.LogError($"Failed to get all products: {ex}");
        return false;
      }
      
    }
  }
}
