using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class DataContext : IdentityDbContext 
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<CostumerType> CostumerTypes { get; set; }
    public DbSet<Costumer> Costumers { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<PublishingCompany> PublishingCompanies { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<CostumerBuyBook> CostumerBuyBooks { get; set; }
    public DbSet<AuthorBook> AuthorBooks { get; set; }
    public DbSet<knowledgeArea> KnowledgeAreas { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<SupplierType> SupplierTypes { get; set; }
    public DbSet<CompanyType> CompanyTypes { get; set; }
    
}