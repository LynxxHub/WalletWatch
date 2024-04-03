using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WalletWatchAPI.Models;

namespace WalletWatchAPI.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<TransactionCategory> Categories { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<User>()
                .HasMany(u => u.Loans)
                .WithOne(l => l.User);
            builder.Entity<User>().
                HasMany(u => u.Transactions)
                .WithOne(t => t.User);
            builder.Entity<Loan>()
                .HasMany(l => l.Transactions)
                .WithOne(t => t.Loan);
            builder.Entity<Transaction>()
                .HasOne(t => t.Category);



            builder.Entity<Loan>()
                .Property(l => l.Amount)
                .HasPrecision(18, 2);
            builder.Entity<Loan>()
                .Property(l => l.PaymentAmount)
                .HasPrecision(18, 2);
            builder.Entity<Transaction>()
                .Property(t => t.Amount).HasPrecision(18, 2);
        }
    }
}
