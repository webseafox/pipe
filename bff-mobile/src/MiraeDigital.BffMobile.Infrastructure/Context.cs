using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using MiraeDigital.BffMobile.Domain.AggregatesModel.AuthenticateAggregate;
using MiraeDigital.BffMobile.Domain.SeedWork;
using MiraeDigital.BffMobile.Infrastructure.Extensions;
using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MiraeDigital.BffMobile.Infrastructure
{
    public class Context : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "sample";
        private IDbContextTransaction _currentTransaction;

        public DbSet<Authenticate> Authenticates { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);


            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.SetSchema(DEFAULT_SCHEMA.ToLower());

                StoreObjectIdentifier.Create(entityType, StoreObjectType.Table);

                entityType.GetProperties()
                    .ToList()
                    .ForEach(p => p.SetColumnName(p.GetColumnName(StoreObjectIdentifier.Create(entityType, StoreObjectType.Table).Value).AsNpgsqlConvention()));

                var pk = entityType.FindPrimaryKey();

                if (pk is not null)
                    pk.SetName(pk.GetName().ToLower());

                entityType.GetForeignKeys()
                    .ToList()
                    .ForEach(fk => fk.SetConstraintName(fk.GetConstraintName().ToLower()));


                entityType.GetIndexes()
                    .ToList()
                    .ForEach(ix => ix.SetDatabaseName(ix.GetDatabaseName().ToLower()));
            }

        }

        public async Task<IDisposable> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync()
        {
            if (_currentTransaction == null)
                return;

            try
            {
                await SaveChangesAsync(cancellationToken: CancellationToken.None);
                await _currentTransaction.CommitAsync();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync()
        {
            try
            {
                await _currentTransaction?.RollbackAsync();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
            }
        }

        public async Task SaveChangesAsync()
        {
            await base.SaveChangesAsync(cancellationToken: CancellationToken.None);
        }
    }

    public class ContextFactory : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseNpgsql("Host=localhost;port=6543;Database=dev-sample-service;Username=postgres;Password=postgrespassword", opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds));

            return new Context(optionsBuilder.Options);
        }
    }
}
