using Domain.Entities.Catalog;
using Domain.Entities.Misc;
using Duende.IdentityServer.EntityFramework.Options;
using Infrastructure.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Contexts
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }

        ///// <summary>
        ///// CreatedByなどを入力する機能を追加
        ///// ※SaveChangesは変えてないので注意
        ///// </summary>
        ///// <param name="cancellationToken"></param>
        ///// <returns></returns>
        //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        //{
        //    foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().ToList())
        //    {
        //        switch (entry.State)
        //        {
        //            case EntityState.Added:
        //                entry.Entity.CreatedOn = _dateTimeService.NowUtc;
        //                entry.Entity.CreatedBy = _currentUserService.UserId;
        //                break;

        //            case EntityState.Modified:
        //                entry.Entity.LastModifiedOn = _dateTimeService.NowUtc;
        //                entry.Entity.LastModifiedBy = _currentUserService.UserId;
        //                break;
        //        }
        //    }
        //    if (_currentUserService.UserId == null)
        //    {
        //        return await base.SaveChangesAsync(cancellationToken);
        //    }
        //    else
        //    {
        //        return await base.SaveChangesAsync(_currentUserService.UserId, cancellationToken);
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // decimalの桁数は固定する
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }

            base.OnModelCreating(builder);
        }
    }
}