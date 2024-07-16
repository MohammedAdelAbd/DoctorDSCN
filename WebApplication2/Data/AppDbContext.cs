using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;

namespace WebApplication2.Data
{
    public class AppDbContext : DbContext
    {
            public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
            {
            }
            public virtual DbSet<User> Users { get; set; }
            public virtual DbSet<UserInfo> userInfos { get; set; }
            public virtual DbSet<Customer> Customers { get; set; }
            public virtual DbSet<Product> Products { get; set; }
            public virtual DbSet<CustomerProduct> CustomerProducts { get; set; }
            //public virtual DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
            public virtual DbSet<UserRole> UserRoles { get; set; }
    }
}
