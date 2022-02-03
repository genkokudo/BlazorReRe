using BlazorSecond.Server.Models;
using BlazorSecond.Server.Services.Interfaces;
using Domain.Contracts;
using Domain.Entities.Catalog;
using Domain.Entities.Misc;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BlazorSecond.Server.Data
{
    /// <summary>
    /// IAuditableEntityを実装しているテーブルに関しては、CreatedByなどが自動入力されるので
    /// これらのフィールドはコントローラでセットしないでください。
    /// </summary>
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        private readonly ICurrentUserService _currentUserService;

        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions, ICurrentUserService currentUserService) : base(options, operationalStoreOptions)
        {
            _currentUserService = currentUserService;
        }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Brand> Brands { get; set; } = null!;
        public DbSet<Document> Documents { get; set; } = null!;
        public DbSet<DocumentType> DocumentTypes { get; set; } = null!;

        /// <summary>
        /// IAuditableEntityを実装している場合
        /// CreatedByなどを入力する機能を追加
        /// ※SaveChangesは変えてないので注意
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = DateTime.UtcNow;
                        entry.Entity.CreatedBy =  _currentUserService.UserId ?? "guest";
                        entry.Entity.LastModifiedBy = "";
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = DateTime.UtcNow;
                        entry.Entity.LastModifiedBy = _currentUserService.UserId ?? "guest";
                        break;
                }
            }
            if (_currentUserService.UserId == null)
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            else
            {
                return await base.SaveChangesAsync(cancellationToken);
                //return await base.SaveChangesAsync(_currentUserService.UserId, cancellationToken);    // AuditableContextは実装していないのでコメントアウト
            }
        }

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