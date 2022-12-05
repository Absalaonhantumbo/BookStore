using System.Text.Json;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Seed;

public class DbInitializer: IDbInitializer
{
    private readonly DataContext _context;

    public DbInitializer(DataContext context)
    {
        _context = context;
    }
    public void Initilize()
    {
        //Apply pending migrations
        try
        {
            if (_context.Database.GetPendingMigrations().Count() > 0) _context.Database.Migrate();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        if (_context.Countries.ToListAsync().GetAwaiter().GetResult().Count <= 0)
        {
            var countrySeed = File.ReadAllText("../Persistence/Seed/SeedData/CountrySeed.json");
            var countryList = JsonSerializer.Deserialize<List<Country>>(countrySeed);

            countryList.ForEach(x =>
            {
                x.CreatedAt = DateTime.UtcNow;
            });
        
            _context.Countries.AddRange(countryList);
        }
        
        if (_context.CompanyTypes.ToListAsync().GetAwaiter().GetResult().Count <= 0)
        {
            var companyTypesSeed = File.ReadAllText("../Persistence/Seed/SeedData/CompanyTypeSeed.json");
            var companyTypesList = JsonSerializer.Deserialize<List<CompanyType>>(companyTypesSeed);

            companyTypesList.ForEach(x =>
            {
                x.CreatedAt = DateTime.UtcNow;
            });
            _context.CompanyTypes.AddRange(companyTypesList);
        }
        
        if (_context.SupplierTypes.ToListAsync().GetAwaiter().GetResult().Count <= 0)
        {
            var supplierTypesSeed = File.ReadAllText("../Persistence/Seed/SeedData/SupplierTypeSeed.json");
            var supplierTypesList = JsonSerializer.Deserialize<List<SupplierType>>(supplierTypesSeed);

            supplierTypesList.ForEach(x =>
            {
                x.CreatedAt = DateTime.UtcNow;
            });
            _context.SupplierTypes.AddRange(supplierTypesList);
            _context.SaveChanges();
        } 
    }
}