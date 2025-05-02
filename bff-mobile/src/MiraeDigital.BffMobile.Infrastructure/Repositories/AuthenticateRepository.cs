using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MiraeDigital.BffMobile.Domain.AggregatesModel.AuthenticateAggregate;
using MiraeDigital.BffMobile.Domain.AggregatesModel.AuthenticateAggregate.Queries;
using MiraeDigital.BffMobile.Domain.SeedWork;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiraeDigital.BffMobile.Infrastructure.Repositories
{
    internal class AuthenticateRepository : IAuthenticateRepository
    {
        private readonly ILogger<AuthenticateRepository> _logger;
        private readonly Context _context;

        public IUnitOfWork UnitOfWork => _context;

        public AuthenticateRepository(
            ILogger<AuthenticateRepository> logger,
            Context context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task AddAsync(Authenticate sample)
        {
            try
            {
                await _context.Authenticates.AddAsync(sample);
                await UnitOfWork.SaveChangesAsync();
                _logger.LogInformation($"New Authenticate successfuly inserted - Id: {sample.Id}");
            }
            catch (NpgsqlException ex)
            {
                _logger.LogError(ex, $"Error Postgrees to insert Authenticate on database");
                sample.AddError(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error to insert Authenticate on database");
                sample.AddError(ex.Message);
            }
        }

        public async Task<IEnumerable<Authenticate>> GetAllAsync()
        {
            return await _context.Authenticates.ToListAsync();
        }

        public async Task<PagedResult<Authenticate>> GetAllPagedAsync(GetAllAuthenticatesPagedQuery query)
        {
            try
            {
                IQueryable<Authenticate> queryable = _context.Authenticates;
                var pagedResult = PagedResult<Authenticate>.Create(query);
                var skip = pagedResult.Skip();

                var totalRows = await queryable.CountAsync();

                var result = await queryable.Skip(skip)
                    .Take(pagedResult.Limit)
                    .ToListAsync();

                return pagedResult.BuildResult(result, totalRows);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error to search paginate {query}");
                return null;
            }
        }

        public async Task<Authenticate> GetByIdAsync(long id)
        {
            return await _context.Authenticates.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Authenticate sample)
        {
            try
            {
                sample.Update();
                _context.Authenticates.Update(sample);
                await UnitOfWork.SaveChangesAsync();
                _logger.LogInformation($"Authenticate successfuly Updated - Id: {sample.Id}");
            }
            catch (NpgsqlException ex)
            {
                _logger.LogError(ex, $"Error Postgrees to update sample on database. - Id: {sample?.Id}");
                sample.AddError(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error to update InvestmentFund on database. - Id: {sample?.Id}");
                sample.AddError(ex.Message);
            }
        }
    }
}
